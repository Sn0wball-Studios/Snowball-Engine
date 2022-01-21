#ifndef __ENGINE_SOUND__
#define __ENGINE_SOUND__
#include <SDL2/SDL_mixer.h>
void sound_init();
void sound_play(Mix_Chunk* sound);
Mix_Chunk* sound_getSound(const char* name);
#endif