using System.Numerics;
using System;
namespace Snowball
{
    public class ObjectTargetManipulator : ObjectManipulator
    {
        Vector2 target;
        float speed = 32;
        float minDistance = 6;
        public ObjectTargetManipulator(Vector2 target, float minDistance, float speed, ScriptedObject scriptedObject)
        {
            SetTarget(target);
            this.scriptedObject = scriptedObject;
            SetMinDistance(minDistance);
            SetSpeed(speed);
        }

        public static void LUA_MoveToLocation(ScriptedObject obj, Vector2 target, float speed)
        {
            obj.manipulators.Add(new ObjectTargetManipulator(target, 10, speed, obj));
        }


        public void SetSpeed(float speed)
        {
            this.speed = speed;
        }

        public void SetMinDistance(float minDistance)
        {
            this.minDistance = minDistance;
        }

        public void SetTarget(Vector2 target)
        {
            this.target = target;
        }

        public override bool Update()
        {
            Vector2 velocity = Vec2Utils.GetDirection(scriptedObject.position, target);

            float distance = Vector2.Distance(scriptedObject.position, target);

            scriptedObject.position += velocity * speed * Engine.deltaTime;
            complete = distance < minDistance;
            return distance < minDistance;
        }
    }
}