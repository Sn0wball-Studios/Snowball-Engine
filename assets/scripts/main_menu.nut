local font = ui_load_font("assets/Kenney Pixel.ttf", 12);
local logoFont = ui_load_font("assets/Kenney Future.ttf", 12);

local text_color = color(255, 255, 255);
local button_color = color(50,50, 50);


local springtrap = window_get_sprite("assets/springtrap/springtrap.png");
local eye = window_get_sprite("assets/springtrap/eye.png");

local flickerSfx = sound_load("assets/sfx/menu/menu_eyeflicker.ogg")

local flickerTimer = 0.0;
local flickerOnTimer = 0.0;
local flickerTime = 0.1;

local flickerTimes = 
[
	0.3,
	0.47
	1.3
];

local flickerIndex = 0;
local timeToPlay = 3;
local playTimer = 0.0;
local hasPlayed = false;

local eyeX = 171.0
local eyeY = 34.0



import("snowball/snowball")
import("player")

sprint(Player)

function update()
{	
	local memSize = 0.0 + debug_get_memory_usage();
	
	memSize = (memSize/1000.0) / 1000.0;


	window_draw_sprite(springtrap, 0, 0, 0);
	ui_text(font, format("AssetMem: %dMB", memSize.tointeger()), 0, 0, text_color)
	

	playTimer += deltatime();
	if(playTimer < timeToPlay)
	{
		return;
	}else
	{
		if(!hasPlayed)
		{
			sound_play(flickerSfx);
		}
		hasPlayed = true;
	}

	flickerTimer += deltatime();

	if(flickerIndex < flickerTimes.len())
	{
		if(flickerTimer > flickerTimes[flickerIndex])
		{
			flickerIndex++;
			flickerOnTimer = 0.0
		}
	}

	flickerOnTimer += deltatime();

	if(flickerOnTimer < flickerTime || flickerIndex == flickerTimes.len())
	{
		window_draw_sprite(eye, eyeX, eyeY, 0);
	}

	if(flickerIndex == flickerTimes.len())
	{
		if(ui_button(logoFont, "start", 160, 175, text_color, button_color))
		{
			scene_load("assets/scripts/game.nut");	
		}
	}

	
}

function fadeOut()
{

}