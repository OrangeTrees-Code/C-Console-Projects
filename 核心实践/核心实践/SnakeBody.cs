using System;
using System.Collections.Generic;
using System.Text;

namespace 贪吃蛇 {
    enum E_SnakeBodyType {
        Head,
        Body,
    }
    internal class SnakeBody:GameObject {
        private E_SnakeBodyType _type;
        public SnakeBody(int x,int y,E_SnakeBodyType type) {
            _type = type;
            Position = new Vector2i(x,y);
        }

        public override void Draw() {
            switch (_type) {
                case E_SnakeBodyType.Head:
                    Global.ShowText("●", Position, ConsoleColor.Yellow);
                    break;
                case E_SnakeBodyType.Body:
                    Global.ShowText("◎", Position, ConsoleColor.Green);
                    break;
            }
        }
    }
}
