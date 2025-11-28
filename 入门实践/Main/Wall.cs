using System;

namespace Main {
    internal class Wall {
        public int X { get; private set; }
        public int Y { get; private set; }
        public Wall(int x,int y) {
            X = x;
            Y = y;
        }
        public void Draw() {
            if(Console.ForegroundColor != ConsoleColor.Red) {
                Console.ForegroundColor = ConsoleColor.Red;
            }
            Console.SetCursorPosition(X, Y);
            Console.Write("■");
        }
    }
}
