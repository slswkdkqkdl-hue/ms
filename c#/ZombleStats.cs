using System;

class ZombleStats
{
    public string Name;
    public int Attack;
    public int Defense;
    public int Hp;
    public int Luck;
    public int KKK;

    static Random rand = new Random();

    public static ZombleStats CreateRandom(int index)
    {
        return new ZombleStats
        {
            Name = $"Zombie_{index}",
            Attack = rand.Next(5, 15),
            Defense = rand.Next(1, 10),
            Hp = rand.Next(50, 150),
            Luck = rand.Next(0, 5),
            KKK = rand.Next(0, 3)
        };
    }

    public void Print()
    {
        Console.WriteLine(
            $"{Name} | ATK:{Attack} DEF:{Defense} HP:{Hp} LUCK:{Luck} KKK:{KKK}"
        );
    }
}
