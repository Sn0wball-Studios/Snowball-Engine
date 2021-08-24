using System.Numerics;
using SFML.System;
using SFML.Graphics;
namespace Snowball
{
    public abstract class RenderTextureImplementation : IRenderTarget
    {
        public abstract void Clear();
        public abstract void DrawSprite(Sprite sprite);
        public abstract void DrawText(string text, string font, Vector2 position, OriginType type);
        public abstract void Present();
        public abstract void SetPixel(uint x, uint y, Color color);
        public abstract void UIDrawText(string text, string font, Vector2 position, OriginType type);
        public abstract void DrawBox(BoundingBox box, Color color);
    }
}