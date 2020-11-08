#ifndef CAFFPARSER_H
#define CAFFPARSER_H

#include <functional>
#include <vector>
#include <cstdint>
#include <string>

struct CaffHeader {
    uint64_t frameCount = 0;
};

struct CaffCredits {
    uint16_t creationYear = 0;
    uint8_t creationMonth = 0;
    uint8_t creationDay = 0;
    uint8_t creationHour = 0;
    uint8_t creationMinute = 0;

    std::string creatorName;
};

struct CaffFrame {
    uint64_t duration = 0;

    uint64_t width = 0, height = 0;
    std::string caption;
    std::vector<std::string> tags;

    std::vector<uint8_t> data;
};

class CaffParser {

    const std::function<void(const CaffHeader&)> headerConsumer;
    const std::function<void(const CaffCredits&)> creditsConsumer;
    const std::function<void(const CaffFrame&)> frameConsumer;

    template<typename T>
    void readData(FILE *file, T *pointer, size_t count, const char *errorText) const;

    void readHeader(FILE *file, uint64_t &remainingFrames, uint64_t length) const;

    void readCredits(FILE *file, uint64_t length) const;

    void readFrame(FILE *file, uint64_t &remainingFrames, uint64_t length) const;

    void readBlock(FILE *file, uint64_t &remainingFrames) const;

public:
    CaffParser(
            std::function<void(const CaffHeader&)> headerConsumer,
            std::function<void(const CaffCredits&)> creditsConsumer,
            std::function<void(const CaffFrame&)> frameConsumer) : headerConsumer{std::move(headerConsumer)}, creditsConsumer{std::move(creditsConsumer)}, frameConsumer{std::move(frameConsumer)} {}

    void parse(FILE *file) const;
};


#endif //CAFFPARSER_H
