using System.Numerics;
using System;
namespace BBQLib
{
    public class PhysicsHandler
    {
        public Vector2 velocity = new Vector2();
        public Vector2 position = new Vector2();
        public float maxSpeed = 50;
        public float drag = 25;
        public Vector2 acceleration = new Vector2();

        public BoundingBox box;

        public static PhysicsHandler Create(Vector2 position, float maxSpeed)
        {
            PhysicsHandler handler = new PhysicsHandler()
            {
                position = position,
                maxSpeed = maxSpeed
            };
            return handler;
        }

        public void SetBox(BoundingBox box)
        {
            this.box = box;
        }

        public void DoPhysics()
        {
            velocity += acceleration * BBQLib.DeltaTime;
            
            //todo make this not ugly
            if(velocity.X > maxSpeed)
            {
                velocity.X = maxSpeed;
            }
            if(velocity.X < -maxSpeed)
            {
                velocity.X = -maxSpeed;
            }

            if(velocity.Y > maxSpeed)
            {
                velocity.Y = maxSpeed;
            }
            if(velocity.Y < -maxSpeed)
            {
                velocity.Y = -maxSpeed;
            }

            if(acceleration.Length() < 1)
            {
                if(velocity.X > 0)
                {
                    velocity.X -= drag * BBQLib.DeltaTime;
                }
                if(velocity.X < 0)
                {
                    velocity.X += drag * BBQLib.DeltaTime;
                }

                if(velocity.Y > 0)
                {
                    velocity.Y -= drag * BBQLib.DeltaTime;
                }
                if(velocity.Y < 0)
                {
                    velocity.Y += drag * BBQLib.DeltaTime;
                }
            }
            
            if(Math.Abs(velocity.X) < 0.1){velocity.X = 0;}
            if(MathF.Abs(velocity.Y) < 0.1){velocity.Y = 0;}

            position += velocity * BBQLib.DeltaTime;
        }
    }
}