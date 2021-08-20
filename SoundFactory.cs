namespace Snowball
{
    public abstract class SoundFactory
    {
        public abstract SoundSource CreateSource(string fileName);
    }
}