#include "WebPAnim.h"
#include <stdexcept>

WebPAnim::WebPAnim(){
    //Config setup
    this->config = new WebPConfig();
    if (!WebPConfigPreset(this->config, WEBP_PRESET_PHOTO, 50.0))
        std::runtime_error("Invalid WebP config settings");

    // this->config->lossless = 1;

    this->mux = WebPMuxNew();
}

WebPAnim::~WebPAnim(){
    delete this->config;
    WebPMuxDelete(this->mux);
}

int WebPAnim::addFrame(uint8_t *pixels, int width, int height, int duration){
    WebPMuxFrameInfo frame;
    this->createFrame(pixels,width,height,duration, &frame);
    return WebPMuxPushFrame(mux, &frame, 0);
}


int WebPAnim::createFrame(uint8_t * pixels, int width, int height, int duration, WebPMuxFrameInfo * frame){
    
    WebPPicture pic;
    if (!WebPPictureInit(&pic)) return 0; 
     
    pic.width = width;
    pic.height = height;
    
    WebPPictureImportRGB(&pic, pixels, width * 3);
    
    WebPMemoryWriter writer;
    WebPMemoryWriterInit(&writer);
    pic.writer = WebPMemoryWrite;
    pic.custom_ptr = &writer;

    int ok = WebPEncode(this->config, &pic);

    WebPPictureFree(&pic);  

    if (!ok)
        printf("Encoding error: %d\n", pic.error_code);
    else 
        printf("Frame size: %d\n", writer.size);
        
    WebPDataInit(&frame->bitstream);

    frame->bitstream.bytes = writer.mem;
    frame->bitstream.size = writer.size;

    frame->x_offset = 0;
    frame->y_offset = 0;
    frame->duration = duration;
    frame->id = WEBP_CHUNK_ANMF;
    frame->dispose_method = WEBP_MUX_DISPOSE_NONE;
    frame->blend_method = WEBP_MUX_BLEND;

    return 1;
}

int WebPAnim::generate(FILE * outputFile){
    WebPData output_data;
    WebPDataInit(&output_data);

    WebPMuxAnimParams params = {0xFFFFFFFF, 0}; //background , looping

    WebPMuxSetAnimationParams(this->mux, &params);
    WebPMuxAssemble(this->mux, &output_data);

    printf("Anim size: %d", output_data.size);

    //Save output
    fwrite(output_data.bytes, 1, output_data.size, outputFile);

    WebPDataClear(&output_data);

    return 1;
}