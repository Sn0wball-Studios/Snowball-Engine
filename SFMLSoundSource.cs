using System.Numerics;
using SFML.Audio;
using SFML.System;
namespace Snowball
{
    internal class SFMLSoundSource : SoundSource
    {
        Sound sound;

        public SFMLSoundSource(string filename)
        {
            sound = new Sound(new SoundBuffer(Engine.soundDirectory + filename));
        }

        public override void Pause()
        {
            sound?.Pause();
        }

        public override void SetPosition(Vector2 position)
        {
            sound.Position = new Vector3f(position.X, position.Y, 0);
        }

        public override void Start()
        {
            sound?.Play();
        }

        public override void Stop()
        {
            sound?.Stop();
        }
    }
}