using System;

abstract class GameUnit
    {
        protected string name;
        protected int hp;
        protected int maxHP;
        protected int moveSpeed;

        public GameUnit(string unitName, int health, int speed)
        {
            name = unitName;
            maxHP = health;
            hp = maxHP;
            moveSpeed = speed;
        }
   
        // 추상 메서드 (구현 안 됨 - 자식이 반드시 구현)
        public abstract void ShowInfo();
    }


    // 구체 클래스 1: 전사
    class Warrior : GameUnit
    {
        private int attack;
        private int defense;

        public Warrior(string name) : base(name, 200, 5)
        {
            attack = 80;
            defense = 50;
        }

        public override void ShowInfo()
        {
            Console.WriteLine($"━━━━━━━━━━━━━━━━━━━━");
            Console.WriteLine($"[전사] {name}");
            Console.WriteLine($"HP: {hp}/{maxHP}");
            Console.WriteLine($"공격력: {attack}");
            Console.WriteLine($"방어력: {defense}");
            Console.WriteLine($"이동속도: {moveSpeed}");
            Console.WriteLine($"━━━━━━━━━━━━━━━━━━━━");
        }
    }

    // 구체 클래스 2: 마법사
    class Wizard : GameUnit
    {
        private int magicPower;
        private int mana;

        public Wizard(string name) : base(name, 120, 4)
        {   
            base.name = name;
            base.maxHP = 120;
            base.moveSpeed = 4;
            
            magicPower = 150;
            mana = 100;
        }

        public override void ShowInfo()
        {
            Console.WriteLine($"━━━━━━━━━━━━━━━━━━━━");
            Console.WriteLine($"[마법사] {name}");
            Console.WriteLine($"HP: {hp}/{maxHP}");
            Console.WriteLine($"마력: {magicPower}");
            Console.WriteLine($"마나: {mana}");
            Console.WriteLine($"이동속도: {moveSpeed}");
            Console.WriteLine($"━━━━━━━━━━━━━━━━━━━━");
        }
    }