#include <cstdio>

#include "CaffEncoder.h"

int main(int argc, char *argv[]) {

    FILE *outputFile = fopen("../4.caff", "wb");

    CaffHeader header;
    CaffCredits credits;
    CaffFrame frame1, frame2;

    header.frameCount = 2;

    credits.creationYear = 2020;
    credits.creationMonth = 11;
    credits.creationDay = 7;
    credits.creationHour = 21;
    credits.creationMinute = 38;
    credits.creatorName = "Feher Balazs";

    frame1.caption = "feherfekete";
    frame1.duration = 100;
    frame1.width = 2;
    frame1.height = 2;
    frame1.data = { 0xFF, 0xFF, 0xFF, 0x00, 0x00, 0x00,
                    0x00, 0x00, 0x00, 0xFF, 0xFF, 0xFF };

    frame2.caption = "feketefeher";
    frame2.duration = 100;
    frame2.width = 2;
    frame2.height = 2;
    frame2.data = { 0x00, 0x00, 0x00, 0xFF, 0xFF, 0xFF,
                    0xFF, 0xFF, 0xFF, 0x00, 0x00, 0x00 };

    CaffEncoder::writeHeader(outputFile, header);
    CaffEncoder::writeCredits(outputFile, credits);
    CaffEncoder::writeFrame(outputFile, frame1);
    CaffEncoder::writeFrame(outputFile, frame2);

    fclose(outputFile);

    return 0;
}
