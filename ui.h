#ifndef __SNOWBALL_UI__
#define __SNOWBALL_UI__
#include "window.h"
#include <stdbool.h>
#include <SDL2/SDL_ttf.h>

typedef struct uiconfig_t
{
    
}uiconfig_t;

TTF_Font* ui_load_font(const char* filename, int size);
bool ui_button(TTF_Font* font, const char* text, int x, int y, SDL_Color text_color, SDL_Color box_color);
void ui_text(TTF_Font* font, const char* text, float x, float y, SDL_Color text_color);
void ui_init();
void ui_drawDebugInfo();

#endif