local player = {}

function player:create(position_)

    o = {
        position = position_, 
        rotation = 0, 
        id = 0, 
        name = "player",
        sprite = LoadSprite("guy.json"),
        update = player.update,
        speed = 32 * 1.4
    }
    
	self.__index = self
	setmetatable(o, self)
    return o

end


function player.update(self)
    self.position.X = self.position.X + InputGetAxis("horizontal") * self.speed * dt()
	self.position.Y = self.position.Y + -InputGetAxis("vertical") * self.speed * dt()
	SetCameraPosition(self.position)
end

return player