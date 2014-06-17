using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Hive5
{
    public class Logger
    {
        public static bool IsDebugBuild { get; private set; }

        static Logger()
        {
#if DEBUG
            IsDebugBuild = true;
#else
            IsDebugBuild = false;
#endif
        }

        public static void Log(string message) { Logger.WriteLine(message); }
        public static void Log(string message, Object context) { Logger.WriteLine(message); }

        public static void LogWarning(string s) { Logger.WriteLine(s); }
        public static void LogWarning(string s, Object context) { Logger.WriteLine(s); }
        public static void LogError(string s) { Logger.WriteLine(s); }
        public static void LogError(string s, Object context) { Logger.WriteLine(s); }

        public static void LogException(Exception ex) { Logger.WriteLine(ex.Message); }
        public static void LogException(Exception ex, Object context) { Logger.WriteLine(ex.Message); }

        public static void WriteLine(string s)
        {
#if TEST
            Console.WriteLine(s);
#else
            UnityEngine.Debug.Log(s);            
#endif
        }
    }
}
