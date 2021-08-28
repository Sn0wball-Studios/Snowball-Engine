using System.Numerics;
namespace Snowball
{
    public abstract class SoundSource
    {
        public abstract void Start();
        public abstract void Stop();
        public abstract void Pause();
        public abstract void SetPosition(Vector2 position);
    }
}