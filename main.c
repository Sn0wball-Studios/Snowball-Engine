// #include "snowscript.h"
#include "scripthandler.h"
#include "window.h"
#include <cjson/cJSON.h>
#include "time.h"
#include "snow_file_io.h"
#include "asset.h"
#include "sound.h"
#include "ui.h"
int main()
{
	window_init();
	sound_init();

	
	
	int len;
	const char* json_source = snow_file_read_all_text("assets/game.json", &len);
	cJSON* json = cJSON_Parse(json_source);

	const char* startScript = cJSON_GetObjectItem(json, "startScript")->valuestring;
	int windowWidth = cJSON_GetObjectItem(json, "windowWidth")->valueint;
	int windowHeight = cJSON_GetObjectItem(json, "windowHeight")->valueint;
	const char* windowName = cJSON_GetObjectItem(json, "gameName")->valuestring;

	int stackSize = cJSON_GetObjectItem(json, "stackSize")->valueint;

	window_set_size(windowWidth, windowHeight);
	window_set_title(windowName);

	asset_loadAssets();

	ui_init();
	

	root_script = script_create(stackSize);
	script_compile(root_script, startScript);
	script_start(root_script);
	while(window_is_open())
	{
		time_updateTime();
		window_poll();
		script_invoke(root_script, "update");
		squirrel_updateFunctionTimers();
		window_present();
	}

	window_close();
}

