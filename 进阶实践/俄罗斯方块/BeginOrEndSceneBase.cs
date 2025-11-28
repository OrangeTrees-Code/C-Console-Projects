using System;
using System.Collections.Generic;
using System.Reflection.Metadata.Ecma335;
using System.Text;

namespace 俄罗斯方块 {
    internal abstract class BeginOrEndSceneBase : ISceneUpdate {
        protected int _optionIndex = 0;
        protected int OptionIndex {
            get => _optionIndex;
            set => _optionIndex = Math.Clamp(value, 0, 1);
        }
        protected string _title {  get; set; }
        protected string _optionOne {  get; set; }

        protected abstract void EnterJLogic();
        public void Update() {
            Global.ShowText(_title, Console.BufferWidth / 2 - _title.Length, 5, ConsoleColor.White);
            ConsoleColor color1 = _optionIndex == 0 ? ConsoleColor.Red : ConsoleColor.White;
            Global.ShowText(_optionOne, Console.BufferWidth / 2 - _optionOne.Length, 8, color1);
            ConsoleColor color2 = _optionIndex == 1 ? ConsoleColor.Red : ConsoleColor.White;
            Global.ShowText("结束游戏", Console.BufferWidth / 2 - 4, 10, color2);
            switch (Console.ReadKey(true).Key) {
                case ConsoleKey.W:
                    OptionIndex--;
                    break;
                case ConsoleKey.S:
                    OptionIndex++; 
                    break;
                case ConsoleKey.J:
                    EnterJLogic();
                    break;
            }
        }
    }
}
