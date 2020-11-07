#ifndef CAFFENCODER_H
#define CAFFENCODER_H

#include <cstdint>
#include <vector>
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

class CaffEncoder {

    template<typename T>
    static void writeData(FILE *file, const T *pointer, size_t count);

public:
    static void writeHeader(FILE *file, const CaffHeader& header);
    static void writeCredits(FILE *file, const CaffCredits& credits);
    static void writeFrame(FILE *file, const CaffFrame& frame);
};


#endif //CAFFENCODER_H
