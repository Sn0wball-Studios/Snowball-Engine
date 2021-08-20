using System.Collections.Generic;
using System.Numerics;
namespace Snowball
{
    public abstract class WindowImplementation
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
        public abstract void CacheSprite(Sprite sprite);
        public abstract bool SpriteIsChached(Sprite sprite);

        public Dictionary<string, FontFile> fonts = new Dictionary<string, FontFile>();

        public Vector2 size = new Vector2();
        public Vector2 camera = new Vector2();
        public void SetCamera(Vector2 position)
        {
            camera = position;
        }

        public void LoadFonts()
        {
            fonts = Json.Load<Dictionary<string, FontFile>>(Engine.gameDirectory + "fonts.json");
            ProcessFonts();
        }
    }
}