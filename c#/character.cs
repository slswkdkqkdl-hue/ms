using System;
using System.Formats.Asn1;
using System.Text;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Reflection;
class character
    {
        public string name;
        public int level;
        public int hp;
        public int maxHP;
        public int mp;
        public int maxMP;
        

        Random rand = new Random();

        public character(string name , int level )
        {
            this.name = name;
            this.level = level;
            this.hp = rand.Next(100,151);
            this.maxHP   = 200;
            this.mp = rand.Next(50,101);
            this.maxMP  = 100;

            ShowInfo();
        }
        public void ShowInfo()
        {   

            Console.WriteLine($"━━━━━━━━━━━━━━━━━━━━");
            Console.WriteLine($"이름: {name}");
            Console.WriteLine($"레벨: {level}");
            Console.WriteLine($"HP: {hp}/{maxHP}");
            Console.WriteLine($"MP: {mp}/{maxMP}");
            Console.WriteLine($"━━━━━━━━━━━━━━━━━━━━");
        }
        public void TakeDamage(int damage)
        {
            hp -= damage;
            if (hp < 0) hp = 0;
            
            Console.WriteLine($"공격 : {name}이(가) {damage} 데미지를 받았습니다!");
            Console.WriteLine($"   남은 HP: {hp}/{maxHP}");
        }

        public void Heal(int amount)
        {
            hp += amount;
            if (hp > maxHP) hp = maxHP;
            
            Console.WriteLine($"체력 : {name}의 HP가 {amount} 회복되었습니다!");
            Console.WriteLine($"   현재 HP: {hp}/{maxHP}");
        }
        public static void Game_Start()
        {   
            character player = null;

        while (true)
        {
            Console.WriteLine("==게임 시작==");
            Console.WriteLine("1. 캐릭터 생성");
            Console.WriteLine("2. 공격");
            Console.WriteLine("3. 회복");
            Console.WriteLine("0. 종료\n");
            Console.Write("선택 해주세요 : ");

            int input = int.Parse(Console.ReadLine());
            Console.WriteLine();

            switch (input)
            {
                case 1:
                    Console.WriteLine("이름을 입력해주세요");
                    string name = Console.ReadLine();
                    player = new character(name, 100);
                    Console.WriteLine();
                    Console.WriteLine("캐릭터가 생성되었습니다.");
                    break;
                case 2:
                    if (player == null) { Console.WriteLine("캐릭터 먼저 생성해주세요"); break; }
                    Console.Write("데미지 입력 :");
                    int damage = int.Parse(Console.ReadLine());
                    player.TakeDamage(damage);
                    player.ShowInfo();
                    break;
                case 3:
                    if (player == null) { Console.WriteLine("캐릭터 먼저 생성해주세요"); break; }
                    Console.Write("힐 입력 :");
                    int Heal = int.Parse(Console.ReadLine());
                    player.Heal(Heal);
                    player.ShowInfo();
                    break;
                case 0:
                    break;
            }
            if (player != null && player.hp == 0)
            {
                Console.WriteLine(" 캐릭터가 사망했습니다. 게임 종료 ");
                break;
            }
            if(input == 0 )break;
        }
    }

    }