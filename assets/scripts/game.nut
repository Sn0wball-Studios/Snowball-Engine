local font = ui_load_font("assets/Kenney Pixel.ttf", 16);
local text_color = color(255, 255, 255);

import("snowball/snowball")
// import("player")


// Entity.Create(Player);

local s = window_get_sprite("assets/springtrap/springtrap.png");
function update()
{
    // ui_text(font, "UwU", 0, 0, text_color);
    Entity.UpdateEntities();
}


// function update()
// {
//     clockChangeTimer += deltatime();

//     if(clockChangeTimer > clockChangeTime)
//     {
//         clockChangeTimer = 0.0;
//         clock++;
//         if(clock > timeText.len()){clock = timeText.len}
//     }

//     animatronicMoveTimer += deltatime();
//     if(animatronicMoveTimer > animatronicMoveTime)
//     {
//         animatronicMoveTimer = 0;
//         updateAnimatronics();
//     }

//     ui_text(font, timeText[clock], 0, 0, text_color)
// }