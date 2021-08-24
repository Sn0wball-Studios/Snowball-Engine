using MoonSharp.Interpreter;
using MoonSharp.Interpreter.Loaders;
using System.IO;
using System;
namespace Snowball
{
    internal class SnowballScriptLoader : ScriptLoaderBase
    {
        public override object LoadFile(string file, Table globalContext)
        {
            return File.ReadAllText(DirectoryConsts.scriptDirectory + file);

        }

        public override bool ScriptFileExists(string name)
        {
            return File.Exists(DirectoryConsts.scriptDirectory + name);
        }
    }
}