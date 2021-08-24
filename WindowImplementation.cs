using System.Collections.Generic;
using System.Numerics;
using System;
namespace Snowball
{
    public abstract class WindowImplementation : IRenderTarget
    {
        public abstract void PollEvents();
        public abstract void Create(uint width, uint height, string caption, uint fps=60);
        public abstract void Clear();
        public abstract void Present();
        public abstract  void Close();
        public abstract bool IsKeyDown(KeyboardKey key);
        public abstract bool IsOpen();
        public abstract void SetFramerate(uint frameRate);
        public abstract void SetCaption(string caption);
        public abstract void DrawSprite(Sprite sprite);
        public abstract void UIDrawText(string text, string font, Vector2 position, OriginType type);
        public abstract void DrawText(string text, string font, Vector2 position, OriginType type);
        public abstract void ProcessFonts();
        public abstract float DeltaTime();
        public abstract Vector2 GetSpriteSize(Sprite sprite);
        public abstract BoundingBox GetBounds(Sprite sprite);
        public abstract void CacheSprite(Sprite sprite);
        public abstract bool SpriteIsChached(Sprite sprite);
        public abstract void SetPixel(uint x, uint y, Color color);

        public abstract Sprite LoadFromBuffer(uint width, uint height, byte[] buffer, string name);
        public abstract void DebugDrawBox(BoundingBox box);
        public Dictionary<string, FontFile> fonts = new Dictionary<string, FontFile>();

        public Vector2 size = new Vector2();
        public Vector2 camera = new Vector2();

        public void SetCamera(Vector2 position)
        {
            camera = position;
        }

        


        public bool IsOnScreen(Sprite sprite)
        {
            BoundingBox screen = new BoundingBox(size);
            screen.min = camera - size / 2;
            return BoundingBox.AABB(screen, sprite.bounds);
        }

        public void LoadFonts()
        {
            fonts = Json.Load<Dictionary<string, FontFile>>(Engine.gameDirectory + "fonts.json");
            ProcessFonts();
        }
    }
}