using System.Numerics;
namespace Snowball
{
    public interface IRenderTarget
    {
        void SetPixel(uint x, uint y, Color color);
        void Clear();
        void Present();
        void DrawSprite(Sprite sprite);
        void UIDrawText(string text, string font, Vector2 position, OriginType type);
        void DrawText(string text, string font, Vector2 position, OriginType type);
        void DebugDrawBox(BoundingBox box);
    }
}