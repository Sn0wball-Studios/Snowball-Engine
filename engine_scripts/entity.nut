import("snowball/vec2")
class Entity
{
    position = null;
    rotation = 0.0;
    name = "Entity";
    sprite = null;

    constructor()
    {
       
    }

    static entities = [];
    static function UpdateEntities()
    {
        foreach(i, entity in entities)
        {
            entity.update();
        }
    }

    static function Create(classType, position, angle)
    {
        local entity = classType();
        entity.position = position;
        entity.rotation = angle;
        entities.append(entity);
    }
}

//squirrel doesn't seem to have static functions?
function entityManager_update()
{

}