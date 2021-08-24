local entity = {}

function entity:create(position_)

    variables = {
        position = position_, 
        rotation = 0, 
        id = 0, 
        name = "entity",
        sprite = LoadSprite("blank.json"),
        update = entity.update,
    }

    
	self.__index = self
	setmetatable(variables, self)
    return variables

end



function entity.update(self)
end

return entity