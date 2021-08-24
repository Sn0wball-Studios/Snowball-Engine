using System.Numerics;
using Newtonsoft.Json;
using SFML.Graphics;
namespace Snowball
{
    public class Sprite
    {
        [JsonIgnore]
        public float rotation;
        [JsonIgnore]
        public Vector2 position = new Vector2();
        
        public Vector2 origin = new Vector2();
        public string textureFile;

        public Vector2 size = new Vector2();

        public static Sprite LoadSprite(string spriteFile)
        {
            Sprite sprite = Json.Load<Sprite>(DirectoryConsts.spriteDirectory + spriteFile);
            sprite.Init(spriteFile);
            return sprite;
        }

        public static Sprite LoadIsometric(string spriteFile)
        {
            var sprite = LoadSprite(spriteFile);
            sprite.origin = sprite.bounds.bottomCenter;
            return sprite;
        }

        [JsonIgnore]
        public BoundingBox bounds
        {
            get
            {
                return GetBounds();
            }
        }

        public BoundingBox GetBounds()
        {
            BoundingBox output = new BoundingBox(new Vector2(size.X, size.Y) * 1.5f);
            output.min =  position - (size / 1.5f);
            return output;
        }

        //bri'ish sprite
        public void Init(string json)
        {
            if(!Engine.window.SpriteIsChached(this))
            {
                Engine.window.CacheSprite(this);
                size = Engine.window.GetSpriteSize(this);
                Json.Save(DirectoryConsts.spriteDirectory + json, this);
            }
        }
    }
}