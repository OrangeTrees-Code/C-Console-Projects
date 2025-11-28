using System;

namespace Main {
    internal class Player : EntityBase {
        public Move_dir Dir { get; set; }
        public bool IsFight { get; set; }
        public Player(int x, int y, string icon, ConsoleColor iconColor, (int min, int max) atkRange, int health) : base(x, y, icon, iconColor, atkRange, health) {
            Dir = Move_dir.Right;
            IsFight = false;
        }


        public void Move(params EntityBase[] Entitys) {
            int tempX = X;
            int tempY = Y;
            switch (Dir) {
                case Move_dir.Left:

                    X -= 2;
                    if (X < 2) X = 2;
                    break;
                case Move_dir.Right:

                    X += 2;
                    if (X > Console.BufferWidth - 4) X = Console.BufferWidth - 4;
                    break;
                case Move_dir.Up:

                    Y -= 1;
                    if (Y < 1) Y = 1;
                    break;
                case Move_dir.Down:

                    Y += 1;
                    if (Y > Console.BufferHeight - 7) Y = Console.BufferHeight - 7;
                    break;
            }
            foreach (EntityBase entity in Entitys) {
                if (entity!=null&&entity.IsAlive && entity.X == X && entity.Y == Y) {
                    X = tempX;
                    Y = tempY;
                }
            }
        }

        public void flush(params EntityBase[] Entitys) {
            Clear();
            Move(Entitys);
            DrawIfAlive();
        }

    }

    internal enum Move_dir {
        Up, Down, Left, Right,
    }
}
