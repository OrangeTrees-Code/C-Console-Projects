using System;
using System.Collections.Generic;
using System.Text;
using 基础实践;

namespace Main {
    internal class Bot : EntityBase {
        public Bot(int index) : base(EntityType.Bot, index) {
        }

        public override void Draw(Map mapInfo) {
            var pos = GetGridPosition(mapInfo);
            Console.SetCursorPosition(pos.X, pos.Y);
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.Write("▲");
        }
    }
}
