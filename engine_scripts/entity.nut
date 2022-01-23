import("snowball/vec2")
class Entity
{
    static entities = [];

    position = null;
    rotation = 0.0;
    name = "Entity";

    constructor(_position, rotation, _name)
    {
        local str = format("created entity %s", _name);
    }
}