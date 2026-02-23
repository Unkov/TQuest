using System;
using System.IO;

namespace TQuest
{
    class Global
    {
        public const string title = "[ TQuest 1 ]";
        public const string textv = "═";
        public const string texth = "║";
        public static string spath = Directory.GetCurrentDirectory() + "\\common\\saves.ini";
        public static bool oldVersion = false;
        public static int numberKey = -1;
    }
}
