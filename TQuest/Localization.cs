using System;

namespace TQuest
{
    public class Localization
    {
        public class RU
        {
            public static string welcomemessage = $"Добро пожаловать в TQuest © 2022 {Environment.NewLine} Оригинальный автор: MineCR {Environment.NewLine} Автор портирования и переписи кода: kolya112";
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
            public static string room3_description = "Вы находитесь в кабинете. У окна находится стол с креслом, в углу кабинета стоит массивный сейф с кодовым замком. Вдоль стены стоит книжный шкаф, уставленный множеством книг.";
            public static string room3_action1 = "осмотреть: книжный шкаф";
            public static string room3_action2 = "осмотреть: стол";
            public static string room3_action3 = "осмотреть: сейф";
            public static string room3_action4 = "идти: коридор";
            public static string room3_notice41_find = "[Книжный шкаф] Вы нашли записку 4=1";
            public static string room3_notice41_finded = "[Книжный шкаф] Вы уже осмотрели это место";
            public static string room3_password = "[Сейф] Введите пароль от сейфа, пользуясь найденными записками. Подсказка: записок должно быть 5";
            public static string room3_wrongpassword = "Неверный пароль, подумай еще раз";
            public static string room3_acceptpassword = "Пароль от сейфа оказался верный, что ж, молодец! Вот твой ключ";
            public static string room4_description = "Вы находитесь в спальне. Посреди комнаты стоит кровать, у стены находится шкаф-купе. Также вы видите возле кровати красивый торшер, наполняющий комнату мягким светом.";
            public static string room4_action1 = "осмотреть: кровать";
            public static string room4_action2 = "осмотреть: торшер";
            public static string room4_action3 = "осмотреть: шкаф-купе";
            public static string room4_action4 = "идти: коридор";
            public static string room5_description = "Вы находитесь на чердаке. Окно на крышу выбито. В углу валяются коробки, к потолку подвешен старый велосипед, на полу лежат потрепанные валенки.";
            public static string room5_action1 = "осмотреть: старый велосипед";
            public static string room5_action2 = "осмотреть: коробки";
            public static string room5_action3 = "осмотреть: потрёпанные валенки";
            public static string room5_action4 = "идти: коридор";
            public static string room5_action5 = "идти: крыша";
            public static string room5_notice34_find = "[Потрёпанные валенки] Вы нашли записку 3=4";
            public static string room5_notice34_finded = "[Потрёпанные валенки] Вы уже осмотрели это место";
            public static string room6_description = "Вы находитесь на крыше. Осторожо, не упадите! В центре возвышается кирпичная труба камина. А у самого края свили гнездо какие-то птицы.";
            public static string room6_action1 = "осмотреть: труба камина";
            public static string room6_action2 = "осмотреть: гнездо";
            public static string room6_action3 = "идти: чердак";
            public static string room7_description = "Вы находитесь в подвале. Тут довольно сыро и темно, но вы разглядели ржавую стиральную машинку, куча тряпья в корзине, а еще какой-то разлом в стене.";
            public static string room7_action1 = "осмотреть: стиралка";
            public static string room7_action2 = "осмотреть: куча шмоток";
            public static string room7_action3 = "идти: кухня";
            public static string room7_action4 = "идти: разлом";
            public static string gameover = "[ Конец! ]";
            public static string gameovertext = $"Вот и все, Вы прошли столь долгий, и, возможно, опасный путь. Спасибо, что прошли эту игру! Но это еще не все, когда-нибудь выйдет TQuest 2! {Environment.NewLine} Огромное спасибо MineCR за идею, сценарий и код на Lua.";
            public static string needkey = "Вам требуется ключ!";
            public static string typeneedaction = "Введите нужное действие";
            public static string notice14 = "Записка 1=4";
            public static string notice24 = "Записка 2=4";
            public static string notice41 = "Записка 4=1";
            public static string notice34 = "Записка 3=4";
            public static string notice54 = "Записка 5=4";
            public static string choosecorrectvariant = "Выберите корректный вариант";
            public static string nothingfind = "Вы ничего не нашли";
            public static string searchthis = "Вы уже осмотрели это место";
        }
    }
}
