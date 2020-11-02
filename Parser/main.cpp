#include <exception>
#include <iostream>

#include "CaffParser.h"
#include "WebPAnim.h"

int main(int argc, char *argv[]) {
    FILE *inputFile = stdin;
    FILE *outputFile = stdout;

    if (argc > 1) {
        inputFile = fopen(argv[1], "rb");
    }

    if (argc > 2) {
        outputFile = fopen(argv[2], "wb");
    }

    try {
        WebPAnim anim;

        CaffParser caffParser{[&](auto header){},[&](auto credits){
                fprintf(stderr, "Creation date: %04d. %02d. %02d. %02d:%02d\n",
                    credits.creationYear,
                    credits.creationMonth,
                    credits.creationDay,
                    credits.creationHour,
                    credits.creationMinute);
                std::cerr << "Creator: " << credits.creatorName << std::endl;
            }, [&](auto frame){
            anim.addFrame(frame.data.data(), frame.width, frame.height, frame.duration);
        }};
        caffParser.parse(inputFile);

        anim.generate(outputFile);

    } catch (std::exception &e) {
        std::cerr << e.what();
    }

    if (argc > 1) {
        fclose(inputFile);
    }

    if (argc > 2) {
        fclose(outputFile);
    }

    return 0;
}
