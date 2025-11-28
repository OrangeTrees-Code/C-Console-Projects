using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Security.Policy;

namespace Main {
    internal class GameState {
        public List<Wall> Walls { get; set; }
        private bool _turnFlag;
        public bool IsWIn { get; set; }
        public GameState() {
            Walls = new List<Wall>();
            for (int i = 0; i < Console.BufferWidth; i += 2) {
                Walls.Add(new Wall(i, 0));
                Walls.Add(new Wall(i, Console.BufferHeight - 1));
                Walls.Add(new Wall(i, Console.BufferHeight - 6));
            }
            for (int i = 0; i < Console.BufferHeight; i++) {
                Walls.Add(new Wall(0, i));
                Walls.Add(new Wall(Console.BufferWidth - 2, i));
            }
            _turnFlag = true;

            IsWIn = false;
        }
        public void ShowGuidance() {
            ClearLine(Console.BufferHeight - 5);
            Console.ForegroundColor = ConsoleColor.White;
            Console.SetCursorPosition(2, Console.BufferHeight - 5);
            Console.Write("开始和Boss战斗了,按J键继续");
        }
        public void ShowHealthInfo(Player player, Boss boss) {
            ClearLine(Console.BufferHeight - 4);
            ClearLine(Console.BufferHeight - 3);
            Console.ForegroundColor = ConsoleColor.White;
            Console.SetCursorPosition(2, Console.BufferHeight - 4);
            Console.Write($"玩家当前血量为{player.Health}");
            Console.SetCursorPosition(2, Console.BufferHeight - 3);
            Console.Write($"Boss当前血量为{boss.Health}");
        }
        public void ShowDamageInfo(DamageInfoPayload payload) {
            if (!payload.IsValid) {
                return;
            }
            
            string attacker = payload.Attacker is Player ? "Player" : "Boss";
            int damage = payload.Damage;
            string victim = payload.Victim is Player ? "Player" : "Boss";
            if (_turnFlag) {
                ClearLine(Console.BufferHeight - 4);
                Console.ForegroundColor = ConsoleColor.Red;
                Console.SetCursorPosition(2, Console.BufferHeight - 4);
                Console.Write($"{attacker}对{victim}造成了{damage}点伤害,{victim}剩余血量为{payload.Victim.Health}");
                _turnFlag = false;
            }
            else {
                ClearLine(Console.BufferHeight - 3);

                Console.ForegroundColor = ConsoleColor.Green;
                Console.SetCursorPosition(2, Console.BufferHeight - 3);
                Console.Write($"{attacker}对{victim}造成了{damage}点伤害,{victim}剩余血量为{payload.Victim.Health}");
                _turnFlag = true;
            }

        }
        public void ShowWalls() {
            foreach (var item in Walls) {
                (item as Wall).Draw();
            }
        }
        private void ClearLine(int line) {
            for (int i = 2; i < Console.BufferWidth - 2; i++) {
                Console.SetCursorPosition(i, line);
                Console.Write(" ");
            }
        }
        public void ShowSuccessInfo() {
            ClearLine(Console.BufferHeight - 2);
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.SetCursorPosition(2, Console.BufferHeight - 2);
            Console.Write("恭喜你通过了Boss的试炼,按J去营救公主");
        }

        public void ShowFailureInfo() {
            ClearLine(Console.BufferHeight - 2);
            Console.ForegroundColor = ConsoleColor.DarkRed;
            Console.SetCursorPosition(2, Console.BufferHeight - 2);
            Console.Write("你被Boss杀死了!");
        }

        public void ShowFinalScene() {
            Console.Clear();
            Console.SetCursorPosition(Console.BufferWidth / 2 - 4, 5);
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write("GAMEOVER");

            Console.SetCursorPosition(Console.BufferWidth / 2 - 4, 7);
            Console.ForegroundColor = IsWIn == true ? ConsoleColor.Green : ConsoleColor.Red;
            string info = IsWIn == true ? "英雄救美" : "惨遭屠杀";
            Console.Write(info);

        }

    }
}
