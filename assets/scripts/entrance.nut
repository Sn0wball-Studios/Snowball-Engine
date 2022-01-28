class Entrance extends entity
{
    room = ""
    name = ""
    
    function setName(_name)
    {
        name = "Entrance to " + _name
    }

    function setRoom(_room)
    {
        room = "assets/rooms/" + _room + ".json"
    }

    function update()
    {

    }
}