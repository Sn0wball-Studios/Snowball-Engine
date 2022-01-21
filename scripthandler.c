#include "scripthandler.h"
#include "snow_file_io.h"
#include <SDL2/SDL.h>
#include <squirrel3/sqstdmath.h>
#include <squirrel3/sqstdaux.h>
#include <squirrel3/sqstdblob.h>
#include <squirrel3/sqstdio.h>
#include<squirrel3/sqstdstring.h>
#include <stdbool.h>

#define ENGINESCRIPTCOUNT 3
const char* engineScripts[ENGINESCRIPTCOUNT] = 
{
	"engine_scripts/import.nut",
	"engine_scripts/vec2.nut",
	"engine_scripts/entity.nut",
	
};

void script_addEngineScripts(script_t* script)
{
	for(int i = 0; i < ENGINESCRIPTCOUNT; i++)
	{
		script_compile(script, engineScripts[i]);
		script_start(script);
	}
}

script_t* script_create(size_t stackSize)
{
	int scriptLength;
		
	script_t* script = malloc(sizeof(script_t));

	script->vm = sq_open(stackSize);
	sq_seterrorhandler(script->vm);

	sq_pushroottable(script->vm);
	sqstd_register_bloblib(script->vm);
	sqstd_register_iolib(script->vm);
	sqstd_register_stringlib(script->vm);
	if(SQ_FAILED(sqstd_register_mathlib(script->vm)))
	{
		SDL_Log("failed to register math library\n");
	}
	script_addEngineScripts(script);
	return script;
}

void script_invoke(script_t* script, const char* funcName)
{
	sq_pushroottable(script->vm);
	sq_pushstring(script->vm,funcName,-1);
	sq_get(script->vm,-2); //get the function from the root table
	sq_pushroottable(script->vm); //'this' (function environment object)
	
	if(SQ_FAILED(sq_call(script->vm,1,SQFalse, SQTrue)))
	{
		SDL_Log("failed to invoke function %s\n", funcName);
	}
	sq_pop(script->vm,1); //pops the roottable and the function

}

SQInteger file_lexfeedASCII(SQUserPointer file)
{
    int ret;
    char c;
    if( ( ret=fread(&c,sizeof(c),1,(FILE *)file )>0) )
        return c;
    return 0;
}

void squirrel_compilerError(HSQUIRRELVM vm, const SQChar* desc, const SQChar* source, 
                             SQInteger line, SQInteger column)
{
    // tic_core* core = getSquirrelCore(vm);
    // char buffer[1024];
    printf("you fucked up!\n");
    
    // if (core->data)
    //     core->data->error(core->data->data, buffer);
}

#include "squirrel_nativefuncs.h"

SQInteger register_global_func(script_t* script,SQFUNCTION f,const char *fname)
{
    sq_pushroottable(script->vm);
    sq_pushstring(script->vm,fname,-1);
    sq_newclosure(script->vm,f,0); //create a new function
    sq_newslot(script->vm,-3,SQFalse);
    sq_pop(script->vm,1); //pops the root table
}

SQInteger createClassInstance(HSQUIRRELVM v)
{
    HSQOBJECT instance;
    sq_resetobject(&instance);
    sq_getstackobj(v, -1, &instance);

    // T *p = new T(instance);

    // sq_setinstanceup(v, -1, p);
    // sq_setreleasehook(v, -1, deleteClassInstance<T>);

    return 0;
}



void registerClass(HSQUIRRELVM v, const char *name)
{
    sq_pushroottable(v);

    sq_pushstring(v, name, -1);
    sq_newclass(v, false);

	// register_global_func

    // sq_pushstring(v, _SC("constructor"), -1);
    // sq_newclosure(v, createClassInstance<T>, 0);
    // sq_newslot(v, -3, false); // Add the constructor method

    // sq_newslot(v, -3, SQFalse); // Add the class

    sq_pop(v, 2);
}

void push_funcs(script_t* script)
{
	register_global_func(script, print_args, "sprint");
	register_global_func(script, sqBinding_deltaTime, "deltatime");
	register_global_func(script, sqBinding_invoke_inSeconds, "invokeInSeconds");

	register_global_func(script, sqBinding_window_set_title, "window_set_title");
	register_global_func(script, sqBinding_window_set_size, "window_set_size");
	register_global_func(script, sqBinding_window_set_draw_color, "window_set_draw_color");
	register_global_func(script, sqBinding_window_get_mouse, "window_get_mouse");
	register_global_func(script, sqBinding_window_draw_sprite, "window_draw_sprite");
	register_global_func(script, sqBinding_window_get_sprite, "window_get_sprite");
	
	//UI FUNCS
	register_global_func(script, sqBinding_ui_load_font, "ui_load_font");
	register_global_func(script, sqBinding_ui_button, "ui_button");
	register_global_func(script, sqBinding_ui_text, "ui_text");

	register_global_func(script, sqBinding_scene_load, "scene_load");

	register_global_func(script, sqBinding_sound_get_sound, "sound_load");
	register_global_func(script, sqBinding_sound_play_sound, "sound_play");

	register_global_func(script, sqBinding_color, "color");
}

void script_start(script_t* script)
{
 SQInteger oldtop = sq_gettop(script->vm);

  sq_pushroottable(script->vm);
	if (SQ_FAILED(sq_call(script->vm, 1, SQFalse, SQTrue)))
      SDL_Log("runtime error encountered\n");
}
	
void script_compile(script_t* script, const char* filename)
{
	sq_setcompilererrorhandler(script->vm, squirrel_compilerError);
	
	push_funcs(script);

	FILE *f = fopen(filename,"rb");
	if(!SQ_FAILED(sq_compile(script->vm,file_lexfeedASCII,f,filename,1)))
	{
		SDL_Log("compiled script %s\n", filename);
	}else
	{
		SDL_Log("failed to compile!!\n");
	}
	fclose(f);
}