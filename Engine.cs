using System.IO;
using System;
using System.Reflection;
using Newtonsoft.Json;
using MoonSharp.Interpreter;
using BBQLib;
namespace Snowball
{
    public static class Engine
    {
        const string defaultDirectory = "demo";
        public const string engineName = "Sn0wballEngine V6.0";
        public static float deltaTime;

        public static string gameDirectory;

        public const string engineScripts = "engineScripts/";

        public static float simulationRange = 1600;

        private static string defaultBackend
        {
            get
            {
                return "Default Backend";
            }
        }

        static void LoadGameConfig(string directory)
        {
            var config = File.ReadAllLines(directory + "game.conf");
            var width = uint.Parse(config[0]);
            var height = uint.Parse(config[1]);
            var fps = uint.Parse(config[2]);
            var title = config[3];

            WindowConfig winConfig = new WindowConfig()
            {
                width = width,
                height = height,
                fps = fps,
                name = title
            };

            BBQLib.BBQLib.Init(winConfig, BackendType.SFML);
        }


        public static void CreateGameDirectory(string name)
        {
            Engine.gameDirectory = name;
            Directory.CreateDirectory(name);

            string[] importantDirs = new string[]
            {
                DirectoryConsts.scriptDirectory,
                DirectoryConsts.spriteDirectory,
                DirectoryConsts.soundDirectory,
                DirectoryConsts.musicDirectory,
                DirectoryConsts.sceneDirectory,
                DirectoryConsts.prefabDirectory,
                DirectoryConsts.inputDirectory,
                DirectoryConsts.fontDirectory
            };

            Console.WriteLine("Creating new game {0}", name);

            foreach(var dir in importantDirs)
            {
                Console.WriteLine("creating directory {0}", dir);
                Directory.CreateDirectory(dir);
            }
            
            LuaUtils.CreateLuaFile("load.lua");
        }

        public static void Run(string[] args)
        {
            Console.WriteLine("{0} created by BBQGiraffe running on {1}", engineName, Environment.OSVersion);

            Json.InitSettings();
            
            gameDirectory = ((args.Length > 0) ? args[0] : defaultDirectory) + "/";

            
            LoadGameConfig(gameDirectory);
            Input.LoadAxisFile(DirectoryConsts.inputDirectory + "axes.json");
            BBQLib.BBQLib.rootDirectory = gameDirectory;
            BBQLib.BBQLib.LoadFonts("fonts.json");

            Console.WriteLine("starting lua{0}...", Script.LUA_VERSION);
            
            LuaUtils.LoadEngineClasses();
            LuaObject.LoadLua();

            while(BBQLib.BBQLib.IsOpen)
            {
                BBQLib.BBQLib.Clear();
                LuaObject.Update();
                BBQLib.BBQLib.Display();
                GC.Collect();
            }
        }
    }
}