using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace 俄罗斯方块 {
    internal class GameInfo {
        public int Score { get; set; }
        public DateTime StartTime { get; set; }

        private Vector2i _position = new Vector2i(Console.BufferWidth - 8,Console.BufferHeight/3);

        public GameInfo() {
            Score = 0;
            StartTime = DateTime.Now;
        }

        public void ShowInfo() {
            for (int i = _position.X; i < Console.BufferWidth; i++) {
                Global.ShowText(" ", i, _position.Y);
                Global.ShowText(" ", i, _position.Y*2);
            }

            Global.ShowText(Score.ToString(), _position.X, _position.Y, ConsoleColor.White);
            var duration = DateTime.Now - StartTime;
            Global.ShowText(duration.ToString(@"hh\:mm\:ss"), _position.X, _position.Y*2, ConsoleColor.White);
        }
    }
}
