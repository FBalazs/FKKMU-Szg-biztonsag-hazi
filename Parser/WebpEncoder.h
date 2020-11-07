#ifndef WEBPENCODER_H
#define WEBPENCODER_H

#include <cstdio>

#include <webp/encode.h>
#include <webp/mux.h>

class WebpEncoder {

    WebPMux *mux;

public:
    WebpEncoder() : mux{WebPMuxNew()} {}

    ~WebpEncoder() {
        WebPMuxDelete(mux);
    }

    void init();

    void addFrame(int duration, int width, int height, const uint8_t *data);

    void writeToFile(FILE *file);
};

#endif //WEBPENCODER_H