using System;



class Character
{
        //public ,private , protected  //상속이되어있는 자식이 사용가능하게 열어주는 접근제어자
        protected string name;
        protected int level;
        protected int hp;
        protected int maxHP;
        protected int attack;
        protected int defense;

        protected int pointX;

        protected int pointY;
        
        public Character()
        {
            name = "";
            level = 1;
            maxHP = 100;
            hp = maxHP;
            attack = 30;
            defense = 20;

        }

        public virtual void ShowInfo()
        {   
            Console.WriteLine("============================");
            Console.WriteLine($"이름 : {name}");
            Console.WriteLine($"레벨 : {level}");
            Console.WriteLine($"HP : {hp}/{maxHP}");
            Console.WriteLine($"공격력 : {attack}");
            Console.WriteLine($"방어력 : {defense}");
            Console.WriteLine($"위치 X : {pointX}");
            Console.WriteLine($"위치 Y : {pointY}");
        }

        public  virtual  void aaa()
        {
                
        }

    }

    //자식클래스 : 전사
    class Warrior : Character
    {
        private int rage; //전사만의 고유 속성

        Random rand = new Random();
        
        public Warrior()
        {
            name = "전사";  //base.name과 같다. 부모꺼 명시적으로 호출
            attack = 60;
            defense = 40;
            maxHP = 150;
            hp = maxHP;
            rage = 0;

            pointX =    rand.Next(100,301);
            pointY = rand.Next(10,101);    

            Console.WriteLine("직업 : 전사");
        }

        // public override void ShowInfo()
        // {
        //     base.ShowInfo();
        //     Console.WriteLine("난 전사다 ");
        //     Console.WriteLine(rage);
        // }
    }   
    
    class skt : Character
    {
        private int sk;

        Random rand = new Random();
        public skt(int value1 , int value2)
        {
            name = "궁수";  //base.name과 같다. 부모꺼 명시적으로 호출
            attack = 60;
            defense = 40;
            maxHP = 150;
            hp = maxHP;
            sk = value1;     

            pointX = rand.Next(10,101);
            pointY = rand.Next(10,101);      

            Console.WriteLine("직업 : 궁수");
        }
    // public override void ShowInfo()
    // {
    //     base.ShowInfo();
    //     Console.WriteLine("난 궁수다  ");
    //     Console.WriteLine(sk);
    // }
        public override void aaa()
        {
            Console.WriteLine("궁수 특스 공격 발동!");
        }
            
    }


    internal class Program
    {
        static void Main(string[] args)
        {
            Console.Clear();
            List<Character> tet = new List<Character>();

            tet.Add(new Warrior());
            tet.Add(new skt(100,200));

            foreach(var temp in tet)
            {
              temp.ShowInfo();  
              temp.aaa();
            };
            

        }
}