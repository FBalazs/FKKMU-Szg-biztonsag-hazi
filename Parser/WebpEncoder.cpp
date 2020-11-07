#include "WebpEncoder.h"

#include <stdexcept>

void WebpEncoder::init() {
    WebPMuxAnimParams animParams;
    animParams.bgcolor = 0;
    animParams.loop_count = 0;
    if (WebPMuxSetAnimationParams(mux, &animParams) != WEBP_MUX_OK) {
        throw std::runtime_error{"WebPMux error"};
    }
    if (WebPMuxSetCanvasSize(mux, 0, 0) != WEBP_MUX_OK) {
        throw std::runtime_error{"WebPMux error"};
    }
}

void WebpEncoder::addFrame(int duration, int width, int height, const uint8_t *data) {
    WebPConfig config;
    if (!WebPConfigInit(&config)) {
        throw std::runtime_error{"WebPConfig error"};
    }
    if (!WebPConfigLosslessPreset(&config, 6)) {
        throw std::runtime_error{"WebPConfig error"};
    }
    if (!WebPValidateConfig(&config)) {
        throw std::runtime_error{"WebPConfig error"};
    }

    WebPMuxFrameInfo frameInfo;
    frameInfo.x_offset = 0;
    frameInfo.y_offset = 0;
    frameInfo.duration = duration;
    frameInfo.id = WEBP_CHUNK_ANMF;
    frameInfo.dispose_method = WEBP_MUX_DISPOSE_NONE;
    frameInfo.blend_method = WEBP_MUX_NO_BLEND;

    uint8_t *webpBytes;
    frameInfo.bitstream.size = WebPEncodeLosslessRGB(data, width, height, width*3, &webpBytes);
    frameInfo.bitstream.bytes = webpBytes;

    if (WebPMuxPushFrame(mux, &frameInfo, 0) != WEBP_MUX_OK) {
        throw std::runtime_error{"WebPMux error"};
    }
}

void WebpEncoder::writeToFile(FILE *file) {
    WebPData webpData;
    if (!WebPMuxAssemble(mux, &webpData)) {
        throw std::runtime_error{"WebPMux error"};
    }
    fwrite(webpData.bytes, sizeof(uint8_t), webpData.size, file);
    WebPDataClear(&webpData);
}
