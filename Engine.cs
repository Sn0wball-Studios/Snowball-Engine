using System.IO;
using System;
using System.Reflection;
using Newtonsoft.Json;
using MoonSharp.Interpreter;
namespace Snowball
{
    public static class Engine
    {
        public static WindowImplementation window;
        public static SoundFactory soundFactory;

        const string defaultDirectory = "demo";
        public const string engineName = "Sn0wballEngine V6.0";
        public static float deltaTime;

        public static string gameDirectory;

        public const string engineScripts = "engineScripts/";

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

            window.Create(width, height, title, fps);

        }

        public static void LoadEngineConfig(string file)
        {
            var config = Json.Load<EngineConfig>(DirectoryConsts.dllDirectory + file);
            if(config.name.Equals(defaultBackend))
            {
                LoadDefaultConfig();
            }

             Console.WriteLine("Loading engine backend {0}...", config.name);
             var asm = Assembly.LoadFile(Path.GetFullPath(DirectoryConsts.dllDirectory + config.engineDLL));
             var winType = asm.GetType(config.window);
             var soundFactoryType = asm.GetType(config.soundFactory);
            
             window = Activator.CreateInstance(winType) as WindowImplementation;
             soundFactory = Activator.CreateInstance(soundFactoryType) as SoundFactory;
        }

        static void LoadDefaultConfig()
        {
            Console.WriteLine("Loading default SFML engine backend...");
            window = new SFMLWindow();
            soundFactory = new SFMLSoundFactory();
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

        public static EngineConfig config;


        public static void Run(string[] args)
        {
            Console.WriteLine("{0} created by BBQGiraffe running on {1}", engineName, Environment.OSVersion);

            Json.InitSettings();
            
            gameDirectory = ((args.Length > 0) ? args[0] : defaultDirectory) + "/";
            

            if(args.Length > 1)
            {
                LoadEngineConfig(args[1]);
            }else{
                LoadDefaultConfig();
            }

            
            LoadGameConfig(gameDirectory);
            Input.LoadDefs();

            window.LoadFonts();

            Console.WriteLine("starting lua{0}...", Script.LUA_VERSION);
            
            LuaUtils.LoadEngineClasses();
            LuaObject.LoadLua();
            while(window.IsOpen())
            {
                window.PollEvents();
                Input.ProcessInputs();
                window.Clear();
                deltaTime = window.DeltaTime();
                LuaObject.Update();
                window.Present();

                GC.Collect();
            }
        }
    }
}