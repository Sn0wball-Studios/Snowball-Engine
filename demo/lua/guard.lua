guard = require("baseobject")

function guard.update()
    guard.rotation = guard.rotation + deltaTime-- * 30
end

function guard.start()
    guard.name = "Guard"
    guard.sprite = LoadSprite("guy.json")
end


return guard