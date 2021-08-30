--load.lua created on 8/15/2021 5:30:34 PM using Sn0wballEngine V6.0
scientist = require("scientist")
player = require("player")


bytes = ReadBinaryFile("PLAYPAL.pal")
sprite = CreateSpriteFromBuffer(bytes, 16,16, "doom")

function start()

	for i  = 0,500 do 
		CreateObject(scientist:create(Vec2(32 + i * 32, i * 16)))
	end

	player = CreateObject(player:create(Vec2(0, 0)))
	
end

function update()  
	UIDrawText("FPS:".. math.floor(1/dt), "demoFont", Vec2(0,0), OriginType.topLeft)
	UIDrawText("memory:".. math.floor(DebugGetMemoryUsage()/1000/1000).."MB", "demoFont", Vec2(0, 20), OriginType.topLeft)
	--UIDrawText("camera: ".. player.position.ToString(), "demoFont", Vec2(0, 60), OriginType.topLeft)

	DrawSprite(sprite)
end

