using System.Collections.Generic;
using System;
using System.IO;
namespace Snowball
{
    public static class Input
    {
        static Dictionary<string, InputAxis> axes = new Dictionary<string, InputAxis>();
        

        public static bool IsKeyDown(KeyboardKey key)
        {
            return Engine.window.IsKeyDown(key);
        }

        public static void LoadDefs()
        {
            Console.WriteLine("loading input axes...");
            axes = Json.Load<Dictionary<string, InputAxis>>(DirectoryConsts.inputDirectory + "axes.json");
        }


        public static void ProcessInputs()
        {
            foreach(var axis in axes.Values)
            {
                axis.currentValue = 0;
                if(IsKeyDown(axis.up))
                {
                    axis.currentValue = 1;
                }
                if(IsKeyDown(axis.down))
                {
                    axis.currentValue = -1;
                }

            }
        }

        public static float GetAxis(string name)
        {
            return axes[name].currentValue;
        }

    }
}