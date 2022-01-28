#include "window.h"
#include <stdio.h>
#include <SDL2/SDL.h>
#include "asset.h"

SDL_Window* window = NULL;
SDL_Renderer* renderer = NULL;

float logical_scale = 1;
int game_width, game_height;
int window_width, window_height;
const char* title = "game";
bool open = true;

bool window_is_open()
{
	return open;
}

void HandleWindowEvent(SDL_Event event)
{
    switch (event.window.event)
    {
    case SDL_WINDOWEVENT_CLOSE:
        open = false;
        break;
    
    default:
        break;
    }
}

bool mouseButtons[5];

int mouseX, mouseY;
void window_poll()
{
	SDL_Event event;

    while(SDL_PollEvent(&event))
    {
        switch (event.type)
        {
        case SDL_WINDOWEVENT:
            HandleWindowEvent(event);
            break;
        case SDL_MOUSEMOTION:   
            mouseX = event.motion.x;
            mouseY = event.motion.y;
            break;
        case SDL_MOUSEBUTTONDOWN:
            mouseButtons[event.button.button] = true;
            break;
        case SDL_MOUSEBUTTONUP:
            mouseButtons[event.button.button] = false;
            break;

        
        default:
            break;
        }
    }
}

void* myLogFn(void* userdata, int category, SDL_LogPriority priority, const char* message)
{
    // Change to fit your needs (like to output to a file)
    printf("%s\n", message);

    FILE* file = fopen("engine.log", "a");

    fwrite(message, strlen(message), 1, file);
    fclose(file);
}

SDL_Texture* window_getSprite(const char* name)
{
    //todo: get json file
    int len = strlen(name);
    char* jsonName = malloc(len + 8);

    sprintf(jsonName, "%s.json", name);

    SDL_Log("loaded sprite json %s\n", jsonName);
    free(jsonName);
    return asset_getasset(name)->data;
}

void window_draw_sprite(SDL_Texture* texture, float x, float y, float angle)
{
    int w, h;
	SDL_QueryTexture(texture, NULL, NULL, &w, &h);
	SDL_FRect dest = 
	{
		.x = x,
		.y = y,
		.w = w,
		.h = h
	};


	SDL_RenderCopyF(renderer, texture, NULL, &dest);
}

void window_set_size(int w, int h)
{
	game_width = window_width = w;
	game_height = window_height = h;

	if(!window)
	{
        SDL_Init(SDL_INIT_EVERYTHING);

        SDL_LogSetOutputFunction(myLogFn, NULL);
		SDL_DisplayMode mode;
		if (SDL_GetDesktopDisplayMode(0, &mode) != 0)
        {
            SDL_Log("SDL_GetDesktopDisplayMode failed: %s", SDL_GetError());
            return 1;
        }
		if(window_width < mode.w / 2)
		{
			window_width = mode.w / 2; 
            window_height = mode.h / 2; 
		}


        logical_scale = window_width / game_width;

		window = SDL_CreateWindow(title, SDL_WINDOWPOS_CENTERED, SDL_WINDOWPOS_CENTERED, window_width, window_height, SDL_WINDOW_RESIZABLE);
		renderer = SDL_CreateRenderer(window, -1, SDL_RENDERER_ACCELERATED | SDL_RENDERER_PRESENTVSYNC);
        SDL_RenderSetLogicalSize(renderer, game_width, game_height);
		
        
        SDL_Log("set game logical resolution to {%d, %d}, window resolution to {%d, %d}\n", game_width, game_height, window_width, window_height);
	}

}

void window_draw_circle(float x, float y, float radius)
{
    for (int w = 0; w < radius * 2; w++)
    {
        for (int h = 0; h < radius * 2; h++)
        {
            int dx = radius - w; // horizontal offset
            int dy = radius - h; // vertical offset
            if ((dx*dx + dy*dy) <= (radius * radius))
            {
                SDL_RenderDrawPoint(renderer, x + dx, y + dy);
            }
        }
    }
//    const float diameter = (radius * 2);

//    float x = (radius - 1);
//    float y = 0;
//    float tx = 1;
//    float ty = 1;
//    float error = (tx - diameter);

//    while (x >= y)
//    {
//       //  Each of the following renders an octant of the circle
//       SDL_RenderDrawPointF(renderer, centreX + x, centreY - y);
//       SDL_RenderDrawPointF(renderer, centreX + x, centreY + y);
//       SDL_RenderDrawPointF(renderer, centreX - x, centreY - y);
//       SDL_RenderDrawPointF(renderer, centreX - x, centreY + y);
//       SDL_RenderDrawPointF(renderer, centreX + y, centreY - x);
//       SDL_RenderDrawPointF(renderer, centreX + y, centreY + x);
//       SDL_RenderDrawPointF(renderer, centreX - y, centreY - x);
//       SDL_RenderDrawPointF(renderer, centreX - y, centreY + x);

//       if (error <= 0)
//       {
//          ++y;
//          error += ty;
//          ty += 2;
//       }

//       if (error > 0)
//       {
//          --x;
//          tx += 2;
//          error += (tx - diameter);
//       }
//    }
}


void window_set_draw_color(uint8_t R, uint8_t G, uint8_t B)
{
    if(SDL_SetRenderDrawColor(renderer, R, G, B, 255) < 0)
    {
        SDL_Log(SDL_GetError());
    }

    window_color = (SDL_Color)
    {
        .r = R,
        .g = G,
        .b = B,
        .a = 255
    };
}

void window_get_mouse(int* x, int* y)
{
    *x = mouseX;
    *y = mouseY;
}

void window_draw_box(float x, float y, int w, int h)
{
    SDL_FRect box = 
    {
        .x = x,
        .y = y,
        .w = w,
        .h = h
    };

    SDL_RenderFillRectF(renderer, &box);
}

void window_close()
{
    SDL_DestroyWindow(window);
    SDL_DestroyRenderer(renderer);
}

void window_set_title(const char* title)
{
	printf("set window title to %s\n", title);
    SDL_SetWindowTitle(window, title);
}

void window_present()
{
    SDL_RenderPresent(renderer);
    window_set_draw_color(0,0,0);
    SDL_RenderClear(renderer);
}

#include <SDL2/SDL_ttf.h>

void window_init()
{
	TTF_Init();
}

bool window_leftClick()
{
    return mouseButtons[SDL_BUTTON_LEFT];
}