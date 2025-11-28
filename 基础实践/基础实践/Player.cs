using System;
using System.Collections.Generic;
using System.Text;
using 基础实践;

namespace Main {
    internal class Player : EntityBase {
        public Player(int index) : base(EntityType.Player, index) {
        }

        public override void Draw(Map mapInfo) {
            var pos = GetGridPosition(mapInfo);
            Console.SetCursorPosition(pos.X, pos.Y);
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.Write("★");
        }
    }
}
