using System;

namespace Main {
    internal class Lobby {
        public int NowIndex { get; set; }
        public int EndIndex { get; set; }
        public Lobby() {
            NowIndex = 0;
            EndIndex = 0;
        }
        public void Show() {
            Console.ForegroundColor = ConsoleColor.White;
            Console.SetCursorPosition(Console.BufferWidth / 2 - 7, 8);
            Console.Write("唐老师营救公主");
            Console.ForegroundColor = NowIndex == 0 ? ConsoleColor.Red : ConsoleColor.White;
            Console.SetCursorPosition(Console.BufferWidth / 2 - 4, 12);
            Console.Write("开始游戏");
            Console.ForegroundColor = NowIndex == 1 ? ConsoleColor.Red : ConsoleColor.White;
            Console.SetCursorPosition(Console.BufferWidth / 2 - 4, 14);
            Console.Write("结束游戏");
            Console.ForegroundColor = ConsoleColor.White;
        }

        public void Show_Final() {
            Console.ForegroundColor = EndIndex == 0?ConsoleColor.Red:ConsoleColor.White;
            Console.SetCursorPosition(Console.BufferWidth / 2 - 6, 11);
            Console.Write("回到开始界面");
            Console.ForegroundColor = EndIndex == 1 ? ConsoleColor.Red : ConsoleColor.White;
            Console.SetCursorPosition(Console.BufferWidth / 2 - 4, 13);
            Console.Write("退出游戏");
        }
    }
}
