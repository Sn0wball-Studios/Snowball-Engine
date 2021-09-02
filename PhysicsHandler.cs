using System.Numerics;
namespace BBQLib
{
    public class PhysicsHandler
    {
        public Vector2 velocity = new Vector2();
        public Vector2 position = new Vector2();
        public Vector2 maxSpeed = new Vector2();
        public float drag = 25;
        public Vector2 acceleration = new Vector2();

        public static PhysicsHandler Create(Vector2 position, Vector2 maxSpeed)
        {
            PhysicsHandler p = new PhysicsHandler()
            {
                position = position,
                maxSpeed = maxSpeed
            };
            return p;
        }

        public void DoPhysics()
        {
            velocity += acceleration * BBQLib.DeltaTime;
            if(velocity.X > maxSpeed.X)
            {
                velocity.X = maxSpeed.X;
            }
            if(velocity.X < -maxSpeed.X)
            {
                velocity.X = -maxSpeed.X;
            }

            if(velocity.Y > maxSpeed.Y)
            {
                velocity.Y = maxSpeed.Y;
            }
            if(velocity.Y < -maxSpeed.Y)
            {
                velocity.Y = -maxSpeed.Y;
            }

            if(velocity.X > 0)
            {
                velocity.X -= drag * BBQLib.DeltaTime;
            }
            if(velocity.X < 0)
            {
                velocity.X += drag * BBQLib.DeltaTime;
            }


            position += velocity * BBQLib.DeltaTime;
        }
    }
}