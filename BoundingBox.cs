using System.Numerics;
namespace Snowball
{
    public class BoundingBox
    {
        public Vector2 min = new Vector2();


        
        public Vector2 size = new Vector2();

        public Vector2 origin;

        public Vector2 max
        {
            get
            {
                return min + (max/2); 
            }
        }

        public Vector2 topRight
        {
            get
            {
                return new Vector2(min.X + size.X, min.Y);
            }
        }

        public Vector2 bottomCenter
        {
            get
            {
                return new Vector2(size.X / 2, size.Y);
            }
        }

        public Vector2 topCenter
        {
            get
            {
                return new Vector2(size.X / 2, 0);
            }
        }

        public BoundingBox(Vector2 size)
        {
            this.size = size;   
        }
    }
}