using System;

// 부모 클래스: 모든 캐릭터의 공통 분모
public class character
{
    public string Name;
    public int hp;
    public int max_hp;
    public int mp;
    public int def;
    public int atk;
    public int speed;

    public List<Skill> Skills = new List<Skill>();
    public virtual void Attack(Enemy enemy)
    {
        // 기본 공격 로직
    }
}



public class Warrior : character
{
    public Warrior()
    {
        Name = "전사";
        hp = 20;
        max_hp = hp;
        mp = 6;
        def = 3;
        atk = 2;
        speed = 3;

        Skills.Add(new Skill
        {
            Name = "밀치기",
            MpCost = 3,
            Desc = "(2 + atk) 데미지를 입힌다.",
            Effect = (user, enemy) =>
            {
                if(enemy.name == "슬라임")
                {
                    Console.SetCursorPosition(5,23);
                    Console.Write($"적에게 0 만큼 공격.");
                    Console.SetCursorPosition(5,24);
                    Console.Write($"아무련 효과가없었다..");
                }
                else
                {
                    int dmg = 2 + atk - enemy.def;
                    dmg = Math.Max(0, dmg);

                    enemy.hp -= dmg;
                    Console.SetCursorPosition(5,23);
                    Console.Write($"기술 : 밀치기 발동");
                    Console.SetCursorPosition(5,24);
                    Console.Write($"적에게 {dmg}만큼 공격");
                }
            }
        });

        Skills.Add(new Skill
        {
            Name = "분노",
            MpCost = 4,
            Desc = "atk + 2 (전투 종료 시 사라짐)",
            Effect = (user, enemy) =>
            {
                atk += 2;
                Console.SetCursorPosition(5,23);
                Console.Write($"기술 : 분노 발동");
                Console.SetCursorPosition(5,24);
                Console.Write($"적에게 Atk 2 증가");
            }
        });
    }
    public override void Attack(Enemy enemy)
    {
        int dmg = atk - enemy.def;
        dmg = Math.Max(0, dmg);
        enemy.hp -= dmg;

        Console.SetCursorPosition(5,24);
        Console.Write($"적에게 {dmg}만큼 공격");
    }
}


public class Wizard : character
{
    public Wizard()
    {
        Name = "마법사";
        hp = 14;
        max_hp = hp;
        mp = 10;
        def = 1;
        atk = 6;
        speed = 3;

        Skills.Add(new Skill
        {
            Name = "라이트닝",
            MpCost = 4,
            Desc = "(atk + atk) 데미지",
            Effect = (user, enemy) =>
            {   

                if(enemy.name == "슬라임")
                {
                    Console.SetCursorPosition(5,23);
                    Console.Write($"적에게 0 만큼 공격.");
                    Console.SetCursorPosition(5,24);
                    Console.Write($"아무련 효과가없었다..");
                }
                else
                {
                    
                    int dmg = (atk * 2) - enemy.def;
                    dmg = Math.Max(0, dmg);

                    enemy.hp -= dmg;
                    Console.SetCursorPosition(5,23);
                    Console.Write($"기술 : 라이트닝 발동");
                    Console.SetCursorPosition(5,24);
                    Console.Write($"적에게 {dmg}만큼 공격");
                }
               
            }
        });

        Skills.Add(new Skill
        {
            Name = "파이어볼",
            MpCost = 1,
            Desc = "(atk + 1) 데미지",
            Effect = (user, enemy) =>
            {

                if(enemy.name == "슬라임")
                {
                    Console.SetCursorPosition(5,23);
                    Console.Write($"적에게 0 만큼 공격.");
                    Console.SetCursorPosition(5,24);
                    Console.Write($"아무련 효과가없었다..");
                }
                else
                {
                    int dmg = (atk + 1) - enemy.def;
                    dmg = Math.Max(0, dmg);

                    enemy.hp -= dmg;
                    Console.SetCursorPosition(5,23);
                    Console.Write($"기술 : 파이어볼 발동");
                    Console.SetCursorPosition(5,24);
                    Console.Write($"적에게 {dmg}만큼 공격");  
                }
            }
        });
    }
    public override void Attack(Enemy enemy)
    {
        int dmg = atk - enemy.def;
        dmg = Math.Max(0, dmg);
        enemy.hp -= dmg;

        Console.SetCursorPosition(5,24);
        Console.Write($"적에게 {dmg}만큼 공격");
    }
}


public class Archer : character
{
    public Archer()
    {
        Name = "궁수";
        hp = 16;
        max_hp = hp;
        mp = 7;
        def = 2;
        atk = 5;
        speed = 4;


        Skills.Add(new Skill
        {
            Name = "기습 일격",
            MpCost = 6,
            Desc = "(atk + speed) 데미지",
            Effect = (user, enemy) =>
            {


                if(enemy.name == "슬라임")
                {
                    Console.SetCursorPosition(5,23);
                    Console.Write($"적에게 0 만큼 공격.");
                    Console.SetCursorPosition(5,24);
                    Console.Write($"아무련 효과가없었다..");
                }
                else
                {
                int dmg = (atk + speed) - enemy.def;
                dmg = Math.Max(0, dmg);

                enemy.hp -= dmg;
                Console.SetCursorPosition(5,23);
                Console.Write($"기술 : 기습일격 발동");
                Console.SetCursorPosition(5,24);
                Console.Write($"적에게 {dmg}만큼 공격");
                }
            }
        });

        Skills.Add(new Skill
        {
            Name = "트릭샷",
            MpCost = 3,
            Desc = "50% 확률로 강공격",
            Effect = (user, enemy) =>
            {
  
                
                if(enemy.name == "슬라임")
                {
                    Console.SetCursorPosition(5,23);
                    Console.Write($"적에게 0 만큼 공격.");
                    Console.SetCursorPosition(5,24);
                    Console.Write($"아무련 효과가없었다..");
                }
                else
                {    
                    Random rand = new Random();
                    int c = rand.Next(0, 100);

                    
                    if (c >= 50)
                    {   int dmg = (atk * 2) - enemy.def;
                        dmg = Math.Max(0,dmg);
                        enemy.hp -= dmg;
                        Console.SetCursorPosition(5,22);
                        Console.Write($"기술 : 트릭샷 발동");
                        Console.SetCursorPosition(5,23);
                        Console.Write($"적에게 큰데미지");
                        Console.SetCursorPosition(5,24);
                        Console.Write($"적에게 {dmg}만큼 공격");
                    }
                    else
                    {   
                        int dmg = atk - enemy.def;
                        dmg = Math.Max(0,dmg);
                        enemy.hp -= dmg;
                        Console.SetCursorPosition(5,22);
                        Console.Write($"기술 : 트릭샷 발동");
                        Console.SetCursorPosition(5,23);
                        Console.Write($"적에게 약한데미지");
                        Console.SetCursorPosition(5,24);
                        Console.Write($"적에게 {dmg}만큼 공격");
                    }
                    
                }

            
            }
        });


    }
    public override void Attack(Enemy enemy)
    {
        int dmg = atk - enemy.def;
        dmg = Math.Max(0, dmg);
        enemy.hp -= dmg;

        Console.SetCursorPosition(5,24);
        Console.Write($"적에게 {dmg}만큼 공격");
    }
}