using System;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.IO;
using System.Net;
using System.Net.NetworkInformation;
using System.Runtime.InteropServices;

namespace TQuestLib
{
    public class Core
    {
        public static void intro() // Текст в начале игры
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
            Console.WriteLine("     Unkov Company | www.unkov.su | vk.com/unkovcompany");
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

        public static string sha256hash(string text) // Шифрование ключа сейфа
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

        public static void MissionImpossible() // Музыка в начале игры
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
    }

    public static class Internet
    {
        [DllImport("wininet.dll")]
        static extern bool InternetGetConnectedState(ref InternetConnectionState lpdwFlags, int dwReserved);

        [Flags]
        enum InternetConnectionState : int
        {
            INTERNET_CONNECTION_MODEM = 0x1,
            INTERNET_CONNECTION_LAN = 0x2,
            INTERNET_CONNECTION_PROXY = 0x4,
            INTERNET_RAS_INSTALLED = 0x10,
            INTERNET_CONNECTION_OFFLINE = 0x20,
            INTERNET_CONNECTION_CONFIGURED = 0x40
        }

        static object _syncObj = new object();

        /// <summary>
        /// Проверить, есть ли соединение с интернетом
        /// </summary>
        /// <returns></returns>
        public static Boolean CheckConnection()
        {
            lock (_syncObj)
            {
                try
                {
                    InternetConnectionState flags = InternetConnectionState.INTERNET_CONNECTION_CONFIGURED | 0;
                    bool checkStatus = InternetGetConnectedState(ref flags, 0);

                    if (checkStatus)
                        return PingServer(new string[]
                                            {
                                                @"google.com",
                                                @"microsoft.com",
                                                @"ibm.com"
                                            });

                    return checkStatus;
                }
                catch
                {
                    return false;
                }
            }
        }


        /// <summary>
        /// Пингует сервера, при первом получении ответа от любого сервера возвращает true 
        /// </summary>
        /// <param name="serverList">Список серверов</param>
        /// <returns></returns>
        public static bool PingServer(string[] serverList)
        {
            bool haveAnInternetConnection = false;
            Ping ping = new Ping();
            for (int i = 0; i < serverList.Length; i++)
            {
                PingReply pingReply = ping.Send(serverList[i]);
                haveAnInternetConnection = (pingReply.Status == IPStatus.Success);
                if (haveAnInternetConnection)
                    break;
            }

            return haveAnInternetConnection;
        }
    }
}
