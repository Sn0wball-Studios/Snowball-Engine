--load.lua created on 8/15/2021 5:30:34 PM using Sn0wballEngine V6.0
scientist = require("scientist")
player = require("player")

function start()


	for x = 0, 10 do
		for y = 0, 10 do
			ent = CreateObject(scientist:create(Vec2(x * 32 , y * 32)))
			ent.speed = (math.sin(x)) * 5
		end
	end

	player = CreateObject(player:create(Vec2(0, 0)))
end

function update()  
	UIDrawText("FPS:".. math.floor(1/dt), "demoFont", Vec2(0,0), OriginType.topLeft)
	UIDrawText("memory:".. math.floor(DebugGetMemoryUsage()/1000/1000).."MB", "demoFont", Vec2(0, 20), OriginType.topLeft)

	
	--UIDrawText("camera: ".. player.position.ToString(), "demoFont", Vec2(0, 60), OriginType.topLeft)

	--DrawSprite(sprite)
end

