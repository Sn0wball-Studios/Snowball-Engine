using System.Collections.Generic;
using MoonSharp.Interpreter;
using MoonSharp.Interpreter.Loaders;
using System.Numerics;
using System;
namespace Snowball
{
    public sealed class ScriptedObject
    {
        public string id = "Scripted Object";
        public string tag = "empty";
        public Script script = new Script();
        public Vector2 position = new Vector2();
        public Vector2 velocity = new Vector2();
        public float rotation;

        public BoundingBox boundingBox = new BoundingBox(new Vector2(0,0));

        public static ScriptedObject GetScriptedObjectByID(string id)
        {
            foreach(var scriptedObject in scriptedObjects)
            {
                if(scriptedObject.id.Equals(id))
                {
                    return scriptedObject;
                }
            }

            throw new NullReferenceException(string.Format("could not find object {0}", id));
        }

        public readonly static List<ScriptedObject> scriptedObjects = new List<ScriptedObject>();

        public static ScriptedObject Create(string file, Vector2 position)
        {
            
            ScriptedObject scriptedObject = new ScriptedObject();
            //LuaUtils.LoadGlobalFunctions(scriptedObject.script, scriptedObject);
            scriptedObject.position = position;
            scriptedObject.script.Options.ScriptLoader = new FileSystemScriptLoader()
            {
                ModulePaths = new string[]
                {
                    Engine.scriptDirectory + "?.lua",
                    Engine.engineScripts + "?.lua"
                }
            };
            scriptedObject.script.DoFile(Engine.scriptDirectory + file);
            scriptedObject.DoFunction("start");
            scriptedObjects.Add(scriptedObject);
            return scriptedObject;
        }

        public static ScriptedObject[] GetObjectsWithTag(string objectTag)
        {
            List<ScriptedObject> output = new List<ScriptedObject>();
            foreach(var obj in scriptedObjects)
            {
                if(obj.tag.Equals(objectTag))
                {
                    output.Add(obj);
                }
            }
            return output.ToArray();
        }

        public static ScriptedObject[] GetObjectsWithID(string objectID)
        {
            List<ScriptedObject> output = new List<ScriptedObject>();
            foreach(var obj in scriptedObjects)
            {
                if(obj.id.Equals(objectID))
                {
                    output.Add(obj);
                }
            }
            return output.ToArray();
        }
        
        readonly List<Sprite> sprites = new List<Sprite>();
        
        public void AddSprite(string spriteFile)
        {
            Sprite sprite = Json.Load<Sprite>(Engine.spriteDirectory + spriteFile);
            sprite.Init(spriteFile);
            sprites.Add(sprite);
            boundingBox = new BoundingBox(sprite.size);
        }

        Table scriptedSequenceManager;

        public void SetScriptedSequenceManager(Table manager, ScriptedObject scriptedObject)
        {
            if(scriptedObject.scriptedSequenceManager != null){return;}
            Console.WriteLine("loaded sequence manager for {0}", id);
            scriptedObject.scriptedSequenceManager = manager;
        }
        
        public void Update()
        {
            boundingBox.min = position;
            DoFunction("update");

            
           
            //draw sprites
            foreach(var sprite in sprites)
            {
                sprite.position = position;
                sprite.rotation = rotation;
                Engine.window.DrawSprite(sprite);
            }

            
        }
        void SetGlobals()
        {
            SetProperty("position", position);
            SetProperty("rotation", rotation);
            SetProperty("id", id);
            SetProperty("deltaTime", Engine.deltaTime);
            SetProperty("boundingBox", boundingBox);
            SetProperty("OriginType", UserData.CreateStatic<OriginType>());
            SetProperty("tag", tag);
            SetProperty("KeyboardKey", UserData.CreateStatic<KeyboardKey>());
        }

        void LoadGlobals()
        {
            position = GetProperty<Vector2>("position");
            rotation = (float)GetProperty<double>("rotation");
            id = GetProperty<string>("id");
            tag = GetProperty<string>("tag");
            boundingBox = GetProperty<BoundingBox>("boundingBox");
        }

        public DynValue DoFunction(string name, params object[] args)
        {
            SetGlobals();
            var value = script.Call(script.Globals[name], args);
            LoadGlobals();
            return value;
        }

        public void SetProperty(string name, object value)
        {
            script.Globals[name] = value;
        }

        public T GetProperty<T>(string name)
        {
            return (T)script.Globals[name];
        }

    }
}