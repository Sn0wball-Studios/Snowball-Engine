#include "ui.h"
#include "asset.h"
#define CUTE_C2_IMPLEMENTATION
#include "cute/cute_c2.h"


extern SDL_Renderer* renderer;
extern SDL_Color window_color;

#include "sound.h"
Mix_Chunk* UI_hoversound, UI_clickSound;

void ui_init()
{
	UI_hoversound = sound_getSound("assets/audio/ui/rollover6.ogg");
}

bool uiPreviousMouseState = false;
bool uiPreviousHoverState = false;

extern listitem_t* asset_root;
TTF_Font* ui_load_font(const char* filename, int size)
{
	
	// return asset_getasset(filename)->data;
	return TTF_OpenFont(filename, size);
}

extern int game_width;


void ui_text(TTF_Font* font, const char* text, float x, float y, SDL_Color text_color)
{
	SDL_Surface* surface = TTF_RenderText_Blended_Wrapped(font, text, text_color, game_width);
	SDL_Texture* texture = SDL_CreateTextureFromSurface(renderer, surface);
	SDL_FreeSurface(surface);

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
#include "linkedlist.h"
size_t asset_size;

extern listitem_t* asset_root;
void ui_drawDebugInfo()
{
	listitem_t* item = asset_root;

	while(item)
	{
		asset_size += item->size;
		item = item->next;
	}

	char memSizeBuff[64];

	// printf(&memSizeBuff, "asset memory usage: %s\n")
}


bool ui_button(TTF_Font* font, const char* text, int x, int y, SDL_Color text_color, SDL_Color box_color)
{
	SDL_Surface* surface = TTF_RenderText_Blended_Wrapped(font, text, text_color, game_width);
	SDL_Texture* texture = SDL_CreateTextureFromSurface(renderer, surface);
	SDL_FreeSurface(surface);

	int w, h;
	SDL_QueryTexture(texture, NULL, NULL, &w, &h);
	SDL_Rect dest = 
	{
		.x = x-16-(w/2)+8,
		.y = y-16-(h/2)+8,
		.w = w,
		.h = h
	};
	int mx, my;
	window_get_mouse(&mx, &my);

	// window_set_draw_color(255, 255, 255);
	// window_draw_box(mx-4, my-4, 8, 8);

	c2AABB box = 
	{
		.min = (c2v){.x = dest.x, .y = dest.y},
		.max = (c2v){.x = dest.x + w, .y = dest.y + h}
	};

	
	bool mouseState = window_leftClick();

	bool touching = c2AABBtoPoint(box, c2V(mx, my));

	
	int paddingY = TTF_FontHeight(font) / 8;
	int paddingX = (w / strlen(text)) / 2;
	

	if(touching)
	{
		paddingY *= 1.5;
		paddingX *= 1.5;

		if(!uiPreviousHoverState)
		{
			sound_play(UI_hoversound);
		}
	}

	uiPreviousHoverState = touching;

	window_set_draw_color(box_color.r, box_color.g, box_color.b);

	window_draw_box((dest.x)-(paddingX), (dest.y)-(paddingY), w+paddingX*2, h+paddingY*2);

	SDL_RenderCopy(renderer, texture, NULL, &dest);

	SDL_DestroyTexture(texture);


	bool clicked = false;

	if(!mouseState && uiPreviousMouseState)
	{
		clicked = touching;
	}
	else if(mouseState && !uiPreviousMouseState)
	{
		//click sound here
	}

	uiPreviousMouseState = mouseState;
	return clicked;
}