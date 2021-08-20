
local scripted_sequence = {}
scripted_sequence.scripted = {}

--todo: move these to seperate lua and make custom loader
function scripted_sequence.MoveToDestination(obj)
    while true do 
        MoveObject(obj, scripted_sequence.scripted[obj.id].target, 40)
        coroutine.yield()
    end
end


function speak(object, dialog, time, font)
    Say(object, dialog, time, font)
    coroutine.yield()
end

function scripted_sequence.loop_jeff_shitballs(jeff)
    shit_balls = scripted_sequence.scripted[jeff.id].shit_balls
    while true do 
        speak(jeff, "hello professor", 3, "npcName")
        speak(jeff, "you're acting\npretty sussy\n\t\ttoday",3, "npcName")
        speak(jeff, "...", 5, "npcName")
        MoveObject(jeff, Vec2(-20 * 32, 100), 40)
        speak(shit_balls, "you're fired", 1, "zombieSpeech")
    end
end

function scripted_sequence.start_jeff_shitballs(jeff, shitballs)
    scripted_sequence.scripted[jeff.id] = {behaviour = coroutine.create(scripted_sequence.loop_jeff_shitballs), target=Vec2(0, 5 * 32), done = false, shit_balls = shitballs}
    scripted_sequence.IssueNextTask(jeff)
end

function scripted_sequence.InitObject(object)
    SetScriptedSequenceManager(scripted_sequence, object)
end

function scripted_sequence.IssueNextTask(object)
    --prevents crashing lol
    if scripted_sequence.scripted[object.id] == nill then
        return
    end
    
    if coroutine.status(scripted_sequence.scripted[object.id].behaviour) ~= 'dead' then
        coroutine.resume(scripted_sequence.scripted[object.id].behaviour, object)
    end
end



function scripted_sequence.Sequence_MoveToTask(object, targetPos) 
    scripted_sequence.scripted[object.id] = {behaviour = coroutine.create(scripted_sequence.MoveToDestination), target=targetPos, done = false}
    scripted_sequence.IssueNextTask(object)
end

return scripted_sequence