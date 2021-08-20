--zombie.lua created on 8/15/2021 5:33:17 PM using Sn0wballEngine V6.0

--called on object creation
function start()
	position = Vec2(160, 90)
	AddSprite("guy.json")
	id = "player"
end

turnSpeed = 90
moveSpeed = 32
--called once per frame
function update()
	
	rotation = rotation + InputGetAxis("horizontal") * turnSpeed * deltaTime
	

	
	velocity = Vec2(math.cos(math.rad(rotation)), math.sin(math.rad(rotation)))

	if InputIsKeyDown(KeyboardKey.W) then 
		position = position + velocity * moveSpeed * deltaTime
	end
	

	SetCameraPosition(position)
end
