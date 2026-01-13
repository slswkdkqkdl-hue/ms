using System;
using System.Formats.Asn1;
using System.Text;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Reflection;
class Program
{   

   class Monster
   {
        public string name;
        public int level;
        public int hp;
        public int attack;
        public int defense;
        public int expReward;


        public Monster()
        {
            name = "슬라임";
            level = 1;
            hp = 50;
            attack = 10;
            defense = 5;
            expReward = 10;
        }
    
        // 매개변수가 있는 생성자
        public Monster(string monsterName, int monsterLevel)
        {
            name = monsterName;
            level = monsterLevel;
            hp = 50 * level;
            attack = 10 * level;
            defense = 5 * level;
            expReward = 10 * level;
        }
   }

    

    static void Main()
    {   
        Console.Clear();
    
      
    }

    
}
