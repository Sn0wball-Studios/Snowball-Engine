#include "squirrel_nativefuncs.h"
#include <stdio.h>
#include <squirrel3/squirrel.h>
#include "window.h"
#include "ui.h"
#include "time.h"

/*
    remember that actual args start at index 2 for some fucking reason
*/
#include "linkedlist.h"
#include "sound.h"
#include "scripthandler.h"
#include "asset.h"
SQInteger sqBinding_get_asset_memory(HSQUIRRELVM v)
{
    SQInteger size = asset_getMemorySize();

    sq_pushinteger(v, size);


    return size;
}

SQInteger sqBinding_scene_load(HSQUIRRELVM v)
{
    const char* name;
    sq_getstring(v, 2, &name);

    script_compile(root_script, name);

    script_start(root_script);
}


typedef struct functionInvokeTimer_t
{
    const char* functionName;
    float time, timer;
}functionInvokeTimer_t;

listitem_t* functionTimers_root;

SQInteger sqBinding_import(HSQUIRRELVM v)
{
    const char* name;

    sq_getstring(v, 2, &name);

    char* filename = malloc(strlen(name) + 22);

    sprintf(filename, "assets/scripts/%s.nut", name);

    sq_pushroottable(v);
    
}


void updateTimer(listitem_t* item)
{
    functionInvokeTimer_t* timer = item->data;
    timer->timer += deltaTime;
    
    if(timer->timer > timer->time)
    {
        script_invoke(root_script, timer->functionName);
        linkedlist_remove(&functionTimers_root, item);
    }

}

void squirrel_updateFunctionTimers()
{
    linkedlist_foreach(functionTimers_root, &updateTimer);
}


SQInteger sqBinding_invoke_inSeconds(HSQUIRRELVM v)
{
    functionInvokeTimer_t* instance = malloc(sizeof(*instance));
    
    sq_getstring(v, 2, &instance->functionName);
    sq_getfloat(v, 3, &instance->time);

    instance->timer = 0;

    linkedlist_insert(&functionTimers_root, instance, sizeof(*instance));


}

SQInteger sqBinding_sound_get_sound(HSQUIRRELVM v)
{
    const char* name;
    sq_getstring(v, 2, &name);

    Mix_Chunk* sound = sound_getSound(name);

    sq_pushuserpointer(v, sound);
    return sound;
}

SQInteger sqBinding_sound_play_sound(HSQUIRRELVM v)
{
    Mix_Chunk* sound;
    sq_getuserpointer(v, 2, &sound);


    sound_play(sound);
}

SQInteger sqBinding_deltaTime(HSQUIRRELVM v)
{
    sq_pushfloat(v, deltaTime);
    return 1;
}

SQInteger sqBinding_color(HSQUIRRELVM v)
{
    SQInteger r, g, b;

    sq_getinteger(v, 2, &r);
    sq_getinteger(v, 3, &g);
    sq_getinteger(v, 4, &b);

    // SDL_Color color = 
    // {
    //     .r = r,
    //     .g = g,
    //     .b = b
    // };
    SDL_Color* color = malloc(sizeof(*color));
    color->r = r;
    color->g = g;
    color->b = b;
    color->a = 255;
    sq_pushuserpointer(v, color);
    return color;
}


SQInteger sqBinding_ui_load_font(HSQUIRRELVM v)
{
    const char* name;
    SQInteger size;

    sq_getstring(v, 2, &name);
    sq_getinteger(v, 3, &size);

    TTF_Font* font = ui_load_font(name, size);

    sq_pushuserpointer(v, font);
    return font;
    // sq_pushstring(v, "sex", 4);
}

SQInteger sqBinding_ui_text(HSQUIRRELVM v)
{
    TTF_Font* font;
    const char* text;
    float x, y;
    sq_getuserpointer(v, 2, &font);
    sq_getstring(v, 3, &text);
    sq_getfloat(v, 4, &x);
    sq_getfloat(v, 5, &y);

    SDL_Color* text_color;

    sq_getuserpointer(v, 6, &text_color);

    ui_text(font, text, x, y, *text_color);

}

SQInteger sqBinding_ui_button(HSQUIRRELVM v)
{
    TTF_Font* font;
    const char* text;
    SQInteger x, y;
    sq_getuserpointer(v, 2, &font);
    sq_getstring(v, 3, &text);
    sq_getinteger(v, 4, &x);
    sq_getinteger(v, 5, &y);

    struct SQClass* class;

    SDL_Color* text_color, *button_color;

    sq_getuserpointer(v, 6, &text_color);
    sq_getuserpointer(v, 7, &button_color);

    SQBool val = ui_button(font, text, x, y, *text_color, *button_color);
    sq_pushbool(v, val);
    return val;
}

SQInteger sqBinding_window_set_draw_color(HSQUIRRELVM v)
{
    SQInteger r, g, b;
    sq_getinteger(v, 2, &r);
    sq_getinteger(v, 3, &g);
    sq_getinteger(v, 4, &b);

    window_set_draw_color(r, g, b);

}

SQInteger sqBinding_window_set_size(HSQUIRRELVM v)
{
    printf("{invoked by squirrel} ");
    SQInteger w, h;
    sq_getinteger(v, 2, &w);
    sq_getinteger(v, 3, &h);

    window_set_size(w, h);
}


SQInteger sqBinding_window_get_sprite(HSQUIRRELVM v)
{
    const char* name;
    SDL_Texture* texture;
    sq_getstring(v, 2, &name);
    texture = window_getSprite(name);

    sq_pushuserpointer(v, texture);

    return texture;

}

SQInteger sqBinding_window_draw_sprite(HSQUIRRELVM v)
{
    SDL_Texture* texture;
    float x, y, angle;
    sq_getuserpointer(v, 2, &texture);
    sq_getfloat(v, 3, &x);
    sq_getfloat(v, 4, &y);
    sq_getfloat(v, 5, &angle);

    window_draw_sprite(texture, x, y, angle);
}

SQInteger sqBinding_window_set_title(HSQUIRRELVM v)
{
    // print_args(v);
    printf("{invoked by squirrel} ");
    const char* title;
    sq_getstring(v, 2, &title);
    window_set_title(title);
}

SQInteger print_args(HSQUIRRELVM v)
{
    SQInteger nargs = sq_gettop(v); //number of arguments
    for(SQInteger n=1;n<=nargs;n++)
    {
		void* value;
        float f;
        switch(sq_gettype(v,n))
        {
            case OT_NULL:
                printf("NULL");
                break;
            case OT_INTEGER:
				sq_getinteger(v, n, &value);
                printf("%d", value);
                break;
            case OT_FLOAT:
                //for some reason floats do not want to be casted to void* ¯\_(ツ)_/¯
                sq_getfloat(v, n, &f);
                printf("%f", f);
                break;
            case OT_STRING:
				sq_getstring(v, n, &value);
                printf("%s", value);
                break;
            case OT_TABLE:
                printf("table");
                break;
            case OT_ARRAY:
                printf("array");
                break;
            case OT_USERDATA:
                printf("userdata");
                break;
            case OT_CLOSURE:
                printf("closure(function)");
                break;
            case OT_NATIVECLOSURE:
                printf("native closure(C function)");
                break;
            case OT_GENERATOR:
                printf("generator");
                break;
            case OT_USERPOINTER:
                sq_getuserpointer(v, n, &value);
                printf("%p", value);
                break;
            case OT_CLASS:
                printf("class");
                break;
            case OT_INSTANCE:
                sq_getinstanceup(v, n, &value, 0);
                printf("cum %p", value);
                break;
            case OT_WEAKREF:
                printf("weak reference");
                break;
            case OT_BOOL:
                sq_getbool(v, n, &value);
                printf("%s", (int)(value) ? "true" :"false");
                break;
            default:
                return sq_throwerror(v,"invalid param"); //throws an exception
        }
    }
    printf("\n");
    sq_pushinteger(v,nargs); //push the number of arguments as return value
    return 1; //1 because 1 value is returned
}

SQInteger sqBinding_window_get_mouse(HSQUIRRELVM v)
{
    void* x;
    sq_newarray(v, 2);
    
    // sq_pop(v, 2);
    // sq_pushinteger(v, 420);
}