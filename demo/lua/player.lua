local player = {}

function player.initPhysics(self)
    self.physics = PhysicsHandler(self.position, self.speed)
    self.physics.drag = 64
end

function player:create(position_)

    o = {
        position = position_, 
        rotation = 0, 
        name = "player",
        sprite = LoadSprite("guy.json"),
        update = player.update,
        speed = 32 * 1.4
    }
    
	self.__index = self
	setmetatable(o, self)
    self.initPhysics(o)
    return o
end

function player.update(self)
    input = Vec2(InputGetAxis("horizontal"), -InputGetAxis("vertical"))

    if(Vec2GetLength(input) > 0) then
        self.physics.acceleration = Vec2Normalize(input) * self.speed
    else
        self.physics.acceleration = Vec2(0,0)
    end

    self.physics.DoPhysics()
    self.position = self.physics.position
	SetCameraPosition(self.position)

    UIDrawText("velocity ".. (self.physics.velocity/32).ToString().. " m/s", "demoFont", Vec2(0,40), OriginType.topLeft)
end

return player