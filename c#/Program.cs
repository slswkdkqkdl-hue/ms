using System;

class Program
{
    static void Main()
    {
        
        Console.WriteLine("=== 캐릭터생성 ===");
        Console.Write("캐릭터 이름일 입력하세요 ");
        string name = Console.ReadLine()!;
        Console.WriteLine($"환영합니다 {name}님!");
        Console.Write("시작 레벨을 입력하세요 ");
        string level = Console.ReadLine();
        int value = int.Parse(level);
        Console.WriteLine($"{name}의 시작 레벨은 {value}입니다");






    }
}
