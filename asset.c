#include "declares.h"
#include "asset.h"
#include "linkedlist.h"
#include "cute/cute_files.h"
#include <SDL2/SDL_image.h>


listitem_t* asset_root = NULL;

const char* asset_type_extensions[ASSET_TYPECOUNT+1] = 
{
    ".ogg",
    ".wav",
    ".png",
    ".ttf"

    "binary/text file"
};


void asset_free(asset_t* asset);

//free all assets with specified tag
void asset_freeTag(int tag);

void readDir(const char* name)
{
    cf_dir_t dir;
	cf_dir_open(&dir, name);
    while (dir.has_next)
	{
	    cf_file_t file;
		cf_read_file(&dir, &file);

        if(file.name[0] != '.')
        {
            if(file.is_dir)
            {
                char dirname[256];
                sprintf(&dirname, "%s/%s", name, file.name);

                readDir(dirname);
            }else
            {
                char* filename = malloc(512);
                sprintf(filename, "%s/%s", name, file.name);
    
                FILE* reader = fopen(filename, "rb");

                void* data = malloc(file.size);

                fread(data, file.size, 1, reader);
                fclose(reader);

                asset_t* asset = malloc(sizeof(*asset));
                asset->data = data;
                asset->path = filename;
                asset->size = file.size;
                asset->type = -1;
                asset->tag = 0;
                //determine asset type


                for(int i = 0; i < ASSET_TYPECOUNT; i++)
                {
                    if(strcmp(asset_type_extensions[i], file.ext) == 0)
                    {
                        asset->type = i;
                    }

                    if(i == ASSET_TYPECOUNT-1 && asset->type == -1)
                    {
                        asset->type = ASSET_TEXT;
                    }

                }

                linkedlist_insert(&asset_root, asset, sizeof(asset_t));
            }
        }
		cf_dir_next(&dir);
	}

    cf_dir_close(&dir);
}

asset_t* asset_getasset(const char* name)
{
    listitem_t* item = asset_root;
    while(item)
    {
        asset_t* asset = item->data;
        if(strcmp(asset->path, name) == 0)
        {
            return asset;
        }
        item = item->next;
    }
    sprintf("asset_get: failed to find asset %s\n", name);
    return NULL;
}

void asset_printAssetListItem(listitem_t* item)
{
    asset_t* asset = item->data;
    printf("asset: {length: %u, path: %s, type: %s, tag: %d}\n", asset->size, asset->path, asset_type_extensions[asset->type], asset->tag);
}

extern SDL_Renderer* renderer;

size_t memSize  = 0;

void asset_PNG(asset_t* asset)
{
    SDL_RWops* data = SDL_RWFromMem(asset->data, asset->size);
    if(!data)
    {
        printf("failed to read asset data!\n");
        exit(EXIT_FAILURE);
    }

    SDL_Surface* surface = IMG_LoadPNG_RW(data);

    SDL_Texture* texture = SDL_CreateTextureFromSurface(renderer, surface);
    
    SDL_FreeSurface(surface);
    SDL_FreeRW(data);
    free(asset->data);

    asset->data = texture;

    int w, h;

    SDL_QueryTexture(texture, NULL, NULL, &w, &h);

    printf("asset: {filename: %s, width: %d, height: %d}\n", asset->path,w, h);
}

#include <SDL2/SDL_mixer.h>

void asset_Sound(asset_t* asset)
{
    SDL_RWops* data = SDL_RWFromMem(asset->data, asset->size);
    if(!data)
    {
        printf("failed to read asset data!\n");
        exit(EXIT_FAILURE);
    }
    
    Mix_Chunk* chunk = Mix_LoadWAV_RW(data, 1);

    asset->data = chunk;
    
    memSize += chunk->alen;
    if(!asset->data)
    {
        printf("failed to load sound %s (%s)", asset->path, SDL_GetError());
    }

    printf("sound asset: {%s}\n", asset->path);
}


size_t asset_getMemorySize()
{

}

void asset_TTF(asset_t* asset)
{

}

//conver the asset into a usable item(texture, sound, script, etc)
void asset_readAsset(listitem_t* item)
{
    asset_t* asset = item->data;
    switch (asset->type)
    {
    case ASSET_PNG:
        asset_PNG(asset);
        break;
    case ASSET_MP3:
        asset_Sound(asset);
        break;
    case ASSET_TTF:
        asset_TTF(asset);
        break;
    
    default:
        break;
    }

    memSize += asset->size;
}

void asset_loadAssets()
{
    readDir("assets");
    linkedlist_foreach(asset_root, &asset_readAsset);

    printf("asset mem size(bytes) %u\n", memSize);
}