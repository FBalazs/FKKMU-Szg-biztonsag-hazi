#include <exception>
#include <iostream>

#include "CaffParser.h"
#include "WebpEncoder.h"

int main(int argc, char *argv[]) {
    FILE *inputFile = stdin;
    FILE *outputFile = stdout;

    FILE *metaFile = nullptr;

    bool isInputFromFile = false, isOutputToFile = false;

    try {
        for (int i = 1; i < argc; ++i) {
            std::string arg{argv[i]};

            if (arg == "-i" || arg == "--in" || arg == "--input") {
                if (isInputFromFile) {
                    throw std::runtime_error{"Multiple input files are not supported."};
                }
                if (i+1 >= argc) {
                    throw std::runtime_error{"Usage: -i <path> or --in <path> or --input <path>"};
                }
                isInputFromFile = true;
                inputFile = fopen(argv[++i], "rb");
                if (inputFile == nullptr || ferror(inputFile) != 0) {
                    throw std::runtime_error{"Error opening input file."};
                }
            } else if (arg == "-o" || arg == "--out" || arg == "--output") {
                if (isOutputToFile) {
                    throw std::runtime_error{"Multiple output files are not supported."};
                }
                if (i+1 >= argc) {
                    throw std::runtime_error{"Usage: -o <path> or --out <path> or --output <path>"};
                }
                isOutputToFile = true;
                outputFile = fopen(argv[++i], "wb");
                if (outputFile == nullptr || ferror(outputFile) != 0) {
                    throw std::runtime_error{"Error creating output file."};
                }
            } else if (arg == "-m" || arg == "--meta") {
                if (metaFile != nullptr) {
                    throw std::runtime_error{"Multiple meta outputs are not supported."};
                }
                if (i+1 >= argc) {
                    throw std::runtime_error{"Usage: -m <path> or --meta <path>"};
                }
                metaFile = fopen(argv[++i], "w");
                if (metaFile == nullptr || ferror(metaFile) != 0) {
                    throw std::runtime_error{"Error creating meta file."};
                }
            } else if (arg == "-pm" || arg == "--printmeta") {
                if (metaFile != nullptr) {
                    throw std::runtime_error{"Multiple meta outputs are not supported."};
                }
                metaFile = stdout;
            }
        }

        WebpEncoder webpEncoder{};
        webpEncoder.init();

        CaffParser caffParser{[&](auto header) {}, [&](auto credits) {
                if (metaFile != nullptr) {
                    fprintf(metaFile, "Creation date: %04d. %02d. %02d. %02d:%02d\n",
                            credits.creationYear,
                            credits.creationMonth,
                            credits.creationDay,
                            credits.creationHour,
                            credits.creationMinute);
                    fprintf(metaFile, "Creator: %s\n", credits.creatorName.c_str());
                }
            }, [&](auto frame) {
                if (metaFile != nullptr) {
                    fprintf(metaFile, "Frame caption: %s\n", frame.caption.c_str());
                    fprintf(metaFile, "Frame tags:\n");
                    for (auto &tag : frame.tags) {
                        fprintf(metaFile, "\t%s\n", tag.c_str());
                    }
                }
                webpEncoder.addFrame(frame.duration, frame.width, frame.height, frame.data.data());
            }
        };
        caffParser.parse(inputFile);
        webpEncoder.writeToFile(outputFile);
    } catch (const std::exception &e) {
        std::cerr << e.what();
    }

    if (isInputFromFile) {
        fclose(inputFile);
    }

    if (isOutputToFile) {
        fclose(outputFile);
    }

    if (metaFile != nullptr && metaFile != stdout) {
        fclose(metaFile);
    }

    return 0;
}
