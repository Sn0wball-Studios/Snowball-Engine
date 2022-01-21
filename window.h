#ifndef __SNOWBALL_WINDOW__
#define __SNOWBALL_WINDOW__
#include <stdbool.h>
#include <stdint.h>
#include <SDL2/SDL.h>



SDL_Color window_color;
void window_set_title(const char* title);
void window_set_size(int w, int h);
void window_init();
void window_present();
bool window_is_open();
void window_poll();
void window_close();
void window_set_draw_color(uint8_t R, uint8_t G, uint8_t B);
void window_draw_circle(float centreX, float centreY, float radius);
void window_draw_box(float x, float y, int w, int h);
SDL_Texture* window_getSprite(const char* name);
void window_draw_sprite(SDL_Texture* texture, float x, float y, float angle);
void window_get_mouse(int* x, int* y);

bool window_leftClick();

#endif