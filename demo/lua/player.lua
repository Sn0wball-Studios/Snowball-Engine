local player = {}

function player:create(position_)

    o = {
        position = position_, 
        rotation = 0, 
        id = 0, 
        name = "player",
        sprite = LoadSprite("jario.json"),
        update = player.update,
        physics = PhysicsHandler(position_, Vec2(16 * 2.8, 16 * 2.8)),
        gravity = 9.8 * 16,
    }
    
	self.__index = self
	setmetatable(o, self)
    return o

end

function player.update(self)
    --self.velocity = Vec2(0,0)
    self.physics.acceleration = Vec2(InputGetAxis("horizontal") * 100, 0)
    self.physics.DoPhysics()
    self.position = self.physics.position
    
    --self.position = self.position + self.velocity * dt

	SetCameraPosition(self.position)

end

return player