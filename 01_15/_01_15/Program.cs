using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public interface A1
{
    public void cn()
    {
        int a = 10;
        a += 20;
        Console.WriteLine(a);
    }
     void ct();
}

namespace TextRPG 
{
    
    internal class Program 
    {   


        class ABC : A1
        {
            A1 vz;
            
            public void ct()
            {
                Console.WriteLine("반드시구현댐");
            }
        }



        static void Main(string[] args)
        {

            // MainGame mainGame = new MainGame();

            // mainGame.Initialize();
            // mainGame.Progress();

            A1 obj = new ABC();
            obj.cn();
            obj.ct();

        }
    }
}