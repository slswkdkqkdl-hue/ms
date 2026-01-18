using System;
using System.Runtime.InteropServices;

public class Enemy
{   
    public string name;
    public int hp;
    public int max_hp;

    public int def;
    public int atk;
    public int speed;

    public static Random rand = new Random();
    
    public virtual void Attack(character character)
    {
        
    }
    public virtual void DoAction(character character)
    {
        // int pattern = rand.Next(0, 100); // 0~99

    }

}
    public class Slime : Enemy
    {
        public Slime(int Game_Level)
        {   
           name = "슬라임";

            switch (Game_Level)
            {
                case 1:
                    hp = 6;
                    def = 1;
                    atk = 1;
                    speed = 1;
                    max_hp =hp;
                    break;
                case 2:
                    hp = 7;
                    def = 1;
                    atk = 2;
                    speed = 2;
                    max_hp =hp;
                    break;
                case 3:
                    hp = 8;
                    def = 3;
                    atk = 3;
                    speed = 3;
                    max_hp =hp;
                    break;
            }
        }

        public override void Attack(character character)
        {
                int dmg = atk - character.def;
                dmg = Math.Max(0, dmg);
                character.hp = Math.Max(0, character.hp - dmg);


                Console.SetCursorPosition(5,23);
                Console.Write($"{name} 일반 공격");
                Console.SetCursorPosition(5,24);
                Console.Write($"{name}이 {dmg}만큼 공격");
        }
        public override void DoAction(character character)
        {
            int result = rand.Next(0,101);

            if(0 <= result)
            {
                Attack(character);
            }
        }

    }

    public class Monster_cat : Enemy
    {
       
        public Monster_cat(int Game_Level)
        {
            name =  "고양이" ;  
            switch (Game_Level)
            {
                case 1:
                    hp = 5;
                    def = 2;
                    atk = 3;
                    speed = 2;
                    max_hp =hp;
                    break;
                case 2:
                    hp = 7;
                    def = 2;
                    atk = 4;
                    speed = 3;
                    max_hp =hp;
                    break;
                case 3:
                    hp = 10;
                    def = 2;
                    atk = 5;
                    speed = 4;
                    max_hp =hp;
                    break;
            } 
        }
        public override void Attack(character character)
        {
                int dmg = atk - character.def;
                dmg = Math.Max(0, dmg);
                character.hp = Math.Max(0, character.hp - dmg);


                Console.SetCursorPosition(5,23);
                Console.Write($"{name} 일반 공격");
                Console.SetCursorPosition(5,24);
                Console.Write($"{name}이 {dmg}만큼 공격");
        }
        public override void DoAction(character character)
        {
            int result = rand.Next(0, 101);

            if (result >= 40) 
            {
                Attack(character);
            }
            else 
            {
                int healAmount = 3;
                int beforeHp = hp;
                hp = Math.Min(max_hp, hp + healAmount);
                
                Console.SetCursorPosition(5, 23);
                Console.Write($"{name} 기술 발동");
                Console.SetCursorPosition(5, 24);
                Console.Write($"{hp - beforeHp}만큼 회복! (현재:{hp})");
            }
          
        }
    }
    public class Endermen : Enemy
    {
        public Endermen(int Game_Level)
        {
            name = "엔더맨";

            switch (Game_Level)
            {
                case 1:
                    hp = 14;
                    def = 2;
                    atk = 3;
                    speed = 2;
                    max_hp =hp;
                    break;
                case 2:
                    hp = 15;
                    def = 3;
                    atk = 4;
                    speed = 7;
                    max_hp = hp;
                    break;
                case 3:
                    hp = 16;
                    def = 4;
                    atk = 5;
                    speed = 7;
                    max_hp =hp;
                    break;
            }
        }
        public override void Attack(character character)
        {
                int dmg = atk ;
                dmg = Math.Max(0, dmg);
                character.hp = Math.Max(0, character.hp - dmg);


                Console.SetCursorPosition(5,24);
                Console.Write($"적이 {dmg}만큼 공격");
        }
        public override void DoAction(character character)
        {
            int result = rand.Next(0,101);

            if(result >= 40)
            {
                Attack(character);

            }else
            {
                
                def +=1;

                Console.SetCursorPosition(5,23);
                Console.Write($"{name} 기술 발동");
                Console.SetCursorPosition(5,24);
                Console.Write($"{name}이가 Def 1증가");
            }
        }
    }
