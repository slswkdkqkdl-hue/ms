using System;
using System.Diagnostics.SymbolStore;
using System.Globalization;



class Program
{   
    class Animal
    {
        protected string name;
        protected int age;
        protected string sound;


        public virtual void MakeSound()
        {   
            Console.WriteLine($"{name} : {age}살");
            Console.Write($"{name}가 " );
        }
    }
    
    class Dog : Animal
    {
        
        public Dog(string name , int age , string sound)
        {
            base.name = name;
            base.age = age;
            base.sound = sound;
        }

        public override void MakeSound()
        {   
            base.MakeSound();
            Console.Write($"{sound} 짖는다\n");
            Console.WriteLine();
        }   
  
    }
    class Cat : Animal
    {
        public Cat(string name,int age, string sound)
        {
            base.name = name;
            base.age = age;
            base.sound = sound;
        }

        public override void MakeSound()
        {   
        
            base.MakeSound();
            Console.Write($"{sound} 운다\n");
            Console.WriteLine();
        }

        public void Test()
        {
            Console.WriteLine("테스트함수");
        }
    }



    /// <summary>
    /// ////////////////////////////////////////////////////////////////////////////////////////
    /// </summary>
    /// 
    /// 
    class Shop
    {
        public string Item_name; // 아이템 이름
        public int Item_price; // 아이템 가격
        public string Description; // 아이템 설명

        public int discount;

    }

    class Sword : Shop
    {
        public Sword(string Item_name , int Item_price , string Description, int discount)
        {
            base.Item_name = Item_name;
            base.Item_price = Item_price;
            base.Description = Description;
            base.discount = discount;
        }
    }
    class Gun : Shop
    {
        public Gun(string Item_name , int Item_price , string Description,int discount)
        {
            base.Item_name = Item_name;
            base.Item_price = Item_price;
            base.Description = Description;
            base.discount = discount;
        }
    }
    class Bow : Shop
    {
        public Bow(string Item_name , int Item_price , string Description,int discount)
        {
            base.Item_name = Item_name;
            base.Item_price = Item_price;
            base.Description = Description;
            base.discount = discount;
        }
    }




    static void Main()
    {
        Console.Clear();

        // 다양성 업캐스팅
        Animal a1 = new Cat("고양이",3,"야옹야옹");
        a1.MakeSound();
        Animal a2 = new Dog("강아지",5,"멍멍");
        a2.MakeSound();
 


        // 다운캐스팅
        Cat cat = (Cat)a1;  
        cat.MakeSound();
        cat.Test();


        //////////////////////////////////////////

        Shop A1 = new Sword("검",5000,"날카로운 검이다",20);
        Shop A2 = new Gun("총",10000,"비싼 총이다",10);
        Shop A3 = new Bow("활",3000,"신기하게 생긴 활이다",40);

        List<Shop> all = new List<Shop>();
        all.Add(A1);
        all.Add(A2);
        all.Add(A3);

        for(int i = 0; i < 3; i++)
        {
            int discount = all[i].discount;

            Console.WriteLine($"상품 명 : {all[i].Item_name}");
            Console.WriteLine($"상품 설명 : {all[i].Description}");
            Console.WriteLine($"원래 가격은 {all[i].Item_price}원 입니다");
            Console.WriteLine($"할인율 {discount}% 총 가격 {all[i].Item_price * (100 - discount) / 100}\n");

        }
        
            
    }
}


