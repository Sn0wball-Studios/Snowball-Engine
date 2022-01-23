import("animatronic");

local clock = 0;
local clockChangeTime = 45;
local clockChangeTimer = 0.0;
local font = ui_load_font("assets/Kenney Pixel.ttf", 16);
local text_color = color(255, 255, 255);


local animatronicMoveTime = 3.0;
local animatronicMoveTimer = 0.0;

local timeText = 
[
    "12AM",
    "1AM",
    "2AM",
    "3AM",
    "4AM",
    "5AM",
    "6AM"
];

local defaultJumpScareSound = null

local animatronics =
[
    Animatronic(6, defaultJumpScareSound, "Freddy"), //feddy
    Animatronic(3, defaultJumpScareSound, "Bonnie"),
    Animatronic(4, defaultJumpScareSound, "Chica"),
    Animatronic(2, defaultJumpScareSound, "Foxie")
];

function updateAnimatronics()
{
    foreach (i, animatronic in animatronics)
    {
        animatronic.move();    
    }
}

function update()
{
    clockChangeTimer += deltatime();

    if(clockChangeTimer > clockChangeTime)
    {
        clockChangeTimer = 0.0;
        clock++;
        if(clock > timeText.len()){clock = timeText.len}
    }

    animatronicMoveTimer += deltatime();
    if(animatronicMoveTimer > animatronicMoveTime)
    {
        animatronicMoveTimer = 0;
        updateAnimatronics();
    }

    ui_text(font, timeText[clock], 0, 0, text_color)
}