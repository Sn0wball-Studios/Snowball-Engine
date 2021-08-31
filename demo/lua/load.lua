--load.lua created on 8/15/2021 5:30:34 PM using Sn0wballEngine V6.0
scientist = require("scientist")
player = require("player")


bytes = ReadBinaryFile("Untitled.data")
sprite = CreateSpriteFromBuffer(bytes, 128, 128, "doom")

function start()

	for i  = 0,10 do 
		ent = CreateObject(scientist:create(Vec2(32 + i * 32, i * 16)))
		ent.speed = (math.sin(i)) * i
	end

	player = CreateObject(player:create(Vec2(0, 0)))
	
end

function update()  
	UIDrawText("FPS:".. math.floor(1/dt), "demoFont", Vec2(0,0), OriginType.topLeft)
	UIDrawText("memory:".. math.floor(DebugGetMemoryUsage()/1000/1000).."MB", "demoFont", Vec2(0, 20), OriginType.topLeft)
	--UIDrawText("camera: ".. player.position.ToString(), "demoFont", Vec2(0, 60), OriginType.topLeft)

	DrawSprite(sprite)
end

