using System.Numerics;
using MoonSharp.Interpreter;
using MoonSharp.VsCodeDebugger;
using System;
using System.IO;
using BBQLib;
namespace Snowball
{
    internal static class LuaUtils
    {
        public static void LoadEngineClasses()
        {
            UserData.RegisterType<Vector2>();
            UserData.RegisterType<KeyboardKey>();
            UserData.RegisterType<BoundingBox>();
            UserData.RegisterType<OriginType>();
            UserData.RegisterType<Sprite>();
            UserData.RegisterType<Color>();
            UserData.RegisterType<LuaBinaryReader>();
            UserData.RegisterType<PhysicsHandler>();
        }

        static LuaBinaryReader LUA_CreateBinaryReader(string filename)
        {
            return new LuaBinaryReader(LUA_ReadBinary(filename));
        }
        


        public static void CreateLuaFile(string name)
        {
            if(File.Exists(DirectoryConsts.scriptDirectory + name)){return;}
            var className = name.Substring(0, name.LastIndexOf('.'));
            string output = File.ReadAllText(Engine.engineScripts + "entitytemplate.lua").Replace("entity", className);
            File.WriteAllText(DirectoryConsts.scriptDirectory + name, output);
        }

        static void SetCamera(Vector2 camera)
        {
            BBQLib.BBQLib.Camera = camera;
        }

        static Sprite LUA_LoadSprite(string filename)
        {
            var sprite = BBQLib.BBQLib.RegisterSprite("sprites/" + filename);
            return sprite;
        }
        
        static void LUA_DrawText(string text, string font, Vector2 position, OriginType type)
        {
            BBQLib.BBQLib.Draw(font, text, position- BBQLib.BBQLib.Camera);
        }

        static void LUA_DrawStaticText(string text, string font, Vector2 position, OriginType type)
        {
            BBQLib.BBQLib.Draw(font, text, position);
        }

        static byte[] LUA_ReadBinary(string filename)
        {
            return File.ReadAllBytes(Engine.gameDirectory + filename);
        }

        static void LUA_SaveBinary(string filename, byte[] data)
        {
            File.WriteAllBytes(Engine.gameDirectory + filename, data);
        }

        static byte[] Allocate(int length)
        {
            return new byte[length];
        }

        public static void LoadGlobalFunctions(Script script, LuaObject obj)
        {
            script.Globals["Allocate"] = (Func<int, byte[]>)Allocate;
            script.Globals["PhysicsHandler"] = (Func<Vector2, float, PhysicsHandler>)PhysicsHandler.Create;
            script.Globals["CreateObject"] = (Func<Table, Table>)LuaObject.AddObject;
            script.Globals["DrawSprite"] = (Action<Sprite>)BBQLib.BBQLib.Draw;
            script.Globals["LoadSprite"] = (Func<string, Sprite>)LUA_LoadSprite;
            //script.Globals["LoadIsometricSprite"] = (Func<string, Sprite>)Sprite.LoadIsometric;
            script.Globals["InputIsKeyDown"] = (Func<KeyboardKey, bool>)Input.IsKeyDown;
            script.Globals["InputGetAxis"] = (Func<string, float>)Input.GetAxis;
            script.Globals["Random"] = (Func<float>)Rng.RandomFloat;
            script.Globals["RandomRange"] = (Func<int, int, int>)Rng.Range;
            script.Globals["DrawText"] = (Action<string, string, Vector2, OriginType>)LUA_DrawText;
            script.Globals["UIDrawText"] = (Action<string, string, Vector2, OriginType>)LUA_DrawStaticText;
            script.Globals["PlaySound"] = (Action<string>)BBQLib.BBQLib.PlaySound;
            script.Globals["ReadBinaryFile"] = (Func<string, byte[]>)LUA_ReadBinary;
            script.Globals["SaveBinaryFile"] = (Action<string, byte[]>)LUA_SaveBinary;
            script.Globals["BinaryReader"] = (Func<string, LuaBinaryReader>)LUA_CreateBinaryReader;
            script.Globals["CreateSpriteFromBuffer"]= (Func<byte[], uint, uint, string, Sprite>)BBQLib.BBQLib.CreateSprite;
            script.Globals["CreatePalettedSprite"] = (Func<byte[], byte[], uint, uint, string, Sprite>)BBQLib.BBQLib.CreateSprite;
            script.Globals["Vec2"] = (Func<float, float, Vector2>)Vec2Utils.CreateVec2;
            script.Globals["SetCameraPosition"] = (Action<Vector2>)SetCamera;
            script.Globals["Vec2GetAngle"] = (Func<Vector2, Vector2, float>)Vec2Utils.GetAngle;
            script.Globals["Vec2GetDirection"] = (Func<Vector2, Vector2, Vector2>)Vec2Utils.GetDirection;
            script.Globals["Vec2GetLength"] = (Func<Vector2, float>)Vec2Utils.Length;
            script.Globals["Vec2Distance"] = (Func<Vector2, Vector2, float>)Vector2.Distance;
            script.Globals["Vec2Normalize"] = (Func<Vector2, Vector2>)Vector2.Normalize;
            script.Globals["Color"] = (Func<float,float,float,float,Color>)Color.CreateColor;
            script.Globals["ColorMix"] = (Func<Color,Color,float,Color>)Color.Mix;
            script.Globals["Color255"] = (Func<byte,byte,byte,byte,Color>)Color.From255RGB;
            //script.Globals["DrawBox"] = (Action<BoundingBox,Color>)Engine.window.DrawBox;
            script.Globals["BoundingBox"] = (Func<Vector2, BoundingBox>)BoundingBox.Create;
            //enums
            script.Globals["OriginType"] = UserData.CreateStatic<OriginType>();
            script.Globals["KeyboardKey"] = UserData.CreateStatic<KeyboardKey>();
            //debug stuff
            script.Globals["DebugGetMemoryUsage"] = (Func<long>)Debug.GetMemoryUsage;
        }

        
    }
}