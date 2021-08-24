using System;

namespace Snowball
{
    class Program
    {
        static void CreationState()
        {
            Console.WriteLine("please enter project name");

            Engine.gameDirectory = Console.ReadLine() + "/";
            Console.WriteLine("1 - Create Prefab\n2 - Create Script\n");

            var command = int.Parse(Console.ReadLine());

            switch(command)
            {
                case 1:
                    goto createprefab;
            }

            createprefab:
                Console.WriteLine("Prefab Name?");
                var name = Console.ReadLine();
                Console.WriteLine("lua file?");
                var lua = Console.ReadLine();
                Console.WriteLine("editor icon?");
                var icon = Console.ReadLine();
                icon = (icon.Length > 0) ? icon : "empty.json";

                var prefabs = Json.LoadDirectory<Prefab>(Engine.prefabDirectory);

                uint maxID = 0;
                foreach(var prefab in prefabs)
                {
                    if(prefab.editorID > maxID){maxID = prefab.editorID;}
                }

                Prefab p = new Prefab();
                p.editorID = maxID+1;
                p.luaScript = lua;
                p.editorGraphic = icon;

                Json.Save(Engine.prefabDirectory + name + ".json", p);
                LuaUtils.CreateLuaFile(lua);

        }

        static void Main(string[] args)
        {
            if(args.Length > 0)
            {
                if(args[0].Equals("create") && args.Length < 2)
                {
                    CreationState();
                   
                    
                    return;
                }
                else if (args[0].Equals("create") && args.Length >= 2)
                {
                    if(args[1].Equals("game"))
                    {
                        Engine.CreateGameDirectory(args[2] + "/");
                    }
                    
                }
                return;
            }
            Engine.Run(args);
        }
    }
}
