#include <stdexcept>

#include "Parser.h"

template<typename T>
void Parser::read(T *pointer, size_t count, const char *errorText) {
    if (fread(pointer, sizeof(T), count, inputFile) != count) {
        throw std::runtime_error(errorText);
    }
}

void Parser::readHeader(uint64_t length) {
    uint8_t magic[4];
    uint64_t headerSize;
    read(magic, 4, "Invalid CAFF file. Could not read \"magic\".");
    read(&headerSize, 1, "Invalid CAFF file. Could not read header size.");
    read(&remainingFrames, 1, "Invalid CAFF file. Could not read animation count.");
}

void Parser::readCredits(uint64_t length) {
    read(&animation.creationYear, 1, "Invalid CAFF file. Could not read creation year.");
    read(&animation.creationMonth, 1, "Invalid CAFF file. Could not read creation month.");
    read(&animation.creationDay, 1, "Invalid CAFF file. Could not read creation day.");
    read(&animation.creationHour, 1, "Invalid CAFF file. Could not read creation hour.");
    read(&animation.creationMinute, 1, "Invalid CAFF file. Could not read creation minute.");

    uint64_t creatorNameLength;
    read(&creatorNameLength, 1, "Invalid CAFF file. Could not read creator length.");
    uint8_t tempCreator[creatorNameLength];
    read(tempCreator, creatorNameLength, "Invalid CAFF file. Could not read creator.");
    animation.creatorName = std::string{tempCreator, tempCreator + creatorNameLength};
}

void Parser::readFrame(uint64_t length) {
    if (remainingFrames == 0) {
        throw std::runtime_error("Invalid CAFF file. There should be no more animation blocks.");
    }
    --remainingFrames;

    AnimationFrame frame;

    read(&frame.duration, 1, "Invalid CAFF file. Could not read animation frame display duration.");

    uint8_t magic[4];
    read(magic, 4, "Invalid CAFF file. Could not read CIFF magic.");

    uint64_t headerSize;
    read(&headerSize, 1, "Invalid CAFF file. Could not read CIFF header size.");

    uint64_t contentSize;
    read(&contentSize, 1, "Invalid CAFF file. Could not read CIFF content size.");

    read(&frame.width, 1, "Invalid CAFF file. Could not read CIFF width.");
    read(&frame.height, 1, "Invalid CAFF file. Could not read CIFF height.");

    uint64_t remainingByteCount = headerSize - 4 - 8 - 8 - 8 - 8;
    uint8_t remainingBytes[remainingByteCount];
    read(remainingBytes, remainingByteCount, "Invalid CAFF file. Could not read CIFF caption and tags.");
    // TODO parse caption and tags into frame fields

    if (contentSize != frame.width*frame.height*3) {
        throw std::runtime_error("Invalid CAFF file. Invalid CIFF content size.");
    }

    frame.data.resize(contentSize);
    read(&frame.data[0], contentSize, "Invalid CAFF file. Could not read CIFF content.");

    animation.frames.push_back(std::move(frame));
}

void Parser::readBlock() {
    uint8_t blockType;
    uint64_t length;
    read(&blockType, 1, "Invalid CAFF file. Could not read block ID.");
    read(&length, 1, "Invalid CAFF file. Could not read block length.");
    switch (blockType) {
        case 1:
            readHeader(length);
            break;
        case 2:
            readCredits(length);
            break;
        case 3:
            readFrame(length);
            break;
        default:
            throw std::runtime_error("Invalid CAFF file. Unknown block ID.");
    }
}

void Parser::read() {
    do {
        readBlock();
    } while(!feof(inputFile) && remainingFrames > 0);
}

void Parser::write() {

}

void Parser::process() {
    read();
    write();
}
