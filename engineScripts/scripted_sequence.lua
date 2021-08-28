function IssueNextTask(object)
    if coroutine.status(scripted_sequence.scripted[object.id].behaviour) ~= 'dead' then
        coroutine.resume(scripted_sequence.scripted[object.id].behaviour, object)
    end
end