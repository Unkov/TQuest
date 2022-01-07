using System;

namespace TQuest
{
    public class Localization
    {
        public class RU
        {
            public static string inventory = "[Инвентарь: ]";
            public static string variants = "[Варианты: ]";
            public static string room1_description = "Вы находитесь в коридоре. Позади находится дверь на улицу, слева кухня, справа спальня, перед Вами дверь в кабинет и лестницв. У стены стоит тумба с телефоном, на полу грязный коврик."; // Описание первой комнаты
            public static string room1_action1 = "осмотреть: тумбочка с телефоном";
            public static string room1_action2 = "осмотреть: коврик";
            public static string room1_action3 = "осмотреть: входная дверь";
            public static string room1_action4 = "идти: кухня";
            public static string room1_action5 = "идти: кабинет";
            public static string room1_action6 = "идти: спальня";
            public static string room1_action7 = "идти: чердак";
            public static string gameover = "[ Конец! ]";
            public static string gameovertext = $"Вот и все, Вы прошли столь долгий, и, возможно, опасный путь. Спасибо, что прошли эту игру! Но это еще не все, когда-нибудь выйдет TQuest 2! {Environment.NewLine} Огромное спасибо MineCR за идею, сценарий и код на Lua.";
            public static string needkey = "Вам требуется ключ!";
        }
    }
}
