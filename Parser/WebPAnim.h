#ifndef WEBP_CREATOR
#define WEBP_CREATOR

#include <cstdio>

#include <webp/encode.h>
#include <webp/mux.h>

class WebPAnim{
    WebPConfig * config;
    WebPMux *mux;

    int createFrame(uint8_t * pixels, int width, int height, int duration, WebPMuxFrameInfo * frame);

    public:
        WebPAnim();
        ~WebPAnim();

        int addFrame(uint8_t * pixels, int width, int height, int duration);
        int generate(FILE * outputFile);
};

#endif