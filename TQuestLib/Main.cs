using System;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Runtime.InteropServices;
using System.Net.Http;
using System.Threading.Tasks;

namespace TQuestLib
{
    public class Core
    {
        /// <summary>
        /// Logo at the beginning of the game
        /// </summary>
        public static void intro()
        {
            Console.WriteLine("▄▄▄█████▓ █████   █    ██ ▓█████   ██████ ▄▄▄█████▓    ██▒   ");
            Console.WriteLine("▓  ██▒ ▓▒▒██▓  ██▒ ██  ▓██▒▓█   ▀ ▒██    ▒ ▓  ██▒ ▓▒  ▓███▒   ");
            Console.WriteLine("▒ ▓██░ ▒░▒██▒  ██░▓██  ▒██░▒███   ░ ▓██▄   ▒ ▓██░ ▒░  ░▒██▒   ");
            Console.WriteLine("░ ▓██▓ ░ ░██  █▀ ░▓▓█  ░██░▒▓█  ▄   ▒   ██▒░ ▓██▓ ░    ░██░   ");
            Console.WriteLine("  ▒██▒ ░ ░▒███▒█▄ ▒▒█████▓ ░▒████▒▒██████▒▒  ▒██▒ ░    ░██░   ");
            Console.WriteLine("  ▒ ░░   ░░ ▒▒░ ▒ ░▒▓▒ ▒ ▒ ░░ ▒░ ░▒ ▒▓▒ ▒ ░  ▒ ░░      ░▓     ");
            Console.WriteLine("    ░     ░ ▒░  ░ ░░▒░ ░ ░  ░ ░  ░░ ░▒  ░ ░    ░       ▒ ░   ");
            Console.WriteLine("  ░         ░   ░  ░░░ ░ ░    ░   ░  ░  ░    ░         ░     ");
            Console.WriteLine("          ░       ░        ░  ░      ░                 ");
            Console.WriteLine("                                              ");
            Console.WriteLine("     Unkov Company | unkov.su | vk.com/unkovcompany");
            Console.WriteLine("              unkov.su/projects/tquest1");
        }

        /// <summary>
        /// Print formatting text
        /// </summary>
        /// <param name="text"></param>
        /// <param name="color"></param>
        /// <param name="color2"></param>
        public static void print(string text, ConsoleColor color = ConsoleColor.White, ConsoleColor color2 = ConsoleColor.White)
        {
            Console.ForegroundColor = color;
            Console.Write(text);
            Console.ForegroundColor = color2;
        }

        /// <summary>
        /// Print formatting text and new line
        /// </summary>
        /// <param name="text"></param>
        /// <param name="color"></param>
        /// <param name="color2"></param>
        public static void printl(string text, ConsoleColor color = ConsoleColor.White, ConsoleColor color2 = ConsoleColor.White)
        {
            Console.ForegroundColor = color;
            Console.WriteLine(text);
            Console.ForegroundColor = color2;
        }

        /// <summary>
        /// Encrypt code of safe
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public static string sha256hash(string text)
        {
            StringBuilder oResHash = new StringBuilder();

            using (SHA256 oHash = SHA256.Create())
            {
                Encoding oEnc = Encoding.UTF8;
                byte[] baResult = oHash.ComputeHash(oEnc.GetBytes(text));

                foreach (byte b in baResult)
                    oResHash.Append(b.ToString("x2"));
            }

            return oResHash.ToString();
        }


        /// <summary>
        /// Music at the beginning of the game
        /// </summary>
        public static void MissionImpossible()
        {
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                Console.Beep(784, 150);
                Thread.Sleep(300);
                Console.Beep(784, 150);
                Thread.Sleep(300);
                Console.Beep(932, 150);
                Thread.Sleep(150);
                Console.Beep(1047, 150);
                Thread.Sleep(150);
                Console.Beep(784, 150);
                Thread.Sleep(300);
                Console.Beep(784, 150);
                Thread.Sleep(300);
                Console.Beep(699, 150);
                Thread.Sleep(150);
                Console.Beep(740, 150);
                Thread.Sleep(150);
                Console.Beep(784, 150);
                Thread.Sleep(300);
                Console.Beep(784, 150);
                Thread.Sleep(300);
                Console.Beep(932, 150);
                Thread.Sleep(150);
                Console.Beep(1047, 150);
                Thread.Sleep(150);
                Console.Beep(784, 150);
                Thread.Sleep(300);
                Console.Beep(784, 150);
                Thread.Sleep(300);
                Console.Beep(699, 150);
            }
            else
            {
                Console.Beep();
                Thread.Sleep(9000);
            }
        }
    }

    public static class Internet
    {
        /// <summary>
        /// Check the Internet connection available
        /// </summary>
        /// <returns></returns>
        public static async Task<bool> CheckConnection()
        {
            var checkConnectionRequest = new HttpRequestMessage(HttpMethod.Head, new Uri("http://connectivitycheck.gstatic.com/generate_204"));
            using var hClient = new HttpClient();
            hClient.Timeout = TimeSpan.FromSeconds(5);
            try
            {
                using var hResponse = await hClient.SendAsync(checkConnectionRequest);

                return hResponse.IsSuccessStatusCode;
            }
            catch
            {
                return false;
            }

        }
    }
}
