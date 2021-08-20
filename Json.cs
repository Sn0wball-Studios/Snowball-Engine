using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.IO; 
using System.Collections.Generic;   
namespace Snowball
{
    public static class Json
    {
        static JsonSerializerSettings settings = new JsonSerializerSettings()
        {
            TypeNameHandling = TypeNameHandling.Auto,
            ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
            PreserveReferencesHandling = PreserveReferencesHandling.Objects,
            Formatting = Formatting.Indented
        };

        public static void InitSettings()
        {
            settings.Converters.Add(new StringEnumConverter());
        }

        public static void Save(string filename, object obj)
        {
            var text = JsonConvert.SerializeObject(obj, settings);
            File.WriteAllText(filename, text);    
        }

        public static List<T> LoadDirectory<T>(string directory)
        {
            var files = Directory.GetFiles(directory, "*.json", SearchOption.AllDirectories);
            List<T> output = new List<T>();

            foreach(var file in files)
            {
                output.Add(Load<T>(file));
            }
            return output;
        }

        public static T Load<T>(string filename)
        {
            if(!File.Exists(filename))
            {
                Save(filename, Activator.CreateInstance<T>());
            }

            var text = File.ReadAllText(filename);
            return JsonConvert.DeserializeObject<T>(text, settings);
        }
    }
}