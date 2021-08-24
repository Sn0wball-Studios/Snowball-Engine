using System.Numerics;
using MoonSharp.Interpreter;
using MoonSharp.VsCodeDebugger;
using System;
using System.IO;
namespace Snowball
{
    internal static class LuaUtils
    {
        public static void LoadEngineClasses()
        {
            UserData.RegisterType<Vector2>();
            UserData.RegisterType<ScriptedObject>();
            UserData.RegisterType<SoundSource>();
            UserData.RegisterType<SoundFactory>();
            UserData.RegisterType<KeyboardKey>();
            UserData.RegisterType<BoundingBox>();
            UserData.RegisterType<OriginType>();
            UserData.RegisterType<Script>();
            UserData.RegisterType<Sprite>();
        }

        
        static float LUA_GetDeltaTime()
        {
            return Engine.deltaTime;
        }
        


        public static void CreateLuaFile(string name)
        {
            if(File.Exists(Engine.scriptDirectory + name)){return;}
            var className = name.Substring(0, name.LastIndexOf('.'));
            string output = File.ReadAllText(Engine.engineScripts + "entitytemplate.lua").Replace("entity", className);
            File.WriteAllText(Engine.scriptDirectory + name, output);
        }

        

        public static void LoadGlobalFunctions(Script script, LuaObject obj)
        {
            script.Globals["CreateObject"] = (Func<Table, Table>)LuaObject.AddObject;
            script.Globals["DrawSprite"] = (Action<Sprite>)Engine.window.DrawSprite;
            script.Globals["dt"] = (Func<float>)LUA_GetDeltaTime;
            script.Globals["LoadSprite"] = (Func<string, Sprite>)Sprite.LoadSprite;
            script.Globals["LoadIsometricSprite"] = (Func<string, Sprite>)Sprite.LoadIsometric;
            script.Globals["CreateSoundSource"] = (Func<string, SoundSource>)Engine.soundFactory.CreateSource;
            script.Globals["CreateScriptableObject"] = (Func<string, Vector2, ScriptedObject>)ScriptedObject.Create;
            script.Globals["InputIsKeyDown"] = (Func<KeyboardKey, bool>)Input.IsKeyDown;
            script.Globals["InputGetAxis"] = (Func<string, float>)Input.GetAxis;
            script.Globals["GetScriptedObjectByID"] = (Func<string, ScriptedObject>)ScriptedObject.GetScriptedObjectByID;
            script.Globals["Random"] = (Func<float>)Rng.RandomFloat;
            script.Globals["RandomRange"] = (Func<int, int, int>)Rng.Range;
            script.Globals["DrawText"] = (Action<string, string, Vector2, OriginType>)Engine.window.DrawText;
            script.Globals["UIDrawText"] = (Action<string, string, Vector2, OriginType>)Engine.window.UIDrawText;
            script.Globals["GetObjectsWithID"] = (Func<string, ScriptedObject[]>)ScriptedObject.GetObjectsWithID;
            script.Globals["GetObjectsWithTag"] = (Func<string, ScriptedObject[]>)ScriptedObject.GetObjectsWithTag;
            script.Globals["Vec2"] = (Func<float, float, Vector2>)Vec2Utils.CreateVec2;
            script.Globals["SetCameraPosition"] = (Action<Vector2>)Engine.window.SetCamera;
            script.Globals["Vec2GetAngle"] = (Func<Vector2, Vector2, float>)Vec2Utils.GetAngle;
            script.Globals["Vec2GetDirection"] = (Func<Vector2, Vector2, Vector2>)Vec2Utils.GetDirection;
            script.Globals["Vec2GetLength"] = (Func<Vector2, float>)Vec2Utils.Length;
            script.Globals["Vec2Distance"] = (Func<Vector2, Vector2, float>)Vec2Utils.Distance;

            //enums
            script.Globals["OriginType"] = UserData.CreateStatic<OriginType>();

            //debug stuff
            script.Globals["DebugGetMemoryUsage"] = (Func<long>)Debug.GetMemoryUsage;

            //fast math
            script.Globals["sin"] = (Func<float, float>)FastMath.Sin;
        }

        
    }
}