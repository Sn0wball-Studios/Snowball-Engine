namespace Snowball
{
    public abstract class ObjectManipulator
    {
        public ScriptedObject scriptedObject;
        public abstract bool Update();
        public bool loop = false;
        public bool complete = false;
    }
}