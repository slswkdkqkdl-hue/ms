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

}

// 자식 클래스들: 부모(character)를 상속받음 ( : character 추가 )
public class Warrior : character
{
    public Warrior()
    {
        Name = "전사";
        hp = 20;
        max_hp = hp;
        mp = 6;
        def = 5;
        atk = 2;
        speed = 3;
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
        def = 2;
        atk = 4;
        speed = 2;
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
        def = 3;
        atk = 3;
        speed = 4;
    }
}