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
            public static string room1_notice14_find = "[Коврик] Вы нашли записку 1=4";
            public static string room1_notice14_finded = "[Коврик] Вы уже осмотрели это место";
            public static string room2_description = "Вы находитесь на кухне. Тут находится нехитрое кухонное убранство: стол со стульями, рукомойник и кухонный шкаф с различными тарелками, стаканами и банками с крупой. НА полу люк.";
            public static string room2_action1 = "осмотреть: стол";
            public static string room2_action2 = "осмотреть: кухонный шкаф";
            public static string room2_action3 = "осмотреть: рукомойник";
            public static string room2_action4 = "идти: коридор";
            public static string room2_action5 = "идти: подвал";
            public static string room2_notice24_find = "[Кухонный шкаф] Вы нашли записку 2=4";
            public static string room2_notice24_finded = "[Кухонный шкаф] Вы уже осмотрели это место";
            public static string gameover = "[ Конец! ]";
            public static string gameovertext = $"Вот и все, Вы прошли столь долгий, и, возможно, опасный путь. Спасибо, что прошли эту игру! Но это еще не все, когда-нибудь выйдет TQuest 2! {Environment.NewLine} Огромное спасибо MineCR за идею, сценарий и код на Lua.";
            public static string needkey = "Вам требуется ключ!";
            public static string typeneedaction = "Введите нужное действие";
            public static string notice14 = "Записка 1=4";
            public static string notice24 = "Записка 2=4";
            public static string notice41 = "Записка 4=1";
            public static string notice33 = "Записка 3=3";
            public static string notice54 = "Записка 5=4";
            public static string choosecorrectvariant = "Выберите корректный вариант";
            public static string nothingfind = "Вы нчего не нашли";
            public static string searchthis = "Вы уже осмотрели это место";
        }
    }
}
