--load.lua created on 8/15/2021 5:30:34 PM using Sn0wballEngine V6.0
scientist = require("scientist")
player = require("player")


count = 1

time = 0
function start()
	for i = 1, 200 do
		posy = i * 32
		posx = math.sin(posy) * 32*3

		CreateObject(scientist:create(Vec2(posx, posy)))

		

		count = count + 1
	end

	player = CreateObject(player:create(Vec2(0, 0)))
	
end

Color = color(0,0,0,255)


function update()  
	UIDrawText("FPS:".. math.floor(1/dt()), "demoFont", Vec2(0,0), OriginType.topLeft)
	UIDrawText("entities: ".. count, "demoFont", Vec2(0, 20), OriginType.topLeft)
	UIDrawText("memory:".. math.floor(DebugGetMemoryUsage()/1000/1000).."MB", "demoFont", Vec2(0, 40), OriginType.topLeft)
	--UIDrawText("camera: ".. player.position.ToString(), "demoFont", Vec2(0, 60), OriginType.topLeft)
	box = BoundingBox(Vec2(0,0))
	DrawBox(box, self.color)

end

