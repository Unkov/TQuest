using System;
using INIManager;
using System.Threading;
using System.IO;
using System.Net;

namespace TQuest
{
    internal class Program
    {
        public static HubManager saves = new HubManager(Global.spath);

        static void Main(string[] args)
        {
            // Титры в начале
            Console.Title = "TQuest";
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"Добро пожаловать в TQuest © 2022 {Environment.NewLine} Оригинальный автор: MineCR {Environment.NewLine} Автор портирования и переписи кода: kolya112");
            Thread.Sleep(5000);
            Console.Clear();

            Console.ForegroundColor = ConsoleColor.White;
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
            MissionImpossible();
            Console.Clear();
            RoomOne_1();
        }

        static void RoomOne_1() // Лобби
        {
            for (int i = 0; i < 50; i++) if (i == 25) Console.Write(Global.title); else Console.Write(Global.textv); // Вывод самой верхней строки
            Console.WriteLine("");
            Console.WriteLine(Localization.RU.room1_description);
            for (int i = 0; i < 50; i++) if (i == 25) Console.Write(Localization.RU.inventory); else Console.Write(Global.textv); // Вывод отделения между описанием комнаты и инвентарём
            Console.WriteLine("");
            string onefour = saves.GetString("main", "1-4");
            string twosix = saves.GetString("main", "2-6");
            string fourone = saves.GetString("main", "4-1");
            string threethree = saves.GetString("main", "3-3");
            string fivefour = saves.GetString("main", "5-4");

            if (saves.GetString("main", "1-4") == "1") Console.WriteLine("Записка: 1=4");

            if (saves.GetString("main", "2-6") == "1") Console.WriteLine("Записка: 2=6");

            if (saves.GetString("main", "4-1") == "1") Console.WriteLine("Записка: 4-1");

            if (saves.GetString("main", "3-3") == "1") Console.WriteLine("Записка: 3-3");

            if (saves.GetString("main", "5-4") == "1") Console.WriteLine("Записка: 5-4");

            for (int i = 0; i < 50; i++) if (i == 25) Console.Write(Localization.RU.variants); else Console.Write(Global.textv); // Вывод отделения между инвентарём и вариантами
            Console.WriteLine("");
            Console.WriteLine("1)" + Localization.RU.room1_action1);
            Console.WriteLine("2)" + Localization.RU.room1_action2);
            Console.WriteLine("3)" + Localization.RU.room1_action3);
            Console.WriteLine("4)" + Localization.RU.room1_action4);
            Console.WriteLine("5)" + Localization.RU.room1_action5);
            Console.WriteLine("6)" + Localization.RU.room1_action6);
            Console.WriteLine("7)" + Localization.RU.room1_action7);
            Console.WriteLine("Введите нужное действие");
            string result = Console.ReadLine();
            if (result != null)
            {
                if (result == "1)" || result == "1")
                {
                    Console.Clear();
                    Console.WriteLine("[Тумбочка с телефоном] Вы ничего не нашли");
                    RoomOne_1();
                }

                if (result == "2)" || result == "2")
                {
                    if (saves.GetString("main", "1-4") == "1")
                    {
                        Console.Clear();
                        Console.WriteLine("[Коврик] Вы уже осмотрели это место");
                        RoomOne_1();
                    }
                    Console.Clear();
                    Console.WriteLine("[Коврик] Вы нашли записку 1=4");
                    saves.WriteString("main", "1-4", "1");
                    RoomOne_1();
                }

               if (result == "3)" || result == "3")
               {
                    if (saves.GetString("main", "key") == "1")
                    {
                        Console.Clear();
                        for (int i = 0; i < 50; i++) if (i == 25) Console.Write(Localization.RU.gameover); else Console.Write(Global.textv);
                        Console.WriteLine(Localization.RU.gameovertext);
                    }
                    else Console.WriteLine(Localization.RU.needkey);
               }

                if (result == "4)" || result == "4")
                {
                    RoomTwo_2();
                }

                if (result == "5)" || result == "5")
                {
                    RoomThree_3();
                }

                if (result == "6)" || result == "6")
                {
                    RoomFour_4();
                }

                if (result == "7)" || result == "7")
                {
                    RoomFive_5();
                }
            }
        }

        static void RoomTwo_2() // Кухня
        {

        }

        static void RoomThree_3() // Кабинет
        {

        }

        static void RoomFour_4() // Спальня
        {

        }

        static void RoomFive_5() // Чердак
        {

        }

        static void RoomSix_6() // Крыша
        {

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
            /*Thread.Sleep(150);
            Console.Beep(740, 150);
            Thread.Sleep(150);
            Console.Beep(932, 150);
            Console.Beep(784, 150);
            Console.Beep(587, 1200);
            Thread.Sleep(75);
            Console.Beep(932, 150);
            Console.Beep(784, 150);
            Console.Beep(554, 1200);
            Thread.Sleep(75);
            Console.Beep(932, 150);
            Console.Beep(784, 150);
            Console.Beep(523, 1200);
            Thread.Sleep(150);
            Console.Beep(466, 150);
            Console.Beep(523, 150);*/
        }
    }
}
