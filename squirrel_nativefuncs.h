#ifndef __SNOWBALL_NATIVEFUNCS__
#define __SNOWBALL_NATIVEFUNCS__
#include <squirrel3/squirrel.h>

//engine functions that can be called from a squirrel script

void squirrel_updateFunctionTimers();

SQInteger sqBinding_invoke_inSeconds(HSQUIRRELVM v);
SQInteger sqBinding_import(HSQUIRRELVM v);

SQInteger sqBinding_get_asset_memory(HSQUIRRELVM v);


SQInteger print_args(HSQUIRRELVM v);

SQInteger sqBinding_window_set_title(HSQUIRRELVM v);
SQInteger sqBinding_window_set_size(HSQUIRRELVM v);
SQInteger sqBinding_window_set_draw_color(HSQUIRRELVM v);
SQInteger sqBinding_window_get_mouse(HSQUIRRELVM v);
SQInteger sqBinding_window_draw_sprite(HSQUIRRELVM v);
SQInteger sqBinding_window_get_sprite(HSQUIRRELVM v);

SQInteger sqBinding_sound_get_sound(HSQUIRRELVM v);
SQInteger sqBinding_sound_play_sound(HSQUIRRELVM v);
SQInteger sqBinding_scene_load(HSQUIRRELVM v);


SQInteger sqBinding_ui_load_font(HSQUIRRELVM v);
SQInteger sqBinding_ui_button(HSQUIRRELVM v);
SQInteger sqBinding_ui_text(HSQUIRRELVM v);

SQInteger sqBinding_deltaTime(HSQUIRRELVM v);


SQInteger sqBinding_color(HSQUIRRELVM v);
#endif