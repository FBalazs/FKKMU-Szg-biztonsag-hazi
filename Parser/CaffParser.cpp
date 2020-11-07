#include "CaffParser.h"

#include <stdexcept>
#include <sstream>

template<typename T>
void CaffParser::readData(FILE *file, T *pointer, size_t count, const char *errorText) const {
    if (fread(pointer, sizeof(T), count, file) != count) {
        throw std::runtime_error{errorText};
    }
}

void CaffParser::readHeader(FILE *file, uint64_t &remainingFrames, uint64_t length) const {
    CaffHeader header{};

    uint8_t magic[4];
    uint64_t headerSize;
    readData(file, magic, 4, "Invalid CAFF file. Could not read \"magic\".");
    readData(file, &headerSize, 1, "Invalid CAFF file. Could not read header size.");
    readData(file, &remainingFrames, 1, "Invalid CAFF file. Could not read animation count.");

    header.frameCount = remainingFrames;
    headerConsumer(header);
}

void CaffParser::readCredits(FILE *file, uint64_t length) const {
    CaffCredits caffCredits{};

    readData(file, &caffCredits.creationYear, 1, "Invalid CAFF file. Could not read creation year.");
    readData(file, &caffCredits.creationMonth, 1, "Invalid CAFF file. Could not read creation month.");
    readData(file, &caffCredits.creationDay, 1, "Invalid CAFF file. Could not read creation day.");
    readData(file, &caffCredits.creationHour, 1, "Invalid CAFF file. Could not read creation hour.");
    readData(file, &caffCredits.creationMinute, 1, "Invalid CAFF file. Could not read creation minute.");

    uint64_t creatorNameLength;
    readData(file, &creatorNameLength, 1, "Invalid CAFF file. Could not read creator length.");
    std::vector<uint8_t> creatorNameBytes;
    creatorNameBytes.resize(creatorNameLength);
    readData(file, &creatorNameBytes[0], creatorNameLength, "Invalid CAFF file. Could not read creator.");
    caffCredits.creatorName = std::string{creatorNameBytes.begin(), creatorNameBytes.end()};

    creditsConsumer(caffCredits);
}

void CaffParser::readFrame(FILE *file, uint64_t &remainingFrames, uint64_t length) const {
    if (remainingFrames == 0) {
        throw std::runtime_error{"Invalid CAFF file. There should be no more animation blocks."};
    }
    --remainingFrames;

    CaffFrame frame;

    readData(file, &frame.duration, 1, "Invalid CAFF file. Could not read animation frame display duration.");

    uint8_t magic[4];
    readData(file, magic, 4, "Invalid CAFF file. Could not read CIFF magic.");

    uint64_t headerSize;
    readData(file, &headerSize, 1, "Invalid CAFF file. Could not read CIFF header size.");

    uint64_t contentSize;
    readData(file, &contentSize, 1, "Invalid CAFF file. Could not read CIFF content size.");

    readData(file, &frame.width, 1, "Invalid CAFF file. Could not read CIFF width.");
    readData(file, &frame.height, 1, "Invalid CAFF file. Could not read CIFF height.");

    const uint64_t remainingByteCount = headerSize - 4 - 8 - 8 - 8 - 8;

    std::vector<uint8_t> remainingBytes;
    remainingBytes.resize(remainingByteCount, 0);
    readData(file, &remainingBytes[0], remainingByteCount, "Invalid CAFF file. Could not read CIFF caption and tags.");

    std::stringstream ss{};
    uint64_t i = 0;
    while (i < remainingByteCount && remainingBytes[i] != '\n') {
        ss << (char)remainingBytes[i];
        ++i;
    }
    frame.caption = ss.str();
    ss = std::stringstream{};
    ++i;
    while (i < remainingByteCount) {
        if (remainingBytes[i] == '\0') {
            frame.tags.push_back(ss.str());
            ss = std::stringstream{};
        } else {
            ss << remainingBytes[i];
        }
        ++i;
    }

    if (contentSize != frame.width*frame.height*3) {
        throw std::runtime_error{"Invalid CAFF file. Invalid CIFF content size."};
    }

    frame.data.resize(contentSize);
    readData(file, &frame.data[0], contentSize, "Invalid CAFF file. Could not read CIFF content.");

    frameConsumer(frame);
}

void CaffParser::readBlock(FILE *file, uint64_t &remainingFrames) const {
    uint8_t blockType;
    uint64_t length;
    readData(file, &blockType, 1, "Invalid CAFF file. Could not read block ID.");
    readData(file, &length, 1, "Invalid CAFF file. Could not read block length.");
    switch (blockType) {
        case 1:
            readHeader(file, remainingFrames, length);
            break;
        case 2:
            readCredits(file, length);
            break;
        case 3:
            readFrame(file, remainingFrames, length);
            break;
        default:
            throw std::runtime_error{"Invalid CAFF file. Unknown block ID."};
    }
}

void CaffParser::parse(FILE *file) const {
    uint64_t remainingFrames = 0;
    do {
        readBlock(file, remainingFrames);
    } while(!feof(file) && remainingFrames > 0);
}
