using System.Numerics;
namespace Snowball
{
    public static class ScriptedSequences
    {
        public static void LUA_MoveToLocation(ScriptedObject obj, Vector2 target, float speed)
        {
            obj.manipulators.Add(new ObjectTargetManipulator(target, 10, speed, obj));
        }

        public static void LUA_Delay(ScriptedObject obj, float time)
        {
            obj.manipulators.Add(new DelayManipulator(time));
        }

        public static void LUA_Say(ScriptedObject obj, string dialog, float time, string font)
        {
            obj.manipulators.Add(new ObjectSpeechManipulator(dialog, time, font, obj));
        }
    }
}