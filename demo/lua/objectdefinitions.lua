scientist = require("scientist")
player = require("player")

local objectdefinitions = {
    ["scientist"] = scientist,
    ["player"] = player
}

function objectdefinitions:create(name, position)
    CreateObject(objectdefinitions[name]:create(Vec2(100, 100)))
end

return objectdefinitions