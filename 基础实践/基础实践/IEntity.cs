using System;
using System.Collections.Generic;
using System.Text;
using 基础实践;

namespace Main {
    internal interface IEntity {
        void Draw(Map mapInfo);
        Vector2i GetGridPosition(Map mapInfo);
    }
}
