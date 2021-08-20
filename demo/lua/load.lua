--load.lua created on 8/15/2021 5:30:34 PM using Sn0wballEngine V6.0
function start()
	-- CreateScriptableObject("scientist.lua", Vec2(100, 100)).id = "Jeff"
	-- CreateScriptableObject("scientist.lua", Vec2(200, 100)).id = "Dr Shitballs"
	-- CreateScriptableObject("player.lua", Vec2(50,50))
	-- CreateScriptableObject("sequence_demolevel_scientist.lua", Vec2(0,0))
	guard = Create("guard.lua", Vec2(50,50))

	print(guard.name)
	print(guard.position.x)
end


start()