#ifndef PARSER_PARSER_H
#define PARSER_PARSER_H

#include <cstdio>
#include <vector>

struct AnimationFrame {
    uint64_t duration = 0;

    uint64_t width = 0, height = 0;
    std::string caption;
    std::vector<std::string> tags;

    std::vector<uint8_t> data;

    AnimationFrame() = default;

    AnimationFrame(AnimationFrame &&frame) noexcept :
        duration(frame.duration),
        width(frame.width),
        height(frame.height),
        caption(std::move(frame.caption)),
        tags(std::move(frame.tags)),
        data(std::move(frame.data)) {}
};

struct Animation {
    uint16_t creationYear = 0;
    uint8_t creationMonth = 0;
    uint8_t creationDay = 0;
    uint8_t creationHour = 0;
    uint8_t creationMinute = 0;

    std::string creatorName;

    std::vector<AnimationFrame> frames;
};

class Parser {

    FILE *inputFile = nullptr;
    FILE *outputFile = nullptr;

    uint64_t remainingFrames = 0;

    template<typename T>
    void read(T *pointer, size_t count, const char *errorText);

    void readHeader(uint64_t length);

    void readCredits(uint64_t length);

    void readFrame(uint64_t length);

    void readBlock();

    void read();

    void write();

public:
    
    Animation animation;

    Parser(FILE *inputFile, FILE *outputFile) : inputFile(inputFile), outputFile(outputFile) {}

    void process();
};


#endif //PARSER_PARSER_H
