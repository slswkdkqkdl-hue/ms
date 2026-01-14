using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp41
{
//## 6.8 실전 프로젝트: RPG 캐릭터 시스템

//### 💻 종합 예제


    class Program
    {   
       
        static void Main(string[] args)
        {
             Console.Clear();
            string input ;

            while (true)
            {
                Console.WriteLine("==============");
                input = Console.ReadLine();

                switch (input)
                {
                    case"1":
                        Console.WriteLine("눌림=================");
                        break;
                    case "2":
                        Console.WriteLine("으아아아아앙아아아아아아");
                        break;
                    case "3":
                        Console.Clear();
                        break;
                }
                if(input == "4") break;
            }
            

        }
    }
}