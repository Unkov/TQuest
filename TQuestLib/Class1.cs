using System;
using System.Security.Cryptography;
using System.Text;

namespace TQuestLib
{
    public class Core
    {
        public static void intro()
        {
            Console.WriteLine("     █████   █    ██ ▓█████   ██████ ▄▄▄█████▓");
            Console.WriteLine("   ▒██▓  ██▒ ██  ▓██▒▓█   ▀ ▒██    ▒ ▓  ██▒ ▓▒");
            Console.WriteLine("   ▒██▒  ██░▓██  ▒██░▒███   ░ ▓██▄   ▒ ▓██░ ▒░");
            Console.WriteLine("   ░██  █▀ ░▓▓█  ░██░▒▓█  ▄   ▒   ██▒░ ▓██▓ ░ ");
            Console.WriteLine("   ░▒███▒█▄ ▒▒█████▓ ░▒████▒▒██████▒▒  ▒██▒ ░ ");
            Console.WriteLine("   ░░ ▒▒░ ▒ ░▒▓▒ ▒ ▒ ░░ ▒░ ░▒ ▒▓▒ ▒ ░  ▒ ░░   ");
            Console.WriteLine("    ░ ▒░  ░ ░░▒░ ░ ░  ░ ░  ░░ ░▒  ░ ░    ░    ");
            Console.WriteLine("      ░   ░  ░░░ ░ ░    ░   ░  ░  ░    ░      ");
            Console.WriteLine("       ░       ░        ░  ░      ░           ");
            Console.WriteLine("                                              ");
            Console.WriteLine("     Developed by MineCR & Nikolay Yurchenko github.com/kolya112/TQuest");
        }

        public static void print(string text, ConsoleColor color = ConsoleColor.White, ConsoleColor color2 = ConsoleColor.White)
        {
            Console.ForegroundColor = color;
            Console.Write(text);
            Console.ForegroundColor = color2;
        }

        public static void printl(string text, ConsoleColor color = ConsoleColor.White, ConsoleColor color2 = ConsoleColor.White)
        {
            Console.ForegroundColor = color;
            Console.WriteLine(text);
            Console.ForegroundColor = color2;
        }

        public static string sha256hash(string text)
        {
            StringBuilder oResHash = new StringBuilder();

            using (SHA256 oHash = SHA256Managed.Create())
            {
                Encoding oEnc = Encoding.UTF8;
                byte[] baResult = oHash.ComputeHash(oEnc.GetBytes(text));

                foreach (byte b in baResult)
                    oResHash.Append(b.ToString("x2"));
            }

            return oResHash.ToString();
        }
    }
}
