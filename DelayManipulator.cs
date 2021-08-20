namespace Snowball
{
    public class DelayManipulator : ObjectManipulator
    {
        float timer = 0;
        float time;
        public DelayManipulator(float time)
        {
            this.time = time;
        }

        public override bool Update()
        {
            timer += Engine.deltaTime;
            complete = timer > time;
            return complete;
        }
    }
}