using INIManager;
using System;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using TQuest.Locales;
using TQuestLib;

namespace TQuest
{
    internal class Program
    {
        internal static HubManager saves = new HubManager(Global.spath);

        private static void ExitHandler(object sender, EventArgs e)
        {
            saves.WriteString("main", "1-4", "0");
            saves.WriteString("main", "2-4", "0");
            saves.WriteString("main", "4-1", "0");
            saves.WriteString("main", "3-4", "0");
            saves.WriteString("main", "5-4", "0");
        }

        static async Task Main(string[] args)
        {
            try
            {
                if (!File.Exists(Global.spath) || (args.Length == 1 && args[0] == "--config-reset"))
                {
                    Directory.CreateDirectory(Path.GetDirectoryName(Global.spath));
                    await File.WriteAllTextAsync(Global.spath, $"[main]{Environment.NewLine}1-4=0{Environment.NewLine}2-4=0{Environment.NewLine}4-1=0{Environment.NewLine}3-4=0{Environment.NewLine}5-4=0{Environment.NewLine}key=0{Environment.NewLine}");
                }

            }
            catch (Exception ex)
            {
                Core.printl("[FATAL ERROR] The program failed to write the configuration file. Please make screenshot and contact the developers via email: support@unkov.su | Программе не удалось записать файл конфигурации. Пожалуйста, сделайте скриншот и свяжитесь с разработчиками по электронной почте: support@unkov.su", ConsoleColor.Red);
                Core.printl($"Exception message: {ex.Message} | Source: {ex.Source} | StackTrace: {ex.StackTrace}");
                Core.printl("Press any key to exit.....");
                Console.ReadKey();
                Environment.Exit(1);
            }

            // Internet connection and update available check
            bool isInternetConnected = await Internet.CheckConnection();
            if (isInternetConnected)
            {
                using var hClient = new HttpClient();
                var gameBinaryVersion = FileVersionInfo.GetVersionInfo(Assembly.GetExecutingAssembly().Location);
                string version = gameBinaryVersion.FileVersion;
                using var hRequestServerVersion = await hClient.GetAsync(new Uri("https://api.unkov.su/TQuest/lversion.php"));
                string sVersion = await hRequestServerVersion.Content.ReadAsStringAsync();
                if (version != sVersion)
                    Global.oldVersion = true;
            }

            // Exit Handler
            AppDomain.CurrentDomain.ProcessExit += new EventHandler(ExitHandler);

            // Localization settings for CIS community
            var currentCulture = CultureInfo.CurrentUICulture;
            string[] cisLangs = { "be", "kk", "ky", "tg", "uz", "hy", "az", "uk" };
            if (cisLangs.Any(lang => currentCulture.Name.StartsWith(lang)))
                CultureInfo.DefaultThreadCurrentUICulture = new CultureInfo("ru");

            // Game Start
            Program p = new Program();
            await p.Start();
        }

        // Game Start Point
        public async Task Start()
        {
            var random = new Random();
            Global.numberKey = random.Next(0, 6);
            
            Console.Title = "TQuest 1";
            Core.printl(Strings.welcomemessage, ConsoleColor.Red);
            Thread.Sleep(1500);
            Console.Clear();

            Core.intro();
            Core.MissionImpossible();
            Console.Clear();

            if (Global.oldVersion)
                RoomOne_1(Strings.oldVersionMessage);

            RoomOne_1();
        }

        static void RoomOne_1(string msg = "") // Коридор
        {
            if (msg != "") Core.printl(msg);
            for (int i = 0; i < 50; i++) if (i == 25) Core.print(Global.title); else Core.print(Global.textv); // Вывод самой верхней строки
            Core.printl("");
            Core.printl(Strings.room1_description);
            for (int i = 0; i < 49; i++) if (i == 25) Core.print(Strings.inventory); else Core.print(Global.textv); // Вывод отделения между описанием комнаты и инвентарём
            Core.printl("");

            if (saves.GetString("main", "1-4") == "1") Core.printl(Strings.notice14);
            if (saves.GetString("main", "2-4") == "1") Core.printl(Strings.notice24);
            if (saves.GetString("main", "4-1") == "1") Core.printl(Strings.notice41);
            if (saves.GetString("main", "3-4") == "1") Core.printl(Strings.notice34);
            if (saves.GetString("main", "5-4") == "1") Core.printl(Strings.notice54);
            if (saves.GetString("main", "1-4") == "1" || saves.GetString("main", "2-4") == "1" || saves.GetString("main", "4-1") == "1" || saves.GetString("main", "3-4") == "1" || saves.GetString("main", "5-4") == "1")
            {

            }
            else Core.printl(Strings.inventorynone);

            for (int i = 0; i < 50; i++) if (i == 25) Core.print(Strings.variants); else Core.print(Global.textv); // Вывод отделения между инвентарём и вариантами
            Core.printl("");
            Core.printl("1)" + Strings.room1_action1);
            Core.printl("2)" + Strings.room1_action2);
            Core.printl("3)" + Strings.room1_action3);
            Core.printl("4)" + Strings.room1_action4);
            Core.printl("5)" + Strings.room1_action5);
            Core.printl("6)" + Strings.room1_action6);
            Core.printl("7)" + Strings.room1_action7);
            Core.printl(Strings.typeneedaction);
            string result = Console.ReadLine();
            if (result != null)
            {
                if (result == "1)" || result == "1")
                {
                    Console.Clear();
                    RoomOne_1(Strings.nothingfind);
                }

                else if (result == "2)" || result == "2")
                {
                    if (saves.GetString("main", "1-4") == "1")
                    {
                        Console.Clear();
                        RoomOne_1(Strings.room1_notice14_finded);
                    }
                    Console.Clear();
                    saves.WriteString("main", "1-4", "1");
                    RoomOne_1(Strings.room1_notice14_find);
                }

                else if (result == "3)" || result == "3")
                {
                    if (saves.GetString("main", "key") == "1")
                    {
                        Console.Clear();
                        for (int i = 0; i < 50; i++) if (i == 25) Core.print(Strings.gameover); else Core.print(Global.textv);
                        Core.printl(Strings.gameovertext);
                        Console.ReadKey();
                    }
                    else Console.Clear(); RoomOne_1(Strings.needkey);
                }

                else if (result == "4)" || result == "4")
                {
                    RoomTwo_2();
                }

                else if (result == "5)" || result == "5")
                {
                    RoomThree_3();
                }

                else if (result == "6)" || result == "6")
                {
                    RoomFour_4();
                }

                else if (result == "7)" || result == "7")
                {
                    RoomFive_5();
                }

                else RoomOne_1(Strings.choosecorrectvariant);
            }
            else RoomOne_1(Strings.choosecorrectvariant);
        }

        static void RoomTwo_2(string msg = "") // Кухня
        {
            Console.Clear();
            if (msg != "") Core.printl(msg);
            for (int i = 0; i < 50; i++) if (i == 25) Core.print(Global.title); else Core.print(Global.textv); // Вывод самой верхней строки
            Core.printl("");
            Core.printl(Strings.room2_description);
            for (int i = 0; i < 50; i++) if (i == 25) Core.print(Strings.inventory); else Core.print(Global.textv); // Вывод отделения между описанием комнаты и инвентарём
            Core.printl("");

            if (saves.GetString("main", "1-4") == "1") Core.printl(Strings.notice14);
            if (saves.GetString("main", "2-4") == "1") Core.printl(Strings.notice24);
            if (saves.GetString("main", "4-1") == "1") Core.printl(Strings.notice41);
            if (saves.GetString("main", "3-4") == "1") Core.printl(Strings.notice34);
            if (saves.GetString("main", "5-4") == "1") Core.printl(Strings.notice54);
            if (saves.GetString("main", "1-4") == "1" || saves.GetString("main", "2-4") == "1" || saves.GetString("main", "4-1") == "1" || saves.GetString("main", "3-4") == "1" || saves.GetString("main", "5-4") == "1")
            {

            }
            else Core.printl(Strings.inventorynone);

            for (int i = 0; i < 50; i++) if (i == 25) Core.print(Strings.variants); else Core.print(Global.textv); // Вывод отделения между инвентарём и вариантами
            Core.printl("");
            Core.printl("1)" + Strings.room2_action1);
            Core.printl("2)" + Strings.room2_action2);
            Core.printl("3)" + Strings.room2_action3);
            Core.printl("4)" + Strings.room2_action4);
            Core.printl("5)" + Strings.room2_action5);
            Core.printl(Strings.typeneedaction);
            string result = Console.ReadLine();
            if (result != null)
            {
                if (result == "1" || result == "1)")
                {
                    Console.Clear();
                    RoomTwo_2(Strings.nothingfind);
                }

                else if (result == "2" || result == "2)")
                {
                    if (saves.GetString("main", "2-4") == "1")
                    {
                        Console.Clear();
                        RoomTwo_2(Strings.room2_notice24_finded);
                    }
                    Console.Clear();
                    saves.WriteString("main", "2-4", "1");
                    RoomTwo_2(Strings.room2_notice24_find);
                }

                else if (result == "3" || result == "3)")
                {
                    Console.Clear();
                    RoomTwo_2(Strings.nothingfind);
                }

                else if (result == "4" || result == "4)")
                {
                    Console.Clear();
                    RoomOne_1();
                }

                else if (result == "5" || result == "5)")
                {
                    RoomSeven_7();
                }

                else RoomTwo_2(Strings.choosecorrectvariant);
            }
            else RoomTwo_2(Strings.choosecorrectvariant);
        }

        static void RoomThree_3(string msg = "") // Кабинет
        {
            Console.Clear();
            if (msg != "") Core.printl(msg);
            for (int i = 0; i < 50; i++) if (i == 25) Core.print(Global.title); else Core.print(Global.textv); // Вывод самой верхней строки
            Core.printl("");
            Core.printl(Strings.room3_description);
            for (int i = 0; i < 50; i++) if (i == 25) Core.print(Strings.inventory); else Core.print(Global.textv); // Вывод отделения между описанием комнаты и инвентарём
            Core.printl("");

            if (saves.GetString("main", "1-4") == "1") Core.printl(Strings.notice14);
            if (saves.GetString("main", "2-4") == "1") Core.printl(Strings.notice24);
            if (saves.GetString("main", "4-1") == "1") Core.printl(Strings.notice41);
            if (saves.GetString("main", "3-4") == "1") Core.printl(Strings.notice34);
            if (saves.GetString("main", "5-4") == "1") Core.printl(Strings.notice54);
            if (saves.GetString("main", "1-4") == "1" || saves.GetString("main", "2-4") == "1" || saves.GetString("main", "4-1") == "1" || saves.GetString("main", "3-4") == "1" || saves.GetString("main", "5-4") == "1")
            {

            }
            else Core.printl(Strings.inventorynone);

            for (int i = 0; i < 50; i++) if (i == 25) Core.print(Strings.variants); else Core.print(Global.textv); // Вывод отделения между инвентарём и вариантами
            Core.printl("");
            Core.printl("1)" + Strings.room3_action1);
            Core.printl("2)" + Strings.room3_action2);
            Core.printl("3)" + Strings.room3_action3);
            Core.printl("4)" + Strings.room3_action4);
            Core.printl(Strings.typeneedaction);
            Core.printl(Global.numberKey.ToString());
            string result = Console.ReadLine();

            if (result != null)
            {
                if (result == "1)" || result == "1")
                {
                    if (saves.GetString("main", "4-1") == "1")
                    {
                        RoomThree_3(Strings.room3_notice41_finded);
                    }
                    saves.WriteString("main", "4-1", "1");
                    RoomThree_3(Strings.room3_notice41_find);
                }

                else if (result == "2)" || result == "2")
                {
                    RoomThree_3(Strings.nothingfind);
                }

                else if (result == "3)" || result == "3")
                {
                    if (saves.GetString("main", "key") == "1") RoomThree_3(Strings.searchthis);
                    string[] passwords = { "e72c72c6748ea2bb59ebf9d79231abe18e7bd608cf524d0f9e438e542d39e38a", "6e5d967631bbc0c7d36eefbe398a75323cdd18b2a6e180b95b42add033524cac", "7400bc38db9838278e42498a258f124a1aa2ddaa844bf0a4ba478a709f98c0d1", "61304c99d645456f73c0126dfc69c6de249d52b816078bf2ec66ea0a1322e1aa", "eab5cab790f3bfbb6a5169c6f776aa7ae10637fbd2b61fcc1cc58a501c018728" };
                    Console.Clear();
                    Core.printl(Strings.room3_password);
                    string password = Console.ReadLine();
                    if (string.IsNullOrEmpty(password))
                    {
                        string hashedpassword = Core.sha256hash(password);
                        string unhashedpassword = passwords[Global.numberKey];
                        if (hashedpassword == unhashedpassword)
                        {
                            Core.printl(Strings.room3_acceptpassword);
                            saves.WriteString("main", "key", "1");
                            Thread.Sleep(15000);
                            RoomThree_3();
                        }
                        else RoomThree_3(Strings.room3_wrongpassword);
                    }
                    else RoomThree_3(Strings.room3_wrongpassword);
                }

                else if (result == "4)" || result == "4")
                {
                    Console.Clear();
                    RoomOne_1();
                }

                else RoomThree_3(Strings.choosecorrectvariant);
            }
            else RoomThree_3(Strings.choosecorrectvariant);
        }

        static void RoomFour_4(string msg = "") // Спальня
        {
            Console.Clear();
            if (msg != "") Core.printl(msg);
            for (int i = 0; i < 50; i++) if (i == 25) Core.print(Global.title); else Core.print(Global.textv); // Вывод самой верхней строки
            Core.printl("");
            Core.printl(Strings.room4_description);
            for (int i = 0; i < 50; i++) if (i == 25) Core.print(Strings.inventory); else Core.print(Global.textv); // Вывод отделения между описанием комнаты и инвентарём
            Core.printl("");

            if (saves.GetString("main", "1-4") == "1") Core.printl(Strings.notice14);
            if (saves.GetString("main", "2-4") == "1") Core.printl(Strings.notice24);
            if (saves.GetString("main", "4-1") == "1") Core.printl(Strings.notice41);
            if (saves.GetString("main", "3-4") == "1") Core.printl(Strings.notice34);
            if (saves.GetString("main", "5-4") == "1") Core.printl(Strings.notice54);
            if (saves.GetString("main", "1-4") == "1" || saves.GetString("main", "2-4") == "1" || saves.GetString("main", "4-1") == "1" || saves.GetString("main", "3-4") == "1" || saves.GetString("main", "5-4") == "1")
            {

            }
            else Core.printl(Strings.inventorynone);

            for (int i = 0; i < 50; i++) if (i == 25) Core.print(Strings.variants); else Core.print(Global.textv); // Вывод отделения между инвентарём и вариантами
            Core.printl("");
            Core.printl("1)" + Strings.room4_action1);
            Core.printl("2)" + Strings.room4_action2);
            Core.printl("3)" + Strings.room4_action3);
            Core.printl("4)" + Strings.room4_action4);
            Core.printl(Strings.typeneedaction);
            string result = Console.ReadLine();

            if (result != null)
            {
                if (result == "1)" || result == "1")
                {
                    RoomFour_4(Strings.nothingfind);
                }

                else if (result == "2)" || result == "2")
                {
                    RoomFour_4(Strings.nothingfind);
                }

                else if (result == "3)" || result == "3")
                {
                    Console.Clear();
                    RoomFour_4(Strings.nothingfind);
                }

                else if (result == "4)" || result == "4")
                {
                    Console.Clear();
                    RoomOne_1();
                }

                else RoomFour_4(Strings.choosecorrectvariant);
            }
            else RoomFour_4(Strings.choosecorrectvariant);
        }

        static void RoomFive_5(string msg = "") // Чердак
        {
            Console.Clear();
            if (msg != "") Core.printl(msg);
            for (int i = 0; i < 50; i++) if (i == 25) Core.print(Global.title); else Core.print(Global.textv); // Вывод самой верхней строки
            Core.printl("");
            Core.printl(Strings.room5_description);
            for (int i = 0; i < 50; i++) if (i == 25) Core.print(Strings.inventory); else Core.print(Global.textv); // Вывод отделения между описанием комнаты и инвентарём
            Core.printl("");

            if (saves.GetString("main", "1-4") == "1") Core.printl(Strings.notice14);
            if (saves.GetString("main", "2-4") == "1") Core.printl(Strings.notice24);
            if (saves.GetString("main", "4-1") == "1") Core.printl(Strings.notice41);
            if (saves.GetString("main", "3-4") == "1") Core.printl(Strings.notice34);
            if (saves.GetString("main", "5-4") == "1") Core.printl(Strings.notice54);
            if (saves.GetString("main", "1-4") == "1" || saves.GetString("main", "2-4") == "1" || saves.GetString("main", "4-1") == "1" || saves.GetString("main", "3-4") == "1" || saves.GetString("main", "5-4") == "1")
            {

            }
            else Core.printl(Strings.inventorynone);

            for (int i = 0; i < 50; i++) if (i == 25) Core.print(Strings.variants); else Core.print(Global.textv); // Вывод отделения между инвентарём и вариантами
            Core.printl("");
            Core.printl("1)" + Strings.room5_action1);
            Core.printl("2)" + Strings.room5_action2);
            Core.printl("3)" + Strings.room5_action3);
            Core.printl("4)" + Strings.room5_action4);
            Core.printl("5)" + Strings.room5_action5);
            Core.printl(Strings.typeneedaction);
            string result = Console.ReadLine();

            if (result != null)
            {
                if (result == "1)" || result == "1")
                {
                    Console.Clear();
                    RoomFive_5(Strings.nothingfind);
                }

                else if (result == "2)" || result == "2")
                {
                    Console.Clear();
                    RoomFive_5(Strings.nothingfind);
                }

                else if (result == "3)" || result == "3")
                {
                    if (saves.GetString("main", "3-4") == "1")
                    {
                        RoomFive_5(Strings.room5_notice34_finded);
                    }
                    saves.WriteString("main", "3-4", "1");
                    RoomFive_5(Strings.room5_notice34_find);
                }

                else if (result == "4)" || result == "4")
                {
                    Console.Clear();
                    RoomOne_1();
                }

                else if (result == "5)" || result == "5")
                {
                    RoomSix_6();
                }
                else RoomFive_5(Strings.choosecorrectvariant);
            }
            else RoomFive_5(Strings.choosecorrectvariant);
        }

        static void RoomSix_6(string msg = "") // Крыша
        {
            Console.Clear();
            if (msg != "") Core.printl(msg);
            for (int i = 0; i < 50; i++) if (i == 25) Core.print(Global.title); else Core.print(Global.textv); // Вывод самой верхней строки
            Core.printl("");
            Core.printl(Strings.room6_description);
            for (int i = 0; i < 50; i++) if (i == 25) Core.print(Strings.inventory); else Core.print(Global.textv); // Вывод отделения между описанием комнаты и инвентарём
            Core.printl("");

            if (saves.GetString("main", "1-4") == "1") Core.printl(Strings.notice14);
            if (saves.GetString("main", "2-4") == "1") Core.printl(Strings.notice24);
            if (saves.GetString("main", "4-1") == "1") Core.printl(Strings.notice41);
            if (saves.GetString("main", "3-4") == "1") Core.printl(Strings.notice34);
            if (saves.GetString("main", "5-4") == "1") Core.printl(Strings.notice54);
            if (saves.GetString("main", "1-4") == "1" || saves.GetString("main", "2-4") == "1" || saves.GetString("main", "4-1") == "1" || saves.GetString("main", "3-4") == "1" || saves.GetString("main", "5-4") == "1")
            {

            }
            else Core.printl(Strings.inventorynone);

            for (int i = 0; i < 50; i++) if (i == 25) Core.print(Strings.variants); else Core.print(Global.textv); // Вывод отделения между инвентарём и вариантами
            Core.printl("");
            Core.printl("1)" + Strings.room6_action1);
            Core.printl("2)" + Strings.room6_action2);
            Core.printl("3)" + Strings.room6_action3);
            Core.printl(Strings.typeneedaction);
            string result = Console.ReadLine();

            if (result != null)
            {
                if (result == "1)" || result == "1")
                {
                    Console.Clear();
                    RoomSix_6(Strings.nothingfind);
                }

                else if (result == "2)" || result == "2")
                {
                    Console.Clear();
                    RoomSix_6(Strings.nothingfind);
                }


                else if (result == "3)" || result == "3")
                {
                    RoomFive_5();
                }

                else RoomSix_6(Strings.choosecorrectvariant);
            }
            else RoomSix_6(Strings.choosecorrectvariant);
        }

        static void RoomSeven_7(string msg = "") // Подвал
        {
            Console.Clear();
            if (msg != "") Core.printl(msg);
            for (int i = 0; i < 50; i++) if (i == 25) Core.print(Global.title); else Core.print(Global.textv); // Вывод самой верхней строки
            Core.printl("");
            Core.printl(Strings.room7_description);
            for (int i = 0; i < 50; i++) if (i == 25) Core.print(Strings.inventory); else Core.print(Global.textv); // Вывод отделения между описанием комнаты и инвентарём
            Core.printl("");

            if (saves.GetString("main", "1-4") == "1") Core.printl(Strings.notice14);
            if (saves.GetString("main", "2-4") == "1") Core.printl(Strings.notice24);
            if (saves.GetString("main", "4-1") == "1") Core.printl(Strings.notice41);
            if (saves.GetString("main", "3-4") == "1") Core.printl(Strings.notice34);
            if (saves.GetString("main", "5-4") == "1") Core.printl(Strings.notice54);
            if (saves.GetString("main", "1-4") == "1" || saves.GetString("main", "2-4") == "1" || saves.GetString("main", "4-1") == "1" || saves.GetString("main", "3-4") == "1" || saves.GetString("main", "5-4") == "1")
            {

            }
            else Core.printl(Strings.inventorynone);

            for (int i = 0; i < 50; i++) if (i == 25) Core.print(Strings.variants); else Core.print(Global.textv); // Вывод отделения между инвентарём и вариантами
            Core.printl("");
            Core.printl("1)" + Strings.room7_action1);
            Core.printl("2)" + Strings.room7_action2);
            Core.printl("3)" + Strings.room7_action3);
            Core.printl("4)" + Strings.room7_action4);
            Core.printl(Strings.typeneedaction);
            string result = Console.ReadLine();

            if (result != null)
            {
                if (result == "1)" || result == "1")
                {
                    Console.Clear();
                    RoomSeven_7(Strings.nothingfind);
                }

                else if (result == "2)" || result == "2")
                {
                    Console.Clear();
                    RoomSeven_7(Strings.nothingfind);
                }

                else if (result == "3)" || result == "3")
                {
                    RoomTwo_2();
                }

                else if (result == "4)" || result == "4")
                {
                    RoomEight_8();
                }

                else RoomSeven_7(Strings.choosecorrectvariant);
            }
            else RoomSeven_7(Strings.choosecorrectvariant);
        }

        static void RoomEight_8(string msg = "") // Разлом
        {
            Console.Clear();
            if (msg != "") Core.printl(msg);
            for (int i = 0; i < 50; i++) if (i == 25) Core.print(Global.title); else Core.print(Global.textv); // Вывод самой верхней строки
            Core.printl("");
            Core.printl(Strings.room8_description);
            for (int i = 0; i < 50; i++) if (i == 25) Core.print(Strings.inventory); else Core.print(Global.textv); // Вывод отделения между описанием комнаты и инвентарём
            Core.printl("");

            if (saves.GetString("main", "1-4") == "1") Core.printl(Strings.notice14);
            if (saves.GetString("main", "2-4") == "1") Core.printl(Strings.notice24);
            if (saves.GetString("main", "4-1") == "1") Core.printl(Strings.notice41);
            if (saves.GetString("main", "3-4") == "1") Core.printl(Strings.notice34);
            if (saves.GetString("main", "5-4") == "1") Core.printl(Strings.notice54);
            if (saves.GetString("main", "1-4") == "1" || saves.GetString("main", "2-4") == "1" || saves.GetString("main", "4-1") == "1" || saves.GetString("main", "3-4") == "1" || saves.GetString("main", "5-4") == "1")
            {

            }
            else Core.printl(Strings.inventorynone);

            for (int i = 0; i < 50; i++) if (i == 25) Core.print(Strings.variants); else Core.print(Global.textv); // Вывод отделения между инвентарём и вариантами
            Core.printl("");
            Core.printl("1)" + Strings.room8_action1);
            Core.printl("2)" + Strings.room8_action2);
            Core.printl(Strings.typeneedaction);
            string result = Console.ReadLine();

            if (result != null)
            {
                if (result == "1)" || result == "1")
                {
                    RoomSeven_7();
                }

                else if (result == "2)" || result == "2")
                {
                    RoomNine_9();
                }

                else RoomEight_8(Strings.choosecorrectvariant);
            }
            else RoomEight_8(Strings.choosecorrectvariant);
        }

        static void RoomNine_9(string msg = "") // Развилка
        {
            Console.Clear();
            if (msg != "") Core.printl(msg);
            for (int i = 0; i < 50; i++) if (i == 25) Core.print(Global.title); else Core.print(Global.textv); // Вывод самой верхней строки
            Core.printl("");
            Core.printl(Strings.room9_description);
            for (int i = 0; i < 50; i++) if (i == 25) Core.print(Strings.inventory); else Core.print(Global.textv); // Вывод отделения между описанием комнаты и инвентарём
            Core.printl("");

            if (saves.GetString("main", "1-4") == "1") Core.printl(Strings.notice14);
            if (saves.GetString("main", "2-4") == "1") Core.printl(Strings.notice24);
            if (saves.GetString("main", "4-1") == "1") Core.printl(Strings.notice41);
            if (saves.GetString("main", "3-4") == "1") Core.printl(Strings.notice34);
            if (saves.GetString("main", "5-4") == "1") Core.printl(Strings.notice54);
            if (saves.GetString("main", "1-4") == "1" || saves.GetString("main", "2-4") == "1" || saves.GetString("main", "4-1") == "1" || saves.GetString("main", "3-4") == "1" || saves.GetString("main", "5-4") == "1")
            {

            }
            else Core.printl(Strings.inventorynone);

            for (int i = 0; i < 50; i++) if (i == 25) Core.print(Strings.variants); else Core.print(Global.textv); // Вывод отделения между инвентарём и вариантами
            Core.printl("");
            Core.printl("1)" + Strings.room9_action1);
            Core.printl("2)" + Strings.room9_action2);
            Core.printl("3)" + Strings.room9_action3);
            Core.printl(Strings.typeneedaction);
            string result = Console.ReadLine();

            if (result != null)
            {
                if (result == "1)" || result == "1")
                {
                    RoomEight_8();
                }

                else if (result == "2)" || result == "2")
                {
                    RoomTen_10();
                }

                else if (result == "3)" || result == "3")
                {
                    RoomTwelve_12();
                }

                else RoomNine_9(Strings.choosecorrectvariant);
            }
            else RoomNine_9(Strings.choosecorrectvariant);
        }

        static void RoomTen_10(string msg = "") // Спуск вниз
        {
            Console.Clear();
            if (msg != "") Core.printl(msg);
            for (int i = 0; i < 50; i++) if (i == 25) Core.print(Global.title); else Core.print(Global.textv); // Вывод самой верхней строки
            Core.printl("");
            Core.printl(Strings.room10_description);
            for (int i = 0; i < 50; i++) if (i == 25) Core.print(Strings.inventory); else Core.print(Global.textv); // Вывод отделения между описанием комнаты и инвентарём
            Core.printl("");

            if (saves.GetString("main", "1-4") == "1") Core.printl(Strings.notice14);
            if (saves.GetString("main", "2-4") == "1") Core.printl(Strings.notice24);
            if (saves.GetString("main", "4-1") == "1") Core.printl(Strings.notice41);
            if (saves.GetString("main", "3-4") == "1") Core.printl(Strings.notice34);
            if (saves.GetString("main", "5-4") == "1") Core.printl(Strings.notice54);
            if (saves.GetString("main", "1-4") == "1" || saves.GetString("main", "2-4") == "1" || saves.GetString("main", "4-1") == "1" || saves.GetString("main", "3-4") == "1" || saves.GetString("main", "5-4") == "1")
            {

            }
            else Core.printl(Strings.inventorynone);

            for (int i = 0; i < 50; i++) if (i == 25) Core.print(Strings.variants); else Core.print(Global.textv); // Вывод отделения между инвентарём и вариантами
            Core.printl("");
            Core.printl("1)" + Strings.room10_action1);
            Core.printl("2)" + Strings.room10_action2);
            Core.printl(Strings.typeneedaction);
            string result = Console.ReadLine();

            if (result != null)
            {
                if (result == "1)" || result == "1")
                {
                    RoomNine_9();
                }

                else if (result == "2)" || result == "2")
                {
                    RoomEleven_11();
                }

                else RoomTen_10(Strings.choosecorrectvariant);
            }
            else RoomTen_10(Strings.choosecorrectvariant);
        }

        static void RoomEleven_11(string msg = "") // Грот
        {
            Console.Clear();
            if (msg != "") Core.printl(msg);
            for (int i = 0; i < 50; i++) if (i == 25) Core.print(Global.title); else Core.print(Global.textv); // Вывод самой верхней строки
            Core.printl("");
            Core.printl(Strings.room11_description);
            for (int i = 0; i < 50; i++) if (i == 25) Core.print(Strings.inventory); else Core.print(Global.textv); // Вывод отделения между описанием комнаты и инвентарём
            Core.printl("");

            if (saves.GetString("main", "1-4") == "1") Core.printl(Strings.notice14);
            if (saves.GetString("main", "2-4") == "1") Core.printl(Strings.notice24);
            if (saves.GetString("main", "4-1") == "1") Core.printl(Strings.notice41);
            if (saves.GetString("main", "3-4") == "1") Core.printl(Strings.notice34);
            if (saves.GetString("main", "5-4") == "1") Core.printl(Strings.notice54);
            if (saves.GetString("main", "1-4") == "1" || saves.GetString("main", "2-4") == "1" || saves.GetString("main", "4-1") == "1" || saves.GetString("main", "3-4") == "1" || saves.GetString("main", "5-4") == "1")
            {

            }
            else Core.printl(Strings.inventorynone);

            for (int i = 0; i < 50; i++) if (i == 25) Core.print(Strings.variants); else Core.print(Global.textv); // Вывод отделения между инвентарём и вариантами
            Core.printl("");
            Core.printl("1)" + Strings.room11_action1);
            Core.printl("2)" + Strings.room11_action2);
            Core.printl("3)" + Strings.room11_action3);
            Core.printl(Strings.typeneedaction);
            string result = Console.ReadLine();

            if (result != null)
            {
                if (result == "1)" || result == "1")
                {
                    Console.Clear();
                    RoomEleven_11(Strings.nothingfind);
                }

                else if (result == "2)" || result == "2")
                {
                    Console.Clear();
                    RoomEleven_11(Strings.nothingfind);
                }

                else if (result == "3)" || result == "3")
                {
                    RoomTen_10();
                }

                else RoomEleven_11(Strings.choosecorrectvariant);
            }
            else RoomEleven_11(Strings.choosecorrectvariant);
        }

        static void RoomTwelve_12(string msg = "") // Пещера
        {
            Console.Clear();
            if (msg != "") Core.printl(msg);
            for (int i = 0; i < 50; i++) if (i == 25) Core.print(Global.title); else Core.print(Global.textv); // Вывод самой верхней строки
            Core.printl("");
            Core.printl(Strings.room12_description);
            for (int i = 0; i < 50; i++) if (i == 25) Core.print(Strings.inventory); else Core.print(Global.textv); // Вывод отделения между описанием комнаты и инвентарём
            Core.printl("");

            if (saves.GetString("main", "1-4") == "1") Core.printl(Strings.notice14);
            if (saves.GetString("main", "2-4") == "1") Core.printl(Strings.notice24);
            if (saves.GetString("main", "4-1") == "1") Core.printl(Strings.notice41);
            if (saves.GetString("main", "3-4") == "1") Core.printl(Strings.notice34);
            if (saves.GetString("main", "5-4") == "1") Core.printl(Strings.notice54);
            if (saves.GetString("main", "1-4") == "1" || saves.GetString("main", "2-4") == "1" || saves.GetString("main", "4-1") == "1" || saves.GetString("main", "3-4") == "1" || saves.GetString("main", "5-4") == "1")
            {

            }
            else Core.printl(Strings.inventorynone);

            for (int i = 0; i < 50; i++) if (i == 25) Core.print(Strings.variants); else Core.print(Global.textv); // Вывод отделения между инвентарём и вариантами
            Core.printl("");
            Core.printl("1)" + Strings.room12_action1);
            Core.printl("2)" + Strings.room12_action2);
            Core.printl("3)" + Strings.room12_action3);
            Core.printl(Strings.typeneedaction);
            string result = Console.ReadLine();

            if (result != null)
            {
                if (result == "1)" || result == "1")
                {
                    if (saves.GetString("main", "5-4") == "1")
                    {
                        RoomTwelve_12(Strings.room12_notice54_finded);
                    }
                    saves.WriteString("main", "5-4", "1");
                    RoomTwelve_12(Strings.room12_notice54_find);
                }

                else if (result == "2)" || result == "2")
                {
                    Console.Clear();
                    RoomTwelve_12(Strings.nothingfind);
                }

                else if (result == "3)" || result == "3")
                {
                    RoomNine_9();
                }

                else RoomTwelve_12(Strings.choosecorrectvariant);
            }
            else RoomTwelve_12(Strings.choosecorrectvariant);
        }
    }
}
