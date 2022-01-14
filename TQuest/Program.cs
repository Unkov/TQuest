using System;
using INIManager;
using System.Threading;
using TQuestLib;
using System.Runtime.InteropServices;

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
            // Титры в начале
            Console.Title = "TQuest";
            Console.ForegroundColor = ConsoleColor.Red;
            Core.printl(Localization.RU.welcomemessage);
            Thread.Sleep(5000);
            Console.Clear();

            Console.ForegroundColor = ConsoleColor.White;
            Core.intro();
            MissionImpossible();
            Console.Clear();
            RoomOne_1();
        }

        static void RoomOne_1() // Коридор
        {
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
                    Core.printl(Localization.RU.nothingfind);
                    RoomOne_1();
                }

                else if (result == "2)" || result == "2")
                {
                    if (saves.GetString("main", "1-4") == "1")
                    {
                        Console.Clear();
                        Core.printl(Localization.RU.room1_notice14_finded);
                        RoomOne_1();
                    }
                    Console.Clear();
                    Core.printl(Localization.RU.room1_notice14_find);
                    saves.WriteString("main", "1-4", "1");
                    RoomOne_1();
                }

                else if (result == "3)" || result == "3")
                {
                    if (saves.GetString("main", "key") == "1")
                    {
                        Console.Clear();
                        for (int i = 0; i < 50; i++) if (i == 25) Core.print(Localization.RU.gameover); else Core.print(Global.textv);
                        Core.printl(Localization.RU.gameovertext);
                    }
                    else Console.Clear(); Core.printl(Localization.RU.needkey); RoomOne_1();
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

                else Core.printl(Localization.RU.choosecorrectvariant); RoomOne_1();
            }
            else Core.printl(Localization.RU.choosecorrectvariant); RoomOne_1();
        }

        static void RoomTwo_2() // Кухня
        {
            Console.Clear();
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
                    Core.printl(Localization.RU.nothingfind);
                    RoomTwo_2();
                }

                else if (result == "2" || result == "2)")
                {
                    if (saves.GetString("main", "2-4") == "1")
                    {
                        Console.Clear();
                        Core.printl(Localization.RU.room2_notice24_finded);
                        RoomOne_1();
                    }
                    Console.Clear();
                    Core.printl(Localization.RU.room2_notice24_find);
                    saves.WriteString("main", "2-4", "1");
                    RoomOne_1();
                }

                else if (result == "3" || result == "3)")
                {
                    Console.Clear();
                    Core.printl(Localization.RU.nothingfind);
                    RoomTwo_2();
                }

                else if (result == "4" || result == "4)")
                {
                    RoomOne_1();
                }

                else if (result == "5" || result == "5)")
                {
                    RoomSeven_7();
                }

                else Core.printl(Localization.RU.choosecorrectvariant); RoomTwo_2();
            }
            else Core.printl(Localization.RU.choosecorrectvariant); RoomTwo_2();
        }

        static void RoomThree_3() // Кабинет
        {
            Console.Clear();
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

            for (int i = 0; i < 50; i++) if (i == 25) Core.print(Localization.RU.variants); else Core.print(Global.textv); // Вывод отделения между инвентарём и вариантами
            Core.printl("");
            Core.printl("1)" + Localization.RU.room3_action1);
            Core.printl("2)" + Localization.RU.room3_action2);
            Core.printl("3)" + Localization.RU.room3_action3);
            Core.printl("4)" + Localization.RU.room3_action4);
            Core.printl(Localization.RU.typeneedaction);
            string result = Console.ReadLine();

            if (result != null)
            {
                if (result == "1)" || result == "1")
                {
                    if (saves.GetString("main", "4-1") == "1")
                    {
                        Core.printl(Localization.RU.room3_notice41_finded);
                        RoomThree_3();
                    }
                    Core.printl(Localization.RU.room3_notice41_find);
                    saves.WriteString("main", "4-1", "1");
                    RoomThree_3();
                }

                else if (result == "2)" || result == "2")
                {
                    Core.printl(Localization.RU.nothingfind);
                    RoomThree_3();
                }

                else if (result == "3)" || result == "3")
                {
                    if (saves.GetString("main", "key") == "1") Core.printl(Localization.RU.searchthis); Thread.Sleep(5000); RoomThree_3();
                    string[] passwords = { "06fada21d8639163167030f081af858e0b567dbb2eaec565b2f6ba50fd17ed1d", "6e5d967631bbc0c7d36eefbe398a75323cdd18b2a6e180b95b42add033524cac", "7400bc38db9838278e42498a258f124a1aa2ddaa844bf0a4ba478a709f98c0d1", "61304c99d645456f73c0126dfc69c6de249d52b816078bf2ec66ea0a1322e1aa", "eab5cab790f3bfbb6a5169c6f776aa7ae10637fbd2b61fcc1cc58a501c018728" };
                    int number = new Random().Next(1, 5);
                    Console.Clear();
                    Core.printl(passwords[number]);
                    string password = Console.ReadLine();
                    if (password != null)
                    {
                        string hashedpassword = Core.sha256hash(password);
                        string unhashedpassword = passwords[number];
                        if (hashedpassword == unhashedpassword)
                        {
                            Core.printl(Localization.RU.room3_acceptpassword);
                            saves.WriteString("main", "key", "1");
                            Thread.Sleep(15000);
                            RoomThree_3();
                        }
                        else Core.printl(Localization.RU.room3_wrongpassword); RoomThree_3();
                    }
                    else Core.printl(Localization.RU.room3_wrongpassword); RoomThree_3();
                }

                else if (result == "4)" || result == "4")
                {
                    Console.Clear();
                    RoomOne_1();
                }

                else Core.printl(Localization.RU.choosecorrectvariant); RoomThree_3();
            }
            else Core.printl(Localization.RU.choosecorrectvariant); RoomThree_3();
        }

        static void RoomFour_4() // Спальня
        {
            Console.Clear();
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
                    Console.Clear();
                    Core.printl(Localization.RU.nothingfind);
                    RoomFour_4();
                }

                else if (result == "2)" || result == "2")
                {
                    Console.Clear();
                    Core.printl(Localization.RU.nothingfind);
                    RoomFour_4();
                }

                else if (result == "3)" || result == "3")
                {
                    Console.Clear();
                    Core.printl(Localization.RU.nothingfind);
                    RoomFour_4();
                }

                else if (result == "4)" || result == "4")
                {
                    Console.Clear();
                    RoomOne_1();
                }

                else Core.printl(Localization.RU.choosecorrectvariant); RoomFour_4();
            }
            else Core.printl(Localization.RU.choosecorrectvariant); RoomFour_4();
        }

        static void RoomFive_5() // Чердак
        {
            Console.Clear();
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
                    Core.printl(Localization.RU.nothingfind);
                    RoomFive_5();
                }

                else if (result == "2)" || result == "2")
                {
                    Console.Clear();
                    Core.printl(Localization.RU.nothingfind);
                    RoomFive_5();
                }

                else if (result == "3)" || result == "3")
                {
                    if (saves.GetString("main", "3-4") == "1")
                    {
                        Core.printl(Localization.RU.room5_notice34_finded);
                        RoomFive_5();
                    }
                    Core.printl(Localization.RU.room5_notice34_find);
                    saves.WriteString("main", "3-4", "1");
                    RoomFive_5();
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
                else Core.printl(Localization.RU.choosecorrectvariant); RoomFive_5();
            }
            else Core.printl(Localization.RU.choosecorrectvariant); RoomFive_5();
        }

        static void RoomSix_6() // Крыша
        {
            Console.Clear();
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
                    Core.printl(Localization.RU.nothingfind);
                    RoomSix_6();
                }

                else if (result == "2)" || result == "2")
                {
                    Console.Clear();
                    Core.printl(Localization.RU.nothingfind);
                    RoomSix_6();
                }


                else if (result == "3)" || result == "3")
                {
                    RoomFive_5();
                }

                else Core.printl(Localization.RU.choosecorrectvariant); RoomSix_6();
            }
            else Core.printl(Localization.RU.choosecorrectvariant); RoomSix_6();
        }

        static void RoomSeven_7() // Подвал
        {
            Console.Clear();
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
                    Core.printl(Localization.RU.nothingfind);
                    RoomSeven_7();
                }

                else if (result == "2)" || result == "2")
                {
                    Console.Clear();
                    Core.printl(Localization.RU.nothingfind);
                    RoomSeven_7();
                }

                else if (result == "3)" || result == "3")
                {
                    RoomTwo_2();
                }

                else if (result == "4)" || result == "4")
                {
                    RoomEight_8();
                }

                else Core.printl(Localization.RU.choosecorrectvariant); RoomSeven_7();
            }
            else Core.printl(Localization.RU.choosecorrectvariant); RoomSeven_7();
        }

        static void RoomEight_8() // Разлом
        {
            Console.Clear();
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

                else Core.printl(Localization.RU.choosecorrectvariant); RoomEight_8();
            }
            else Core.printl(Localization.RU.choosecorrectvariant); RoomEight_8();
        }

        static void RoomNine_9() // Развилка
        {
            Console.Clear();
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

                else Core.printl(Localization.RU.choosecorrectvariant); RoomNine_9();
            }
            else Core.printl(Localization.RU.choosecorrectvariant); RoomNine_9();
        }

        static void RoomTen_10() // Спуск вниз
        {
            Console.Clear();
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

                else Core.printl(Localization.RU.choosecorrectvariant); RoomTen_10();
            }
            else Core.printl(Localization.RU.choosecorrectvariant); RoomTen_10();
        }

        static void RoomEleven_11() // Пещера
        {
            Console.Clear();
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
                if (result == "" || result == "")
                {

                }

                else if (result == "" || result == "")
                {

                }


                else if (result == "" || result == "")
                {

                }

                else Core.printl(Localization.RU.choosecorrectvariant); RoomSix_6();
            }
            else Core.printl(Localization.RU.choosecorrectvariant); RoomSix_6();
        }

        static void RoomTwelve_12() // Грот
        {
            Console.Clear();
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
                if (result == "" || result == "")
                {

                }

                else if (result == "" || result == "")
                {

                }


                else if (result == "" || result == "")
                {

                }

                else Core.printl(Localization.RU.choosecorrectvariant); RoomSix_6();
            }
            else Core.printl(Localization.RU.choosecorrectvariant); RoomSix_6();
        }
        private static void MissionImpossible()
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
}
