local scientist = {}

function scientist:create(position_)

    o = {
        position = position_, 
        rotation = 0, 
        id = 0, 
        name = "scientist",
        sprite = LoadSprite("scientist".. RandomRange(1,5) ..".json"),
        update = scientist.update,
        timer = 1,
        speed = Random() * 4
    }
    
	self.__index = self
	setmetatable(o, self)
    return o

end


function scientist.update(self)
    self.timer = self.timer + dt() * self.speed

    self.position.Y = self.position.Y + math.sin(self.timer) * dt() * 16
    self.position.X = self.position.X + math.cos(self.timer) * dt() * 16

    self.rotation = self.rotation + dt() * self.speed * 180
end

return scientist