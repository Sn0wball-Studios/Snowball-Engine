using MoonSharp.Interpreter;
using MoonSharp.Interpreter.Loaders;
using System.Collections.Generic;
using System.Numerics;
using System;
namespace Snowball
{
    public class LuaObject
    {
        public string name = "Object";
        public int id;

        public static List<DynValue> objects = new List<DynValue>();
        public static Script script = new Script();


        public static DynValue GetObject(int id)
        {
            foreach(var value in objects)
            {
                int _id = (int)value.Table["id"];
                if(id == _id)
                {
                    Console.WriteLine("cum");
                    return value;
                }
            }
            return null;
        }

        public static Table CreateLuaTable(string file, Vector2 position)
        {
            var value = LuaObject.script.DoFile(Engine.scriptDirectory + file);
            value.Table["position"] = position;
            ((Closure)value.Table["start"]).Call();
            objects.Add(value);
            return value.Table;
        }

        public static void SetProperty(string name, object value, DynValue dyn)
        {
            dyn.Table[name] = value;
        }

        public static T GetProperty<T>(string name, DynValue dyn)
        {
            return (T)dyn.Table[name];
        }

        

        public static void Update()
        {
            foreach(var obj in objects)
            {
                SetProperty("deltaTime", Engine.deltaTime, obj);
                
                ((Closure)obj.Table["update"]).Call();
                
                var sprite = obj.Table["sprite"] as Sprite;

                if(sprite != null)
                {
                    sprite.position = GetProperty<Vector2>("position", obj);
                    sprite.rotation = (float)GetProperty<double>("rotation", obj);
                    Engine.window.DrawSprite(sprite as Sprite);
                }
            }
        }

        public static void LoadLua()
        {
            script.Options.ScriptLoader = new FileSystemScriptLoader()
            {
                ModulePaths = new string[]
                {
                    Engine.scriptDirectory + "?.lua"
                }
            };
            LuaUtils.LoadGlobalFunctions(script, new LuaObject());
            
            LuaUtils.LoadFuncs(script);
            script.DoFile(Engine.scriptDirectory + "load.lua");
        }
        

    }    
}
