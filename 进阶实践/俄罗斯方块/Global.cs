using System;
using System.Collections.Generic;
using System.Text;

namespace 俄罗斯方块 {

    internal class Global {
        public static void ShowText(string str, int x, int y, ConsoleColor color = ConsoleColor.White) {
            //if (y < 0) {
            //    return;
            //}
            Console.ForegroundColor = color;
            Console.SetCursorPosition(x, y);
            Console.Write(str);
        }

        public static void ShowText(string str, Vector2i v, ConsoleColor color = ConsoleColor.White) {
            //if (v.Y < 0) {
            //    return; 
            //}
            Console.ForegroundColor = color;
            Console.SetCursorPosition(v.X, v.Y);
            Console.Write(str);
        }
    }
}
