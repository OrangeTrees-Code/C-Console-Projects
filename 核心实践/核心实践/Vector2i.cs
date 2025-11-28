using System;
using System.Collections.Generic;
using System.Text;

namespace 贪吃蛇 {
    internal struct Vector2i {
        public int X;
        public int Y;

        public Vector2i(int x, int y) {
            X = x; Y = y;
        }

        public static bool operator ==(Vector2i p1, Vector2i p2) {
            return p1.X == p2.X && p1.Y == p2.Y;
        }
        public static bool operator !=(Vector2i p1, Vector2i p2) {
            return !(p1 == p2);
        }

    }
}
