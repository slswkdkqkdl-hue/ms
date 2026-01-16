using System;
using System.Runtime.InteropServices;

public class Enemy
{   
    public string name;
    public int hp;

    public int def;
    public int atk;
    public int speed;

}
    public class Slime : Enemy
    {
        public Slime(int Game_Level)
        {   
           name = "슬라임";

            switch (Game_Level)
            {
                case 1:
                    hp = 3;
                    def = 1;
                    atk = 1;
                    speed = 1;
                    break;
                case 2:
                    hp = 4;
                    def = 2;
                    atk = 2;
                    speed = 2;
                    break;
                case 3:
                    hp = 5;
                    def = 3;
                    atk = 3;
                    speed = 3;
                    break;
            }
        }
    }

    public class Monster_cat : Enemy
    {
       
        public Monster_cat(int Game_Level)
        {
            name =  "괴물고양이" ;  
            switch (Game_Level)
            {
                case 1:
                    hp = 5;
                    def = 2;
                    atk = 3;
                    speed = 2;
                    break;
                case 2:
                    hp = 6;
                    def = 3;
                    atk = 4;
                    speed = 3;
                    break;
                case 3:
                    hp = 7;
                    def = 4;
                    atk = 5;
                    speed = 4;
                    break;
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
                    def = 3;
                    atk = 4;
                    speed = 2;
                    break;
                case 2:
                    hp = 16;
                    def = 4;
                    atk = 5;
                    speed = 7;
                    break;
                case 3:
                    hp = 17;
                    def = 5;
                    atk = 6;
                    speed = 7;
                    break;
            }
        }
    }
