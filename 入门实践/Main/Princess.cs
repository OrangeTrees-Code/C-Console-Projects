using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Main {
    internal class Princess : EntityBase {
        public Princess(int x, int y, string icon, ConsoleColor iconColor, (int min, int max) atkRange = default, int health = 1) : base(x, y, icon, iconColor, atkRange, health) {
        }
    }
}
