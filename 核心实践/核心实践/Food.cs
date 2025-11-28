using System;
using System.Collections.Generic;
using System.Text;

namespace 贪吃蛇 {
    internal class Food : GameObject {
        public Food(Snake snake) {
            RandomSpawn(snake);
        }
        public override void Draw() {
            Global.ShowText("¤", Position, ConsoleColor.Cyan);
        }
        public void RandomSpawn(Snake snake) {
            Random rng = new Random();
            int x = rng.Next(2,(Console.BufferWidth/2-1))*2;
            int y = rng.Next(1, Console.BufferHeight-2);
            Position = new Vector2i(x,y);
            if (snake.CheckOverlap(Position)) {
                RandomSpawn(snake);
            }
        }
    }
}
