using MoonSharp.Interpreter;
using MoonSharp.Interpreter.Loaders;
using System.Collections.Generic;
using System.Numerics;
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


        static void HandleCollisions(Table objA)
        {

                var simBounds = new BoundingBox(new Vector2(Engine.simulationRange, Engine.simulationRange));
                simBounds.min = Engine.window.camera - Engine.window.size / 2;
                var spriteA = objA["sprite"] as Sprite;
                if(spriteA != null)
                {
                    

                    if(!BoundingBox.AABB(spriteA.bounds, simBounds))
                    {
                        return;
                    }

                    foreach(var objB in objects)
                    {
                        if((int)((double)objB["id"]) == (int)((double)objA["id"]))
                        {
                            continue;
                        }
                        var spriteB = objB["sprite"] as Sprite;
                        if(spriteB != null)
                        {
                            bool collision = BoundingBox.AABB(spriteA.bounds, spriteB.bounds);
                            if(collision)
                            {
                                Engine.window.DrawBox(spriteA.bounds, Color.Red);
                                Engine.window.DrawBox(spriteB.bounds, Color.Red);
                            }
                        }
                    }
                }
        }

       
        public static void Update()
        {
            ((Closure)script.Globals["update"]).Call();
            
            foreach(var obj in objects)
            {              
                var distance = Vector2.Distance((Vector2)obj["position"], Engine.window.camera);

                if(distance > Engine.simulationRange)
                {
                    continue;
                }

                ((Closure)obj["update"]).Call(obj);
                
                var sprite = obj["sprite"] as Sprite;

                if(sprite != null)
                {
                    sprite.position = GetProperty<Vector2>("position", obj);
                    sprite.rotation = (float)GetProperty<double>("rotation", obj);
                    Engine.window.DrawSprite(sprite as Sprite);
                    HandleCollisions(obj);
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
