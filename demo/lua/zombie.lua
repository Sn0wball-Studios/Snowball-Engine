
function start()
    tag = "zombie"
    targetHandler = CreateScriptableObject("target_handler.lua", position)
    speechHandler = CreateScriptableObject("speech_handler.lua", position)
    targetHandler.DoFunction("SetTarget", position)
    speechHandler.DoFunction("SetFont", "zombieSpeech")
    targetHandler.DoFunction("SetSpeed", RandomRange(15,20))
    AddSprite("guy.json")
end


groanTime = RandomRange(4, 10)
groanTimer = 0

function handleGroaning()
    groanTimer = groanTimer + deltaTime
    speechHandler.position = position
    if groanTimer > groanTime then
        groanTimer = 0
        groanTime = RandomRange(4,10)
        speechHandler.DoFunction("Say", noises[RandomRange(1,#noises + 1)], 2)
    end
end

function update()
    handleGroaning()
    enemies = GetObjectsWithTag("human")
    targetHandler.DoFunction("SetTarget", enemies[2].position)
    position = targetHandler.DoFunction("GetPosition")    
end


noises = {
    "groan...",
    "brains..."
}