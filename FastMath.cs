namespace Snowball
{
    public static class FastMath
    {
        public static float Sin(float x)
        {
            float t = x * 0.15915f;
            t -= (int)t;
            return 20.785f * (t-0.0f) * (t-0.5f) * (t-1.0f);
        }
    }
}