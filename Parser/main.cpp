#include <exception>
#include <iostream>

#include "Parser.h"

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
        Parser parser{inputFile, outputFile};
        parser.process();

        fprintf(stderr, "Creation date: %04d. %02d. %02d. %02d:%02d\n",
               parser.animation.creationYear,
               parser.animation.creationMonth,
               parser.animation.creationDay,
               parser.animation.creationHour,
               parser.animation.creationMinute);
        std::cerr << "Creator: " << parser.animation.creatorName << std::endl;
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
