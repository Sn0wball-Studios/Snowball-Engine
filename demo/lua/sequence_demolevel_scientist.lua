scripted_sequence = require 'scripted_sequence'

shitballs = GetScriptedObjectByID("Dr Shitballs")
jeff = GetScriptedObjectByID("Jeff")

function start()
    id = "sequence_test"
    scripted_sequence.InitObject(jeff)
    scripted_sequence.InitObject(shitballs)

    scripted_sequence.start_jeff_shitballs(jeff, shitballs)

    --sequence.Sequence_MoveToTask(jeff, Vec2(0, 5 * 32))
    --sequence.Sequence_MoveToTask(shitballs, Vec2(1 * 32, 5 * 32))
end


function update()
    --UIDrawText("playing scripted sequence...", "demoFont", Vec2(0,0), OriginType.topLeft)
end