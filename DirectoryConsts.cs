namespace Snowball
{
    public static class DirectoryConsts
    {
        public static string spriteDirectory
        {
            get
            {
                return Engine.gameDirectory + "sprites/";
            }
        }

        public static string scriptDirectory
        {
            get
            {
                return Engine.gameDirectory + "lua/";
            }
        }

        public static string soundDirectory
        {
            get
            {
                return Engine.gameDirectory + "sfx/";
            }
        }

        public static string musicDirectory
        {
            get
            {
                return Engine.gameDirectory + "music/";
            }
        }

        public static string inputDirectory
        {
            get
            {
                return Engine.gameDirectory + "input/";
            }
        }

        public static string fontDirectory
        {
            get
            {
                return Engine.gameDirectory + "fonts/";
            }
        }

        public static string sceneDirectory
        {
            get
            {
                return Engine.gameDirectory + "scenes/";
            }
        }

        public static string prefabDirectory
        {
            get
            {
                return Engine.gameDirectory + "prefabs/";
            }
        }

        public static string dllDirectory
        {
            get
            {
                return "backends/";
            }
        }

        private static string defaultBackend
        {
            get
            {
                return "Default Backend";
            }
        }
    }
}