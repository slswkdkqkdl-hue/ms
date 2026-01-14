using System;
using System.Formats.Asn1;
using System.Text;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Reflection;

class character_2
{
      protected string name;
        protected int level;
        protected int hp;
        protected int maxHP;
        protected int attack;
        protected int defense;

        public character_2()
        {
            name = "기본캐릭터";
            level = 1;
            maxHP = 100;
            hp = maxHP;
            attack = 30;
            defense = 20;

            Console.WriteLine($"캐릭터 {name} 생성!");
        }

        public void ShowInfo()
        {
            Console.WriteLine($"이름 : {name}");
            Console.WriteLine($"레벨 : {level}");
            Console.WriteLine($"HP : {hp}/{maxHP}");
            Console.WriteLine($"공격력 : {attack}");
            Console.WriteLine($"방어력 : {defense}");
        }
         //자식클래스 : 전사
    class Warrior : character_2
    {
        private int rage; //전사만의 고유 속성

        public Warrior()
        {
            name = "전사";
            attack = 60;
            defense = 40;
            maxHP = 150;
            hp = maxHP;
            rage = 0;

            Console.WriteLine("직업 : 전사");
        }

        public void ShowInfo2()
        {
            base.ShowInfo(); //부모의 ShowInfo 호출
            Console.WriteLine($"분노 : {rage}");
        }
    }


}




