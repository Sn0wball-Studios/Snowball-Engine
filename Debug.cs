using System.Diagnostics;
using System;
namespace Snowball
{
    internal static class Debug
    {
        static Process process = Process.GetCurrentProcess();
        public static long GetMemoryUsage()
        {
            return process.PrivateMemorySize64;
        }
    }
}