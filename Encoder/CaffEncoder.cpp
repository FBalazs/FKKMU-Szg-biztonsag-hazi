#include "CaffEncoder.h"

template<typename T>
void CaffEncoder::writeData(FILE *file, const T *pointer, size_t count) {
    fwrite(pointer, sizeof(T), count, file);
}

void CaffEncoder::writeHeader(FILE *file, const CaffHeader &header) {
    const char *magic = "CAFF";
    uint64_t headerSize = 4 + 8 + 8;

    uint8_t blockId = 1;
    uint64_t blockLength = headerSize;

    writeData(file, &blockId, 1);
    writeData(file, &blockLength, 1);
    writeData(file, magic, 4);
    writeData(file, &headerSize, 1);
    writeData(file, &header.frameCount, 1);
}

void CaffEncoder::writeCredits(FILE *file, const CaffCredits &credits) {
    uint64_t creatorLength = credits.creatorName.length();

    uint8_t blockId = 2;
    uint64_t blockLength = 2 + 1 + 1 + 1 + 1 + 8 + creatorLength;

    writeData(file, &blockId, 1);
    writeData(file, &blockLength, 1);
    writeData(file, &credits.creationYear, 1);
    writeData(file, &credits.creationMonth, 1);
    writeData(file, &credits.creationDay, 1);
    writeData(file, &credits.creationHour, 1);
    writeData(file, &credits.creationMinute, 1);
    writeData(file, &creatorLength, 1);
    writeData(file, credits.creatorName.c_str(), creatorLength);
}

void CaffEncoder::writeFrame(FILE *file, const CaffFrame &frame) {
    const char *magic = "CIFF";
    uint64_t headerSize = 4 + 8 + 8 + 8 + 8 + frame.caption.size() + 1;
    for (auto &tag : frame.tags) headerSize += tag.size() + 1;
    char cn = '\n';
    char c0 = '\0';
    uint64_t contentSize = frame.data.size();

    uint8_t blockId = 3;
    uint64_t blockLength = 8 + headerSize + contentSize;

    writeData(file, &blockId, 1);
    writeData(file, &blockLength, 1);
    writeData(file, &frame.duration, 1);
    writeData(file, magic, 4);
    writeData(file, &headerSize, 1);
    writeData(file, &contentSize, 1);
    writeData(file, &frame.width, 1);
    writeData(file, &frame.height, 1);
    writeData(file, frame.caption.c_str(), frame.caption.size());
    writeData(file, &cn, 1);
    for (auto &tag : frame.tags) {
        writeData(file, tag.c_str(), tag.size());
        writeData(file, &c0, 1);
    }
    writeData(file, &frame.data[0], frame.data.size());
}
