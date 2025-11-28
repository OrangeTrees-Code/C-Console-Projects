using System;
using System.Collections.Generic;
using System.Text;
using static System.Net.Mime.MediaTypeNames;

namespace 贪吃蛇 {
    internal class Global {
        public static void ShowText(string text, int x, int y, ConsoleColor color) {
            Console.ForegroundColor = color;
            Console.SetCursorPosition(x, y);
            Console.Write(text);
        }

        public static void ShowText(string text, Vector2i v, ConsoleColor color) {
            Console.ForegroundColor = color;
            Console.SetCursorPosition(v.X, v.Y);
            Console.Write(text);
        }

        public static void Erase(int x,int y) {
            Console.SetCursorPosition(x, y);
            Console.Write(" ");
        }

        public static void Erase(Vector2i v) {
            Console.SetCursorPosition(v.X,v.Y);
            Console.Write(" ");
        }
    }
}
