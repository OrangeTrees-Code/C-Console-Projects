using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;



namespace Main {
    abstract internal class EntityBase : IEntity {
        private int _health;
        public int X { get; set; }
        public int Y { get; set; }
        public string Icon { get; private set; }
        public (int min, int max) ATKRange { get; private set; }
        public ConsoleColor IconColor { get; private set; }
        public int Health {
            get => _health;
            set => _health = Math.Max(value, 0);
        }

        public bool IsAlive {
            get {
                return Health > 0;
            }
        }
        public EntityBase(int x, int y, string icon, ConsoleColor iconColor, (int min, int max) atkRange = default, int health = 0) {
            X = x;
            Y = y;
            Icon = icon;
            Health = health;
            IconColor = iconColor;
            ATKRange = atkRange;
        }

        public void DrawIfAlive() {
            if (IsAlive) {
                Console.SetCursorPosition(X, Y);
                Console.ForegroundColor = IconColor;
                Console.Write(Icon);
            }
        }
        public void Clear() {
            Console.SetCursorPosition(X, Y);
            Console.Write(" ");
        }
        public DamageInfoPayload Attack(EntityBase other) {
            DamageInfoPayload payload = new DamageInfoPayload();
            if (IsAlive&&other.IsAlive) {

                Random rng = new Random();
                int damageValue = rng.Next(ATKRange.min, ATKRange.max);
                other.Health -= damageValue;
                payload.InputValue(this, damageValue, other);
                
            }
            return payload;
        }


        public static double ComputeDistance(EntityBase e1, EntityBase e2) {
            int dx = e1.X / 2 - e2.X / 2;
            int dy = e1.Y - e2.Y;
            return Math.Sqrt(dx * dx + dy * dy);
        }

    }

    class DamageInfoPayload {
        public EntityBase Attacker;
        public int Damage;
        public EntityBase Victim;
        public bool IsValid;
        public DamageInfoPayload() {
            Attacker = null;
            Damage = 0;
            Victim = null;
            IsValid = false;
        }

        public void InputValue(EntityBase attacker,int damage,EntityBase victim) {
            IsValid = true;
            Attacker = attacker;
            Damage = damage;
            Victim = victim;
        }
    }

    
}
