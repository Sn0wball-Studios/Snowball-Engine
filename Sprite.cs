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

        [JsonIgnore]
        public BoundingBox bounds
        {
            get
            {
                return Engine.window.GetBounds(this);
            }
        }

        //bri'ish sprite
        public void Init(string json)
        {
            if(!Engine.window.SpriteIsChached(this))
            {
                Engine.window.CacheSprite(this);
                size = Engine.window.GetSpriteSize(this);
                Json.Save(Engine.spriteDirectory + json, this);
            }
        }
    }
}