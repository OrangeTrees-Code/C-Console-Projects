using System;
using System.Collections.Generic;
using System.Text;

namespace 贪吃蛇 {
    internal abstract class GameObject : IDraw {
        public Vector2i Position;
        public abstract void Draw(); 
    }
}
