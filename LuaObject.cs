using MoonSharp.Interpreter;
using MoonSharp.Interpreter.Loaders;
using System.Collections.Generic;
using System.Numerics;
using BBQLib;
using System;
namespace Snowball
{
    public class LuaObject
    {
        public static List<Table> objects = new List<Table>();
        public static Script script = new Script();


        public static Table GetObject(int id)
        {
            foreach(var value in objects)
            {
                int _id = (int)value["id"];
                if(id == _id)
                {
                    Console.WriteLine("cum");
                    return value;
                }
            }
            return null;
        }



        public static Table AddObject(Table table)
        {
            table["id"] = objects.Count;
            objects.Add(table);
            return table;
        }

        public static void SetProperty(string name, object value, Table dyn)
        {
            dyn[name] = value;
        }

        public static T GetProperty<T>(string name, Table dyn)
        {
            return (T)dyn[name];
        }

       
        public static void Update()
        {
            script.Globals["dt"] = BBQLib.BBQLib.DeltaTime;
            ((Closure)script.Globals["update"]).Call();
            
            foreach(var obj in objects)
            {              
                ((Closure)obj["update"]).Call(obj);
                
                var sprite = obj["sprite"] as Sprite;

                if(sprite != null)
                {
                    sprite.position = GetProperty<Vector2>("position", obj);
                    sprite.rotation = (float)GetProperty<double>("rotation", obj);
                    BBQLib.BBQLib.Draw(sprite as Sprite);
                }
                
            }
            
        }

        public static void LoadLua()
        {
            script.Options.ScriptLoader = new FileSystemScriptLoader()
            {
                ModulePaths = new string[]
                {
                    DirectoryConsts.scriptDirectory + "?.lua"
                }
            };
            LuaUtils.LoadGlobalFunctions(script, new LuaObject());
            script.DoFile(DirectoryConsts.scriptDirectory + "load.lua");

            ((Closure)script.Globals["start"]).Call();
        }
        

    }    
}
