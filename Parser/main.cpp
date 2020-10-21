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

    Parser::process(inputFile, outputFile);

    if (argc > 1) {
        fclose(inputFile);
    }

    if (argc > 2) {
        fclose(outputFile);
    }

    return 0;
}
