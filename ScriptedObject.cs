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
        public readonly List<ObjectManipulator> manipulators = new List<ObjectManipulator>();
        public readonly List<ObjectTargetManipulator> newManipulators = new List<ObjectTargetManipulator>();
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
                    Engine.scriptDirectory + "?.lua"
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

            foreach(var manipulator in newManipulators)
            {
                manipulators.Add(manipulator);
            }
            newManipulators.Clear();


            for(int i = 0; i < manipulators.Count; i++)
            {
                
                if(manipulators[i].Update())
                {
                    if(scriptedSequenceManager == null)
                    {
                        Console.WriteLine("attempting to call scripted sequence on {0} but {0} does not have a scripted sequence manager", id);
                        break;
                    }

                    ((Closure)(scriptedSequenceManager["IssueNextTask"]))?.Call(this);                     
                }
            }
            
            List<ObjectManipulator> remove = new List<ObjectManipulator>();
            for(int i = 0 ; i < manipulators.Count; i++)
            {
                if(manipulators[i].complete)
                {
                    remove.Add(manipulators[i]);
                }
            }
            
            foreach(var i in remove)
            {
                manipulators.Remove(i);
                
            }

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