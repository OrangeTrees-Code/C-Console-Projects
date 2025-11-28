using System;
using System.Collections.Generic;
using System.Text;

namespace 贪吃蛇 {
    internal class Map:IDraw {
        public Wall[] _walls;
        public Map() {
            _walls = new Wall[Console.BufferWidth + (Console.BufferHeight-3)*2];
            int index = 0;
            for (int i = 0; i < Console.BufferWidth; i+=2) {
                _walls[index++] = new Wall(i, 0);
                _walls[index++] = new Wall(i, Console.BufferHeight - 2);
            }
            for (int i = 1; i < Console.BufferHeight-2; i++) {
                _walls[index++] = new Wall(0, i);
                _walls[index++] = new Wall(Console.BufferWidth - 2, i);
            }
        }

        public void Draw() {
            foreach (Wall wall in _walls) {
                wall.Draw();
            }
        }
    }
}
