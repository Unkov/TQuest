using System;
using INIManager;
using System.Threading;
using TQuestLib;
using System.Runtime.InteropServices;
using System.Net;

namespace TQuest
{
    internal class Program
    {
        static bool exitSystem = false;
        
        #region Trap application termination
        [DllImport("Kernel32")]
        private static extern bool SetConsoleCtrlHandler(EventHandler handler, bool add);

        private delegate bool EventHandler(CtrlType sig);
        static EventHandler _handler;

        enum CtrlType
        {
            CTRL_C_EVENT = 0,
            CTRL_BREAK_EVENT = 1,
            CTRL_CLOSE_EVENT = 2,
            CTRL_LOGOFF_EVENT = 5,
            CTRL_SHUTDOWN_EVENT = 6
        }
        public static HubManager saves = new HubManager(Global.spath);

        private static bool Handler(CtrlType sig)
        {
            saves.WriteString("main", "1-4", "0");
            saves.WriteString("main", "2-4", "0");
            saves.WriteString("main", "4-1", "0");
            saves.WriteString("main", "3-4", "0");
            saves.WriteString("main", "5-4", "0");

            //allow main to run off
            exitSystem = true;

            //shutdown right away so there are no lingering threads
            Environment.Exit(-1);

            return true;
        }
        #endregion

        static void Main(string[] args)
        {
            _handler += new EventHandler(Handler);
            SetConsoleCtrlHandler(_handler, true);
            Program p = new Program();
            p.Start();

            while (!exitSystem)
            {
                Thread.Sleep(500);
            }
        }

        public void Start()
        {
            var random = new Random();
            Global.numberKey = random.Next(0, 6);
            bool connection = Internet.CheckConnection();
            if (connection)
            {
                WebClient web = new WebClient();
                string version = saves.GetString("program", "version");
                string sVersion = web.DownloadString("https://api.unkov.su/TQuest/lversion.php");
                if (version != sVersion)
                    Global.oldVersion = true;
            }
            // Титры в начале
            Console.Title = "TQuest";
            Core.printl(Localization.RU.welcomemessage, ConsoleColor.Red);
            Thread.Sleep(5000);
            Console.Clear();

            Core.intro();
            Core.MissionImpossible();
            Console.Clear();
            if (Global.oldVersion)
                RoomOne_1(Localization.RU.oldVersionMessage);
            RoomOne_1();
        }

        static void RoomOne_1(string msg = "") // Коридор
        {
            if (msg != "") Core.printl(msg);
            for (int i = 0; i < 50; i++) if (i == 25) Core.print(Global.title); else Core.print(Global.textv); // Вывод самой верхней строки
            Core.printl("");
            Core.printl(Localization.RU.room1_description);
            for (int i = 0; i < 50; i++) if (i == 25) Core.print(Localization.RU.inventory); else Core.print(Global.textv); // Вывод отделения между описанием комнаты и инвентарём
            Core.printl("");

            if (saves.GetString("main", "1-4") == "1") Core.printl(Localization.RU.notice14);
            if (saves.GetString("main", "2-4") == "1") Core.printl(Localization.RU.notice24);
            if (saves.GetString("main", "4-1") == "1") Core.printl(Localization.RU.notice41);
            if (saves.GetString("main", "3-4") == "1") Core.printl(Localization.RU.notice34);
            if (saves.GetString("main", "5-4") == "1") Core.printl(Localization.RU.notice54);
            if (saves.GetString("main", "1-4") == "1" || saves.GetString("main", "2-4") == "1" || saves.GetString("main", "4-1") == "1" || saves.GetString("main", "3-4") == "1" || saves.GetString("main", "5-4") == "1")
            {

            }
            else Core.printl(Localization.RU.inventorynone);

            for (int i = 0; i < 50; i++) if (i == 25) Core.print(Localization.RU.variants); else Core.print(Global.textv); // Вывод отделения между инвентарём и вариантами
            Core.printl("");
            Core.printl("1)" + Localization.RU.room1_action1);
            Core.printl("2)" + Localization.RU.room1_action2);
            Core.printl("3)" + Localization.RU.room1_action3);
            Core.printl("4)" + Localization.RU.room1_action4);
            Core.printl("5)" + Localization.RU.room1_action5);
            Core.printl("6)" + Localization.RU.room1_action6);
            Core.printl("7)" + Localization.RU.room1_action7);
            Core.printl(Localization.RU.typeneedaction);
            string result = Console.ReadLine();
            if (result != null)
            {
                if (result == "1)" || result == "1")
                {
                    Console.Clear();
                    RoomOne_1(Localization.RU.nothingfind);
                }

                else if (result == "2)" || result == "2")
                {
                    if (saves.GetString("main", "1-4") == "1")
                    {
                        Console.Clear();
                        RoomOne_1(Localization.RU.room1_notice14_finded);
                    }
                    Console.Clear();
                    saves.WriteString("main", "1-4", "1");
                    RoomOne_1(Localization.RU.room1_notice14_find);
                }

                else if (result == "3)" || result == "3")
                {
                    if (saves.GetString("main", "key") == "1")
                    {
                        Console.Clear();
                        for (int i = 0; i < 50; i++) if (i == 25) Core.print(Localization.RU.gameover); else Core.print(Global.textv);
                        Core.printl(Localization.RU.gameovertext);
                        Console.ReadKey();
                    }
                    else Console.Clear(); RoomOne_1(Localization.RU.needkey);
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

                else RoomOne_1(Localization.RU.choosecorrectvariant);
            }
            else RoomOne_1(Localization.RU.choosecorrectvariant);
        }

        static void RoomTwo_2(string msg = "") // Кухня
        {
            Console.Clear();
            if (msg != "") Core.printl(msg);
            for (int i = 0; i < 50; i++) if (i == 25) Core.print(Global.title); else Core.print(Global.textv); // Вывод самой верхней строки
            Core.printl("");
            Core.printl(Localization.RU.room2_description);
            for (int i = 0; i < 50; i++) if (i == 25) Core.print(Localization.RU.inventory); else Core.print(Global.textv); // Вывод отделения между описанием комнаты и инвентарём
            Core.printl("");

            if (saves.GetString("main", "1-4") == "1") Core.printl(Localization.RU.notice14);
            if (saves.GetString("main", "2-4") == "1") Core.printl(Localization.RU.notice24);
            if (saves.GetString("main", "4-1") == "1") Core.printl(Localization.RU.notice41);
            if (saves.GetString("main", "3-4") == "1") Core.printl(Localization.RU.notice34);
            if (saves.GetString("main", "5-4") == "1") Core.printl(Localization.RU.notice54);
            if (saves.GetString("main", "1-4") == "1" || saves.GetString("main", "2-4") == "1" || saves.GetString("main", "4-1") == "1" || saves.GetString("main", "3-4") == "1" || saves.GetString("main", "5-4") == "1")
            {

            }
            else Core.printl(Localization.RU.inventorynone);

            for (int i = 0; i < 50; i++) if (i == 25) Core.print(Localization.RU.variants); else Core.print(Global.textv); // Вывод отделения между инвентарём и вариантами
            Core.printl("");
            Core.printl("1)" + Localization.RU.room2_action1);
            Core.printl("2)" + Localization.RU.room2_action2);
            Core.printl("3)" + Localization.RU.room2_action3);
            Core.printl("4)" + Localization.RU.room2_action4);
            Core.printl("5)" + Localization.RU.room2_action5);
            Core.printl(Localization.RU.typeneedaction);
            string result = Console.ReadLine();
            if (result != null)
            {
                if (result == "1" || result == "1)")
                {
                    Console.Clear();
                    RoomTwo_2(Localization.RU.nothingfind);
                }

                else if (result == "2" || result == "2)")
                {
                    if (saves.GetString("main", "2-4") == "1")
                    {
                        Console.Clear();
                        RoomTwo_2(Localization.RU.room2_notice24_finded);
                    }
                    Console.Clear();
                    saves.WriteString("main", "2-4", "1");
                    RoomTwo_2(Localization.RU.room2_notice24_find);
                }

                else if (result == "3" || result == "3)")
                {
                    Console.Clear();
                    RoomTwo_2(Localization.RU.nothingfind);
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

                else RoomTwo_2(Localization.RU.choosecorrectvariant);
            }
            else RoomTwo_2(Localization.RU.choosecorrectvariant);
        }

        static void RoomThree_3(string msg = "") // Кабинет
        {
            Console.Clear();
            if (msg != "") Core.printl(msg);
            for (int i = 0; i < 50; i++) if (i == 25) Core.print(Global.title); else Core.print(Global.textv); // Вывод самой верхней строки
            Core.printl("");
            Core.printl(Localization.RU.room3_description);
            for (int i = 0; i < 50; i++) if (i == 25) Core.print(Localization.RU.inventory); else Core.print(Global.textv); // Вывод отделения между описанием комнаты и инвентарём
            Core.printl("");

            if (saves.GetString("main", "1-4") == "1") Core.printl(Localization.RU.notice14);
            if (saves.GetString("main", "2-4") == "1") Core.printl(Localization.RU.notice24);
            if (saves.GetString("main", "4-1") == "1") Core.printl(Localization.RU.notice41);
            if (saves.GetString("main", "3-4") == "1") Core.printl(Localization.RU.notice34);
            if (saves.GetString("main", "5-4") == "1") Core.printl(Localization.RU.notice54);
            if (saves.GetString("main", "1-4") == "1" || saves.GetString("main", "2-4") == "1" || saves.GetString("main", "4-1") == "1" || saves.GetString("main", "3-4") == "1" || saves.GetString("main", "5-4") == "1")
            {

            }
            else Core.printl(Localization.RU.inventorynone);

            for (int i = 0; i < 50; i++) if (i == 25) Core.print(Localization.RU.variants); else Core.print(Global.textv); // Вывод отделения между инвентарём и вариантами
            Core.printl("");
            Core.printl("1)" + Localization.RU.room3_action1);
            Core.printl("2)" + Localization.RU.room3_action2);
            Core.printl("3)" + Localization.RU.room3_action3);
            Core.printl("4)" + Localization.RU.room3_action4);
            Core.printl(Localization.RU.typeneedaction);
            Core.printl(Global.numberKey.ToString());
            string result = Console.ReadLine();

            if (result != null)
            {
                if (result == "1)" || result == "1")
                {
                    if (saves.GetString("main", "4-1") == "1")
                    {
                        RoomThree_3(Localization.RU.room3_notice41_finded);
                    }
                    saves.WriteString("main", "4-1", "1");
                    RoomThree_3(Localization.RU.room3_notice41_find);
                }

                else if (result == "2)" || result == "2")
                {
                    RoomThree_3(Localization.RU.nothingfind);
                }

                else if (result == "3)" || result == "3")
                {
                    if (saves.GetString("main", "key") == "1") RoomThree_3(Localization.RU.searchthis);
                    string[] passwords = { "e72c72c6748ea2bb59ebf9d79231abe18e7bd608cf524d0f9e438e542d39e38a", "6e5d967631bbc0c7d36eefbe398a75323cdd18b2a6e180b95b42add033524cac", "7400bc38db9838278e42498a258f124a1aa2ddaa844bf0a4ba478a709f98c0d1", "61304c99d645456f73c0126dfc69c6de249d52b816078bf2ec66ea0a1322e1aa", "eab5cab790f3bfbb6a5169c6f776aa7ae10637fbd2b61fcc1cc58a501c018728" };
                    Console.Clear();
                    Core.printl(Localization.RU.room3_password);
                    string password = Console.ReadLine();
                    if (string.IsNullOrEmpty(password))
                    {
                        string hashedpassword = Core.sha256hash(password);
                        string unhashedpassword = passwords[Global.numberKey];
                        if (hashedpassword == unhashedpassword)
                        {
                            Core.printl(Localization.RU.room3_acceptpassword);
                            saves.WriteString("main", "key", "1");
                            Thread.Sleep(15000);
                            RoomThree_3();
                        }
                        else RoomThree_3(Localization.RU.room3_wrongpassword);
                    }
                    else RoomThree_3(Localization.RU.room3_wrongpassword);
                }

                else if (result == "4)" || result == "4")
                {
                    Console.Clear();
                    RoomOne_1();
                }

                else RoomThree_3(Localization.RU.choosecorrectvariant);
            }
            else RoomThree_3(Localization.RU.choosecorrectvariant);
        }

        static void RoomFour_4(string msg = "") // Спальня
        {
            Console.Clear();
            if (msg != "") Core.printl(msg);
            for (int i = 0; i < 50; i++) if (i == 25) Core.print(Global.title); else Core.print(Global.textv); // Вывод самой верхней строки
            Core.printl("");
            Core.printl(Localization.RU.room4_description);
            for (int i = 0; i < 50; i++) if (i == 25) Core.print(Localization.RU.inventory); else Core.print(Global.textv); // Вывод отделения между описанием комнаты и инвентарём
            Core.printl("");

            if (saves.GetString("main", "1-4") == "1") Core.printl(Localization.RU.notice14);
            if (saves.GetString("main", "2-4") == "1") Core.printl(Localization.RU.notice24);
            if (saves.GetString("main", "4-1") == "1") Core.printl(Localization.RU.notice41);
            if (saves.GetString("main", "3-4") == "1") Core.printl(Localization.RU.notice34);
            if (saves.GetString("main", "5-4") == "1") Core.printl(Localization.RU.notice54);
            if (saves.GetString("main", "1-4") == "1" || saves.GetString("main", "2-4") == "1" || saves.GetString("main", "4-1") == "1" || saves.GetString("main", "3-4") == "1" || saves.GetString("main", "5-4") == "1")
            {

            }
            else Core.printl(Localization.RU.inventorynone);

            for (int i = 0; i < 50; i++) if (i == 25) Core.print(Localization.RU.variants); else Core.print(Global.textv); // Вывод отделения между инвентарём и вариантами
            Core.printl("");
            Core.printl("1)" + Localization.RU.room4_action1);
            Core.printl("2)" + Localization.RU.room4_action2);
            Core.printl("3)" + Localization.RU.room4_action3);
            Core.printl("4)" + Localization.RU.room4_action4);
            Core.printl(Localization.RU.typeneedaction);
            string result = Console.ReadLine();

            if (result != null)
            {
                if (result == "1)" || result == "1")
                {
                    RoomFour_4(Localization.RU.nothingfind);
                }

                else if (result == "2)" || result == "2")
                {
                    RoomFour_4(Localization.RU.nothingfind);
                }

                else if (result == "3)" || result == "3")
                {
                    Console.Clear();
                    RoomFour_4(Localization.RU.nothingfind);
                }

                else if (result == "4)" || result == "4")
                {
                    Console.Clear();
                    RoomOne_1();
                }

                else RoomFour_4(Localization.RU.choosecorrectvariant);
            }
            else RoomFour_4(Localization.RU.choosecorrectvariant);
        }

        static void RoomFive_5(string msg = "") // Чердак
        {
            Console.Clear();
            if (msg != "") Core.printl(msg);
            for (int i = 0; i < 50; i++) if (i == 25) Core.print(Global.title); else Core.print(Global.textv); // Вывод самой верхней строки
            Core.printl("");
            Core.printl(Localization.RU.room5_description);
            for (int i = 0; i < 50; i++) if (i == 25) Core.print(Localization.RU.inventory); else Core.print(Global.textv); // Вывод отделения между описанием комнаты и инвентарём
            Core.printl("");

            if (saves.GetString("main", "1-4") == "1") Core.printl(Localization.RU.notice14);
            if (saves.GetString("main", "2-4") == "1") Core.printl(Localization.RU.notice24);
            if (saves.GetString("main", "4-1") == "1") Core.printl(Localization.RU.notice41);
            if (saves.GetString("main", "3-4") == "1") Core.printl(Localization.RU.notice34);
            if (saves.GetString("main", "5-4") == "1") Core.printl(Localization.RU.notice54);
            if (saves.GetString("main", "1-4") == "1" || saves.GetString("main", "2-4") == "1" || saves.GetString("main", "4-1") == "1" || saves.GetString("main", "3-4") == "1" || saves.GetString("main", "5-4") == "1")
            {

            }
            else Core.printl(Localization.RU.inventorynone);

            for (int i = 0; i < 50; i++) if (i == 25) Core.print(Localization.RU.variants); else Core.print(Global.textv); // Вывод отделения между инвентарём и вариантами
            Core.printl("");
            Core.printl("1)" + Localization.RU.room5_action1);
            Core.printl("2)" + Localization.RU.room5_action2);
            Core.printl("3)" + Localization.RU.room5_action3);
            Core.printl("4)" + Localization.RU.room5_action4);
            Core.printl("5)" + Localization.RU.room5_action5);
            Core.printl(Localization.RU.typeneedaction);
            string result = Console.ReadLine();

            if (result != null)
            {
                if (result == "1)" || result == "1")
                {
                    Console.Clear();
                    RoomFive_5(Localization.RU.nothingfind);
                }

                else if (result == "2)" || result == "2")
                {
                    Console.Clear();
                    RoomFive_5(Localization.RU.nothingfind);
                }

                else if (result == "3)" || result == "3")
                {
                    if (saves.GetString("main", "3-4") == "1")
                    {
                        RoomFive_5(Localization.RU.room5_notice34_finded);
                    }
                    saves.WriteString("main", "3-4", "1");
                    RoomFive_5(Localization.RU.room5_notice34_find);
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
                else RoomFive_5(Localization.RU.choosecorrectvariant);
            }
            else RoomFive_5(Localization.RU.choosecorrectvariant);
        }

        static void RoomSix_6(string msg = "") // Крыша
        {
            Console.Clear();
            if (msg != "") Core.printl(msg);
            for (int i = 0; i < 50; i++) if (i == 25) Core.print(Global.title); else Core.print(Global.textv); // Вывод самой верхней строки
            Core.printl("");
            Core.printl(Localization.RU.room6_description);
            for (int i = 0; i < 50; i++) if (i == 25) Core.print(Localization.RU.inventory); else Core.print(Global.textv); // Вывод отделения между описанием комнаты и инвентарём
            Core.printl("");

            if (saves.GetString("main", "1-4") == "1") Core.printl(Localization.RU.notice14);
            if (saves.GetString("main", "2-4") == "1") Core.printl(Localization.RU.notice24);
            if (saves.GetString("main", "4-1") == "1") Core.printl(Localization.RU.notice41);
            if (saves.GetString("main", "3-4") == "1") Core.printl(Localization.RU.notice34);
            if (saves.GetString("main", "5-4") == "1") Core.printl(Localization.RU.notice54);
            if (saves.GetString("main", "1-4") == "1" || saves.GetString("main", "2-4") == "1" || saves.GetString("main", "4-1") == "1" || saves.GetString("main", "3-4") == "1" || saves.GetString("main", "5-4") == "1")
            {

            }
            else Core.printl(Localization.RU.inventorynone);

            for (int i = 0; i < 50; i++) if (i == 25) Core.print(Localization.RU.variants); else Core.print(Global.textv); // Вывод отделения между инвентарём и вариантами
            Core.printl("");
            Core.printl("1)" + Localization.RU.room6_action1);
            Core.printl("2)" + Localization.RU.room6_action2);
            Core.printl("3)" + Localization.RU.room6_action3);
            Core.printl(Localization.RU.typeneedaction);
            string result = Console.ReadLine();

            if (result != null)
            {
                if (result == "1)" || result == "1")
                {
                    Console.Clear();
                    RoomSix_6(Localization.RU.nothingfind);
                }

                else if (result == "2)" || result == "2")
                {
                    Console.Clear();
                    RoomSix_6(Localization.RU.nothingfind);
                }


                else if (result == "3)" || result == "3")
                {
                    RoomFive_5();
                }

                else RoomSix_6(Localization.RU.choosecorrectvariant);
            }
            else RoomSix_6(Localization.RU.choosecorrectvariant);
        }

        static void RoomSeven_7(string msg = "") // Подвал
        {
            Console.Clear();
            if (msg != "") Core.printl(msg);
            for (int i = 0; i < 50; i++) if (i == 25) Core.print(Global.title); else Core.print(Global.textv); // Вывод самой верхней строки
            Core.printl("");
            Core.printl(Localization.RU.room7_description);
            for (int i = 0; i < 50; i++) if (i == 25) Core.print(Localization.RU.inventory); else Core.print(Global.textv); // Вывод отделения между описанием комнаты и инвентарём
            Core.printl("");

            if (saves.GetString("main", "1-4") == "1") Core.printl(Localization.RU.notice14);
            if (saves.GetString("main", "2-4") == "1") Core.printl(Localization.RU.notice24);
            if (saves.GetString("main", "4-1") == "1") Core.printl(Localization.RU.notice41);
            if (saves.GetString("main", "3-4") == "1") Core.printl(Localization.RU.notice34);
            if (saves.GetString("main", "5-4") == "1") Core.printl(Localization.RU.notice54);
            if (saves.GetString("main", "1-4") == "1" || saves.GetString("main", "2-4") == "1" || saves.GetString("main", "4-1") == "1" || saves.GetString("main", "3-4") == "1" || saves.GetString("main", "5-4") == "1")
            {

            }
            else Core.printl(Localization.RU.inventorynone);

            for (int i = 0; i < 50; i++) if (i == 25) Core.print(Localization.RU.variants); else Core.print(Global.textv); // Вывод отделения между инвентарём и вариантами
            Core.printl("");
            Core.printl("1)" + Localization.RU.room7_action1);
            Core.printl("2)" + Localization.RU.room7_action2);
            Core.printl("3)" + Localization.RU.room7_action3);
            Core.printl("4)" + Localization.RU.room7_action4);
            Core.printl(Localization.RU.typeneedaction);
            string result = Console.ReadLine();

            if (result != null)
            {
                if (result == "1)" || result == "1")
                {
                    Console.Clear();
                    RoomSeven_7(Localization.RU.nothingfind);
                }

                else if (result == "2)" || result == "2")
                {
                    Console.Clear();
                    RoomSeven_7(Localization.RU.nothingfind);
                }

                else if (result == "3)" || result == "3")
                {
                    RoomTwo_2();
                }

                else if (result == "4)" || result == "4")
                {
                    RoomEight_8();
                }

                else RoomSeven_7(Localization.RU.choosecorrectvariant);
            }
            else RoomSeven_7(Localization.RU.choosecorrectvariant);
        }

        static void RoomEight_8(string msg = "") // Разлом
        {
            Console.Clear();
            if (msg != "") Core.printl(msg);
            for (int i = 0; i < 50; i++) if (i == 25) Core.print(Global.title); else Core.print(Global.textv); // Вывод самой верхней строки
            Core.printl("");
            Core.printl(Localization.RU.room8_description);
            for (int i = 0; i < 50; i++) if (i == 25) Core.print(Localization.RU.inventory); else Core.print(Global.textv); // Вывод отделения между описанием комнаты и инвентарём
            Core.printl("");

            if (saves.GetString("main", "1-4") == "1") Core.printl(Localization.RU.notice14);
            if (saves.GetString("main", "2-4") == "1") Core.printl(Localization.RU.notice24);
            if (saves.GetString("main", "4-1") == "1") Core.printl(Localization.RU.notice41);
            if (saves.GetString("main", "3-4") == "1") Core.printl(Localization.RU.notice34);
            if (saves.GetString("main", "5-4") == "1") Core.printl(Localization.RU.notice54);
            if (saves.GetString("main", "1-4") == "1" || saves.GetString("main", "2-4") == "1" || saves.GetString("main", "4-1") == "1" || saves.GetString("main", "3-4") == "1" || saves.GetString("main", "5-4") == "1")
            {

            }
            else Core.printl(Localization.RU.inventorynone);

            for (int i = 0; i < 50; i++) if (i == 25) Core.print(Localization.RU.variants); else Core.print(Global.textv); // Вывод отделения между инвентарём и вариантами
            Core.printl("");
            Core.printl("1)" + Localization.RU.room8_action1);
            Core.printl("2)" + Localization.RU.room8_action2);
            Core.printl(Localization.RU.typeneedaction);
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

                else RoomEight_8(Localization.RU.choosecorrectvariant);
            }
            else RoomEight_8(Localization.RU.choosecorrectvariant);
        }

        static void RoomNine_9(string msg = "") // Развилка
        {
            Console.Clear();
            if (msg != "") Core.printl(msg);
            for (int i = 0; i < 50; i++) if (i == 25) Core.print(Global.title); else Core.print(Global.textv); // Вывод самой верхней строки
            Core.printl("");
            Core.printl(Localization.RU.room9_description);
            for (int i = 0; i < 50; i++) if (i == 25) Core.print(Localization.RU.inventory); else Core.print(Global.textv); // Вывод отделения между описанием комнаты и инвентарём
            Core.printl("");

            if (saves.GetString("main", "1-4") == "1") Core.printl(Localization.RU.notice14);
            if (saves.GetString("main", "2-4") == "1") Core.printl(Localization.RU.notice24);
            if (saves.GetString("main", "4-1") == "1") Core.printl(Localization.RU.notice41);
            if (saves.GetString("main", "3-4") == "1") Core.printl(Localization.RU.notice34);
            if (saves.GetString("main", "5-4") == "1") Core.printl(Localization.RU.notice54);
            if (saves.GetString("main", "1-4") == "1" || saves.GetString("main", "2-4") == "1" || saves.GetString("main", "4-1") == "1" || saves.GetString("main", "3-4") == "1" || saves.GetString("main", "5-4") == "1")
            {

            }
            else Core.printl(Localization.RU.inventorynone);

            for (int i = 0; i < 50; i++) if (i == 25) Core.print(Localization.RU.variants); else Core.print(Global.textv); // Вывод отделения между инвентарём и вариантами
            Core.printl("");
            Core.printl("1)" + Localization.RU.room9_action1);
            Core.printl("2)" + Localization.RU.room9_action2);
            Core.printl("3)" + Localization.RU.room9_action3);
            Core.printl(Localization.RU.typeneedaction);
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
                    RoomEleven_11();
                }

                else RoomNine_9(Localization.RU.choosecorrectvariant);
            }
            else RoomNine_9(Localization.RU.choosecorrectvariant);
        }

        static void RoomTen_10(string msg = "") // Спуск вниз
        {
            Console.Clear();
            if (msg != "") Core.printl(msg);
            for (int i = 0; i < 50; i++) if (i == 25) Core.print(Global.title); else Core.print(Global.textv); // Вывод самой верхней строки
            Core.printl("");
            Core.printl(Localization.RU.room10_description);
            for (int i = 0; i < 50; i++) if (i == 25) Core.print(Localization.RU.inventory); else Core.print(Global.textv); // Вывод отделения между описанием комнаты и инвентарём
            Core.printl("");

            if (saves.GetString("main", "1-4") == "1") Core.printl(Localization.RU.notice14);
            if (saves.GetString("main", "2-4") == "1") Core.printl(Localization.RU.notice24);
            if (saves.GetString("main", "4-1") == "1") Core.printl(Localization.RU.notice41);
            if (saves.GetString("main", "3-4") == "1") Core.printl(Localization.RU.notice34);
            if (saves.GetString("main", "5-4") == "1") Core.printl(Localization.RU.notice54);
            if (saves.GetString("main", "1-4") == "1" || saves.GetString("main", "2-4") == "1" || saves.GetString("main", "4-1") == "1" || saves.GetString("main", "3-4") == "1" || saves.GetString("main", "5-4") == "1")
            {

            }
            else Core.printl(Localization.RU.inventorynone);

            for (int i = 0; i < 50; i++) if (i == 25) Core.print(Localization.RU.variants); else Core.print(Global.textv); // Вывод отделения между инвентарём и вариантами
            Core.printl("");
            Core.printl("1)" + Localization.RU.room10_action1);
            Core.printl("2)" + Localization.RU.room10_action2);
            Core.printl(Localization.RU.typeneedaction);
            string result = Console.ReadLine();

            if (result != null)
            {
                if (result == "1)" || result == "1")
                {
                    RoomNine_9();
                }

                else if (result == "2)" || result == "2")
                {
                    RoomTwelve_12();
                }

                else RoomTen_10(Localization.RU.choosecorrectvariant);
            }
            else RoomTen_10(Localization.RU.choosecorrectvariant);
        }

        static void RoomEleven_11(string msg = "") // Пещера
        {
            Console.Clear();
            if (msg != "") Core.printl(msg);
            for (int i = 0; i < 50; i++) if (i == 25) Core.print(Global.title); else Core.print(Global.textv); // Вывод самой верхней строки
            Core.printl("");
            Core.printl(Localization.RU.room11_description);
            for (int i = 0; i < 50; i++) if (i == 25) Core.print(Localization.RU.inventory); else Core.print(Global.textv); // Вывод отделения между описанием комнаты и инвентарём
            Core.printl("");

            if (saves.GetString("main", "1-4") == "1") Core.printl(Localization.RU.notice14);
            if (saves.GetString("main", "2-4") == "1") Core.printl(Localization.RU.notice24);
            if (saves.GetString("main", "4-1") == "1") Core.printl(Localization.RU.notice41);
            if (saves.GetString("main", "3-4") == "1") Core.printl(Localization.RU.notice34);
            if (saves.GetString("main", "5-4") == "1") Core.printl(Localization.RU.notice54);
            if (saves.GetString("main", "1-4") == "1" || saves.GetString("main", "2-4") == "1" || saves.GetString("main", "4-1") == "1" || saves.GetString("main", "3-4") == "1" || saves.GetString("main", "5-4") == "1")
            {

            }
            else Core.printl(Localization.RU.inventorynone);

            for (int i = 0; i < 50; i++) if (i == 25) Core.print(Localization.RU.variants); else Core.print(Global.textv); // Вывод отделения между инвентарём и вариантами
            Core.printl("");
            Core.printl("1)" + Localization.RU.room11_action1);
            Core.printl("2)" + Localization.RU.room11_action2);
            Core.printl("3)" + Localization.RU.room11_action3);
            Core.printl(Localization.RU.typeneedaction);
            string result = Console.ReadLine();

            if (result != null)
            {
                if (result == "1)" || result == "1")
                {
                    if (saves.GetString("main", "5-4") == "1")
                    {
                        RoomEleven_11(Localization.RU.room5_notice34_finded);
                    }
                    saves.WriteString("main", "5-4", "1");
                    RoomEleven_11(Localization.RU.room5_notice34_find);
                }

                else if (result == "2)" || result == "2")
                {
                    Console.Clear();
                    RoomEleven_11(Localization.RU.nothingfind);
                }

                else if (result == "3)" || result == "3")
                {
                    RoomNine_9();
                }

                else RoomEleven_11(Localization.RU.choosecorrectvariant);
            }
            else RoomEleven_11(Localization.RU.choosecorrectvariant);
        }

        static void RoomTwelve_12(string msg = "") // Грот
        {
            Console.Clear();
            if (msg != "") Core.printl(msg);
            for (int i = 0; i < 50; i++) if (i == 25) Core.print(Global.title); else Core.print(Global.textv); // Вывод самой верхней строки
            Core.printl("");
            Core.printl(Localization.RU.room12_description);
            for (int i = 0; i < 50; i++) if (i == 25) Core.print(Localization.RU.inventory); else Core.print(Global.textv); // Вывод отделения между описанием комнаты и инвентарём
            Core.printl("");

            if (saves.GetString("main", "1-4") == "1") Core.printl(Localization.RU.notice14);
            if (saves.GetString("main", "2-4") == "1") Core.printl(Localization.RU.notice24);
            if (saves.GetString("main", "4-1") == "1") Core.printl(Localization.RU.notice41);
            if (saves.GetString("main", "3-4") == "1") Core.printl(Localization.RU.notice34);
            if (saves.GetString("main", "5-4") == "1") Core.printl(Localization.RU.notice54);
            if (saves.GetString("main", "1-4") == "1" || saves.GetString("main", "2-4") == "1" || saves.GetString("main", "4-1") == "1" || saves.GetString("main", "3-4") == "1" || saves.GetString("main", "5-4") == "1")
            {

            }
            else Core.printl(Localization.RU.inventorynone);

            for (int i = 0; i < 50; i++) if (i == 25) Core.print(Localization.RU.variants); else Core.print(Global.textv); // Вывод отделения между инвентарём и вариантами
            Core.printl("");
            Core.printl("1)" + Localization.RU.room12_action1);
            Core.printl("2)" + Localization.RU.room12_action2);
            Core.printl("3)" + Localization.RU.room12_action3);
            Core.printl(Localization.RU.typeneedaction);
            string result = Console.ReadLine();

            if (result != null)
            {
                if (result == "1)" || result == "1")
                {
                    Console.Clear();
                    RoomTwelve_12(Localization.RU.nothingfind);
                }

                else if (result == "2)" || result == "2")
                {
                    Console.Clear();
                    RoomTwelve_12(Localization.RU.nothingfind);
                }

                else if (result == "3)" || result == "3")
                {
                    RoomTen_10();
                }

                else RoomTwelve_12(Localization.RU.choosecorrectvariant);
            }
            else RoomTwelve_12(Localization.RU.choosecorrectvariant);
        }
    }
}
