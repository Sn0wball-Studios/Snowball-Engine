using SFML.Graphics;
using SFML.System;
using SFML.Window;
using System;
using System.Numerics;
using System.Collections.Generic;
namespace Snowball
{
    internal class SFMLWindow : WindowImplementation
    {
        private RenderWindow window;
        private RenderTexture uiTexture;
        private SFML.Graphics.Sprite uiSprite;
        private readonly Clock deltaClock = new Clock();
        readonly static Dictionary<string, SFML.Graphics.Sprite> sfSprites = new Dictionary<string, SFML.Graphics.Sprite>();
        readonly Dictionary<string, Text> sfText = new Dictionary<string, Text>();
        RectangleShape rectangleShape = new RectangleShape()
        {
            FillColor = new SFML.Graphics.Color(255, 0, 0, 100),
            OutlineColor = SFML.Graphics.Color.Red,
            OutlineThickness = 1
        };

        public override void Close()
        {
            window.Close();
        }

        public override void ProcessFonts()
        {
            foreach(var font in fonts)
            {
                var sfFont = new Text("Sample Text <3", new Font(DirectoryConsts.fontDirectory + font.Value.ttf))
                {
                    CharacterSize = font.Value.size,
                    FillColor = new SFML.Graphics.Color(font.Value.r, font.Value.g, font.Value.b)
                };
                
                sfText.Add(font.Key, sfFont);
            }
        }

        public override void SetPixel(uint x, uint y, Color color)
        {
            throw new NotImplementedException();
        }

        public override Sprite LoadFromBuffer(uint width, uint height, byte[] buffer, string name)
        {
            Sprite sprite = new Sprite()
            {
                textureFile = name,
                size = new Vector2(width, height)
            };
            var sfSprite = SFMLUtils.CreateSfSprite(width, height, buffer);
            sfSprites.Add(sprite.textureFile, sfSprite);
            return sprite;
        }

        public override bool SpriteIsChached(Sprite sprite)
        {
            return sfSprites.ContainsKey(sprite.textureFile);
        }

        public override void UIDrawText(string text, string font, Vector2 position, OriginType originType)
        {
            DrawText(text, font, position + (camera - size/2), originType);
        }

        public override void DrawText(string text, string font, Vector2 position, OriginType originType)
        {
            var textBox = sfText[font];
            var textSize = textBox.GetLocalBounds();
            BoundingBox textBounds = new BoundingBox(new Vector2(textSize.Width, textSize.Height));
            textBox.DisplayedString = text;
            textBounds.SetOrigin(originType);
            textBox.Origin = SFMLUtils.Vec2ToVec2f(textBounds.origin);
            var pos = Vec2Utils.CastToIntVec((position) - (camera - size/2));
            textBox.Position = SFMLUtils.Vec2ToVec2f(pos);
            uiTexture.Draw(textBox);
        }

        public override bool IsKeyDown(KeyboardKey key)
        {
            return Keyboard.IsKeyPressed((Keyboard.Key)(int)key) && window.HasFocus();
        }

        public override void Create(uint width, uint height, string caption, uint fps)
        {
            window = new RenderWindow(new VideoMode(width, height), caption);
            window.SetFramerateLimit(fps);
            window.Closed += (object e, EventArgs a) => Close();
            uiTexture = new RenderTexture(width, height);
            uiSprite = new SFML.Graphics.Sprite(uiTexture.Texture);

            //have to flip uiTexture upside down because SFML is wierd(weird?)
            uiSprite.Scale = new Vector2f(1, -1);
            uiSprite.Position = new Vector2f(0, height);
            size = new Vector2(width, height);
        }

        public override void PollEvents()
        {
            window.DispatchEvents();
        }

        public override void Clear()
        {
            uiTexture.Clear(SFML.Graphics.Color.Transparent);
            window.Clear(new SFML.Graphics.Color(50,50,50));
        }

        public override Vector2 GetSpriteSize(Sprite sprite)
        {
            var sfSprite = sfSprites[sprite.textureFile];
            return SFMLUtils.Vec2uToVec2(sfSprite.Texture.Size);
        }

        public override void DrawBox(BoundingBox box, Color color)
        {
            rectangleShape.Size = SFMLUtils.Vec2ToVec2f(box.size);
            rectangleShape.FillColor = SFMLUtils.ColortoSfColor(color);
            rectangleShape.Position = SFMLUtils.Vec2ToVec2f(box.min - (camera - size/2));
            window.Draw(rectangleShape);
        }
        
        public override bool IsOpen()
        {
            return window.IsOpen;
        }

        public override float DeltaTime()
        {
            return deltaClock.Restart().AsSeconds();
        }

        public override void SetCaption(string caption)
        {
            window.SetTitle(caption);
        }

        public override void SetFramerate(uint frameRate)
        {
            window.SetFramerateLimit(frameRate);
        }

        private void PresentUI()
        {
            uiSprite.Texture = uiTexture.Texture;
            window.Draw(uiSprite);
        }

        public override void Present()
        {
            PresentUI();
            window.Display();
        }

        public override void CacheSprite(Sprite sprite)
        {
            var sfSprite = SFMLUtils.CreateSfSprite(sprite);
            sfSprites.Add(sprite.textureFile, sfSprite);
        }

        public override void DrawSprite(Snowball.Sprite sprite)
        {
            if(!IsOnScreen(sprite))
            {
                return;
            }
            //DrawBox(sprite.bounds, Color.Red);
            var sfSprite = sfSprites[sprite.textureFile];
            sfSprite.Position = SFMLUtils.Vec2ToVec2f(sprite.position - (camera - size/2));
            sfSprite.Rotation = sprite.rotation;
            window.Draw(sfSprite);
        }
    }
}