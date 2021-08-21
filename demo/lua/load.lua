--load.lua created on 8/15/2021 5:30:34 PM using Sn0wballEngine V6.0
scientist = require("scientist")
player = require("player")

function start()
	for i = 0, 1000 do
		posx = RandomRange(-300, 300)
		posy = RandomRange(-300, 300)
		CreateObject(scientist:create(Vec2(posx, posy)))
	end

	CreateObject(player:create(Vec2(100, 100)))
	
end


function update()  
	UIDrawText("FPS:".. math.floor(1/dt()), "demoFont", Vec2(0,0), OriginType.topLeft)
	UIDrawText("entities: 1000", "demoFont", Vec2(0, 20), OriginType.topLeft)
	UIDrawText("memory:".. math.floor(DebugGetMemoryUsage()/1000/1000).."MB", "demoFont", Vec2(0, 40), OriginType.topLeft)
end

