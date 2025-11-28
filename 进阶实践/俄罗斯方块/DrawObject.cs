using System;
using System.Collections.Generic;
using System.Text;

namespace 俄罗斯方块 {
    enum E_DrawType {
        Wall,
        Cube,
        Line,
        Tank,
        Left_Ladder,
        Right_ladder,
        Left_Long_Ladder,
        Right_Long_Ladder,
    }
    internal class DrawObject : IDraw {
        public Vector2i Position;
        public E_DrawType Type { get; set; }
        public DrawObject(E_DrawType type) {
            Type = type;
        }

        public DrawObject(E_DrawType type,int x,int y):this(type) {
            Position = new Vector2i(x,y);
        }
        public void Draw() {
            if(Position.Y < 0) {
                return;
            }
            Console.SetCursorPosition(Position.X, Position.Y);
            switch (Type) {
                case E_DrawType.Wall:
                    Console.ForegroundColor =  ConsoleColor.Red;
                    break;
                case E_DrawType.Cube:
                    Console.ForegroundColor = ConsoleColor.Blue;
                    break;
                case E_DrawType.Line:
                    Console.ForegroundColor = ConsoleColor.Green;
                    break;
                case E_DrawType.Tank:
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    break;
                case E_DrawType.Left_Ladder:
                case E_DrawType.Right_ladder:
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    break;
                case E_DrawType.Left_Long_Ladder:
                case E_DrawType.Right_Long_Ladder:
                    Console.ForegroundColor = ConsoleColor.Magenta;
                    break;
            }
            Console.Write("■");
        }

        public void Erase() {
            if (Position.Y < 0) {
                return; 
            }
            Global.ShowText(" ", Position);
        }
    }
}
