using System;
using System.Collections.Generic;
using System.Text;

namespace 贪吃蛇 {
    internal class Wall : GameObject {
        public Wall(int x,int y) {
            Position = new Vector2i(x,y);
        }
        public override void Draw() {
            Global.ShowText("■",Position,ConsoleColor.Red);
        }
    }
}
