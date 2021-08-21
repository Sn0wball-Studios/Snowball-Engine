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

        public override void Close()
        {
            window.Close();
        }

        readonly Dictionary<string, Text> sfText = new Dictionary<string, Text>();

        public override void ProcessFonts()
        {
            foreach(var font in fonts)
            {
                var sfFont = new Text("Sample Text <3", new Font(Engine.fontDirectory + font.Value.ttf))
                {
                    CharacterSize = font.Value.size,
                    FillColor = new Color(font.Value.r, font.Value.g, font.Value.b)
                };
                
                sfText.Add(font.Key, sfFont);
            }
        }

        public override void UIDrawText(string text, string font, Vector2 position, OriginType type)
        {
            var textBox = sfText[font];
            var textSize = textBox.GetGlobalBounds();
            BoundingBox b = new BoundingBox(new Vector2(textSize.Width, textSize.Height));
            textBox.DisplayedString = text;
            Vector2 textOrigin = new Vector2();
            switch (type)
            {
                case OriginType.topLeft:
                    break;
                case OriginType.topCenter:
                    textOrigin = b.topCenter;
                    break;
                case OriginType.topRight:
                    break;
                case OriginType.centerLeft:
                    break;
                case OriginType.centerCenter:
                    break;
                case OriginType.centerRight:
                    break;
                case OriginType.bottomLeft:
                    break;
                case OriginType.bottomCenter:
                    textOrigin = b.bottomCenter;
                    break;
                case OriginType.bottomRight:
                    break;
            }
            textBox.Origin = SFMLUtils.Vec2ToVec2f(Vec2Utils.CastToIntVec(textOrigin));
            var pos = Vec2Utils.CastToIntVec(position);
            textBox.Position = SFMLUtils.Vec2ToVec2f(pos);
            uiTexture.Draw(textBox);
        }

        public override void DrawText(string text, string font, Vector2 position, OriginType type)
        {
            var textBox = sfText[font];
            var textSize = textBox.GetLocalBounds();
            BoundingBox b = new BoundingBox(new Vector2(textSize.Width, textSize.Height));
            textBox.DisplayedString = text;
            Vector2 textOrigin = new Vector2();
            switch (type)
            {
                case OriginType.topLeft:
                    textOrigin = new Vector2();
                    break;
                case OriginType.topCenter:
                    textOrigin = b.topCenter;
                    break;
                case OriginType.topRight:
                    break;
                case OriginType.centerLeft:
                    break;
                case OriginType.centerCenter:
                    break;
                case OriginType.centerRight:
                    break;
                case OriginType.bottomLeft:
                    break;
                case OriginType.bottomCenter:
                    textOrigin = b.bottomCenter;
                    break;
                case OriginType.bottomRight:
                    break;
                default:
                    textOrigin = new Vector2();
                    break;

            }
            textBox.Origin = SFMLUtils.Vec2ToVec2f(textOrigin);
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
            uiTexture.Clear(Color.Transparent);
            window.Clear(new Color(50,50,50));

        }

        public override Vector2 GetSpriteSize(Sprite sprite)
        {
            var sfSprite = sfSprites[sprite.textureFile];
            return SFMLUtils.Vec2uToVec2(sfSprite.Texture.Size);
        }

        public override BoundingBox GetBounds(Sprite sprite)
        {
            BoundingBox output = new BoundingBox(new Vector2(sprite.size.X, sprite.size.Y) * 1.5f);
            output.min =  sprite.position - (sprite.size / 1.5f);
            return output;
        }
        RectangleShape rectangleShape = new RectangleShape()
        {
            FillColor = new Color(255, 0, 0, 100),
            OutlineColor = Color.Red,
            OutlineThickness = 1
        };

        public override void DebugDrawBox(BoundingBox box)
        {
            rectangleShape.Size = SFMLUtils.Vec2ToVec2f(box.size);
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

        public override bool SpriteIsChached(Sprite sprite)
        {
            return sfSprites.ContainsKey(sprite.textureFile);
        }
        
        readonly static Dictionary<string, SFML.Graphics.Sprite> sfSprites = new Dictionary<string, SFML.Graphics.Sprite>();




        public override void DrawSprite(Snowball.Sprite sprite)
        {
            if(!IsOnScreen(sprite))
            {
                return;
            }

            DebugDrawBox(sprite.bounds);
            var sfSprite = sfSprites[sprite.textureFile];
            sfSprite.Position = SFMLUtils.Vec2ToVec2f(sprite.position - (camera - size/2));
            sfSprite.Rotation = sprite.rotation;
            window.Draw(sfSprite);
        }
    }
}