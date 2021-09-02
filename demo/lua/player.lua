local player = {}

function player.initPhysics(self)
    self.physics = PhysicsHandler(self.position, Vec2(16 * 2.8, 16 * 2.8))
    self.physics.drag = 50
end

function player:create(position_)

    o = {
        position = position_, 
        rotation = 0, 
        id = 0, 
        name = "player",
        sprite = LoadSprite("guy.json"),
        update = player.update,
        speed = 100
    }
    
	self.__index = self
	setmetatable(o, self)
    self.initPhysics(o)
    return o
end

function player.update(self)
    self.physics.acceleration = Vec2(InputGetAxis("horizontal"), -InputGetAxis("vertical")) * self.speed
    self.physics.DoPhysics()
    self.position = self.physics.position
	SetCameraPosition(self.position)

end

return player