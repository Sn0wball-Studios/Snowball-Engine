using System;
namespace Snowball
{
    public static class Rng
    {
        static Random r = new Random(80085);

        public static float RandomFloat()
        {
            return (float)r.NextDouble();
        }
        
        public static int Range(int min, int max)
        {
            return r.Next(min, max);
        }
    }
}