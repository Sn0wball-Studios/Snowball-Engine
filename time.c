#include "time.h"
#include <SDL2/SDL.h>

Uint64 previous = 0;

void time_updateTime()
{
	Uint64 now = SDL_GetTicks();

	Uint64 dt = now - previous;
	previous = now;
	deltaTime = (float)((dt) / 1000.0f);
}