namespace Snowball
{
    public sealed class Color
    {
        public byte r = 255, g = 255, b = 255, a = 255; 
        
        public Color()
        {

        }

        public Color(byte r, byte g, byte b, byte a = 25)
        {
            this.r = r;
            this.g = g;
            this.b = b;
            this.a = a;
        }
    }
}