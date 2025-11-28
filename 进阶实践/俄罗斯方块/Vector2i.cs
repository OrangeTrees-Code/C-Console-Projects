using System;
using System.Collections.Generic;
using System.Text;

namespace 俄罗斯方块 {
    internal struct Vector2i {
        public int X { get; set; }
        public int Y { get; set; }

        public Vector2i(int x,int y) {
            X = x; Y = y;
        }

        public static bool operator ==(Vector2i v1,Vector2i v2) {
            return v1.X==v2.X && v1.Y==v2.Y;
        }

        public static bool operator !=(Vector2i v1, Vector2i v2) {
            return !(v1 == v2);
        }

        public static Vector2i operator +(Vector2i v1, Vector2i v2) {
            return new Vector2i(v1.X+v2.X,v1.Y+v2.Y);
        }

    }
}
