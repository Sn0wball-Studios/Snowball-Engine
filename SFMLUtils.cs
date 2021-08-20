using SFML.System;
using SFML.Graphics;
using System.Numerics;
namespace Snowball
{
    internal static class SFMLUtils
    {
        public static Vector2f Vec2ToVec2f(Vector2 vec)
        {
            return new Vector2f(vec.X, vec.Y);
        }

        public static Vector2 Vec2fToVec2(Vector2f vec)
        {
            return new Vector2(vec.X, vec.Y);
        }

        public static Vector2 Vec2uToVec2(Vector2u vec)
        {
            return new Vector2(vec.X, vec.Y);
        }

        public static Vector2 Vec2iToVec2(Vector2i vec)
        {
            return new Vector2(vec.X, vec.Y);
        }

        public static SFML.Graphics.Sprite CreateSfSprite(Sprite sprite)
        {
            return new SFML.Graphics.Sprite(new Texture(Engine.spriteDirectory + sprite.textureFile))
            {
                Origin = new Vector2f(sprite.origin.X, sprite.origin.Y)
            };
        }

    }
}