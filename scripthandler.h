#ifndef __SNOWBALL_SCRIPT__
#define __SNOWBALL_SCRIPT__
#include <squirrel3/squirrel.h>
#include <stdlib.h>

typedef struct script_t
{
	HSQUIRRELVM vm;
}script_t;

script_t* root_script;
script_t* script_create(size_t stackSize);
void script_compile(script_t* script, const char* source);
void script_start(script_t* script);

void script_invoke(script_t* script, const char* funcName);


#endif