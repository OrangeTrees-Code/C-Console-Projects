using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace 贪吃蛇 {
    internal abstract class BeginOrEndSceneBase : IScenceUpdate {
        protected int _nowIndex = 0;
        protected int NowIndex {
            get {
                return _nowIndex;
            }
            set {
                if (value > 1) {
                    _nowIndex = 1;
                }
                else if (value < 0) {
                    _nowIndex = 0;
                }
                else {
                    _nowIndex = value;
                }
            }
        }

        protected abstract void LogicAfterEnterJ();

        protected string _title;
        protected string _optionOne;
        public void Update() {
            Global.ShowText(_title,Console.BufferWidth/2-_title.Length,5,ConsoleColor.White);
            ConsoleColor color1 = NowIndex == 0 ? ConsoleColor.Red : ConsoleColor.White;
            Global.ShowText(_optionOne, Console.BufferWidth / 2 - _optionOne.Length, 8, color1);
            ConsoleColor color2 = NowIndex == 1 ? ConsoleColor.Red : ConsoleColor.White;
            Global.ShowText("结束游戏",Console.BufferWidth/2-4,10,color2);
            switch (Console.ReadKey(true).Key) {
                case ConsoleKey.W:
                    _nowIndex--;
                    break;
                case ConsoleKey.S:
                    _nowIndex++;
                    break;
                case ConsoleKey.J:
                    LogicAfterEnterJ();
                    break;
            }
        }
    }
}
