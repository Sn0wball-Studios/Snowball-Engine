using System.Numerics;
using System;
public static class Vec2Utils
{
    public const float Rad2Deg = 180 / MathF.PI;
    public const float Deg2Rad = MathF.PI / 180;

    /// <summary>method <c>CastToIntVec</c> casts Vec2 values to integers, useful for keeping text sharp.</summary>
    public static Vector2 CastToIntVec(Vector2 vec)
    {
        return new Vector2((int)vec.X, (int)vec.Y);
    }

    public static float GetAngle(Vector2 me, Vector2 target)
    {
        return 180 - MathF.Atan2(me.X - target.X, me.Y - target.Y);
        //returnreturn Vector2.Normalize((me - target));
    }

    public static Vector2 CreateVec2(float x, float y)
    {
        return new Vector2(x,y);
    }

    /// <summary>method <c>GetDirection</c>gets direction to object.</summary>
    public static Vector2 GetDirection(Vector2 me, Vector2 target)
    {
        float angle = GetAngle(me, target);
        

        return new Vector2(MathF.Cos(angle * Rad2Deg), MathF.Sin(angle * Rad2Deg));
    }

    public static float Distance(Vector2 me, Vector2 target)
    {
        return Vector2.Distance(me, target);
    }

    //have to do this cuz lua
    public static float Length(Vector2 vec)
    {
        return vec.Length();
    }


}