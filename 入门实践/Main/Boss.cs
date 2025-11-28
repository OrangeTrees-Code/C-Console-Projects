using System;


namespace Main {
    internal class Boss:EntityBase {
        public Boss(int x,int y,string icon,ConsoleColor iconColor,(int min,int max) atkRange,int health):base(x,y,icon,iconColor,atkRange,health) {
            
        }

    }
}
