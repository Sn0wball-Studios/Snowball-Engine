#ifndef __ENGINE_ASSET__
#define __ENGINE_ASSET__
#include "linkedlist.h"


#define ASSET_TYPECOUNT 4

typedef enum asset_type_t
{
    ASSET_MP3,
    ASSET_WAV,
    ASSET_PNG,
    ASSET_TTF,

    ASSET_TEXT,
}asset_type_t;



typedef struct asset_t
{
    int type;
    const char* path;
    
    void* data;
    size_t size;

    int tag;
}asset_t;

size_t asset_getMemorySize();

asset_t* asset_getasset(const char* name);

void asset_free(asset_t* asset);

//free all assets with specified tag
void asset_freeTag(int tag);

void asset_loadAssets();

#endif