import("snowball/entity")
class Animatronic
{
    //move chance out of 10
    moveChance = 0;
    jumpScareSound = null;
    name = "Unnamed";

    constructor(_moveChance, _jumpScareSound, _name)
    {
        moveChance = _moveChance;
        jumpScareSound = _jumpScareSound;
        name = _name;
    }

    function move()
    {
        local i = rand() % 10
        if(i < moveChance)
        {
            ::sprint(name + " has moved");
        }
    }
}