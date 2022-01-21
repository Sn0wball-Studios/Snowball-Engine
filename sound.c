#include "sound.h"
#include <stdbool.h>
#include "asset.h"
#define MAX_SOUNDCHANNELS 32
bool soundChannels[MAX_SOUNDCHANNELS] = {0};

void sound_ChannelFinishCallback(int channel)
{
    soundChannels[channel] = false;
}

void sound_init()
{
    Mix_ChannelFinished(&sound_ChannelFinishCallback);

    // Set up the audio stream
    int result = Mix_OpenAudio(44100, AUDIO_S16SYS, 2, 512);
    if( result < 0 )
    {
        fprintf(stderr, "Unable to open audio: %s\n", SDL_GetError());
        exit(-1);
    }

    result = Mix_AllocateChannels(MAX_SOUNDCHANNELS);
    if( result < 0 )
    {
        fprintf(stderr, "Unable to allocate mixing channels: %s\n", SDL_GetError());
        exit(-1);
    }
}

Mix_Chunk* sound_getSound(const char* name)
{
    return asset_getasset(name)->data;
}

int sound_getChannel()
{
    for(int i = 0; i < MAX_SOUNDCHANNELS; i++)
    {
        if(!soundChannels[i])
        {
            return i;
        }
    }

    return -1;
}

void sound_play(Mix_Chunk* sound)
{
    int channel = sound_getChannel();
    soundChannels[channel] = true;
    if(!Mix_PlayChannel(channel, sound, 0))
    {
    }
}