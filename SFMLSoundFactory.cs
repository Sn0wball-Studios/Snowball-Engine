using System;
using System.IO;
using System.Linq;
namespace Snowball
{
    internal class SFMLSoundFactory : SoundFactory
    {
        static readonly string[] supportedFormats = new string[]
        {
            ".ogg",
            ".wav"
        };

        public override SoundSource CreateSource(string fileName)
        {
            var extension = Path.GetExtension(fileName);
            if(!supportedFormats.Contains(extension))
            {
                throw new FileLoadException(string.Format("{0} does not support the extension {1} please use another module", GetType().Name, extension));
            }
            
            SFMLSoundSource source = new SFMLSoundSource(fileName);
            return source;
        }
    }
}