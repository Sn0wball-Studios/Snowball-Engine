using System.Collections.Generic;
using Newtonsoft.Json;
namespace Snowball
{
    public class InputAxis
    {

        public KeyboardKey up, down;
        

        [JsonIgnore]
        public float currentValue;
    }
}