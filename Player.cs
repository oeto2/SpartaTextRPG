using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace SpartaTextRPG
{
    //플레이어의 정보 클래스
    public class Player
    {
        Home home = new Home();

        public int level = 1;
        public string playerClass = "모험가";
        public int attack = 0;
        public int defence = 0;
        public int hp = 10;
        public int gold = 0;
        public string weapon = "미착용";
        public string armor = "미착용";
        public int equipWeponNum = 0;
        public int equipArmorNum = 0;

        //싱글톤
        public static Player instance = new Player();

        public int PrintPlayerInfo()
        {
            Console.Clear();

            Console.WriteLine("상태보기");
            Console.WriteLine("캐릭터의 정보가 표시됩니다.");
            Console.WriteLine();
            Console.WriteLine("Lv. {0}", instance.level);
            Console.WriteLine("직업: {0}", instance.playerClass);
            Console.WriteLine("공격력: {0}", instance.attack);
            Console.WriteLine("방어력: {0}", instance.defence);
            Console.WriteLine("체력: {0}", instance.hp);
            Console.WriteLine("Gold: {0}", instance.gold);
            Console.WriteLine("착용중인 무기 : {0}", instance.weapon);
            Console.WriteLine("착용중인 방어구 : {0}", instance.armor);
            Console.WriteLine();
            Console.WriteLine("0. 나가기");
            Console.WriteLine();
            Console.WriteLine("원하시는 행동을 입력해주세요");
            Console.Write(">>");

            string input = Console.ReadLine();

            if (home.CheckNullEnter(input))
                return 1;
            else
            {
                switch (int.Parse(input))
                {
                    case 0:
                        return int.Parse(input);
                }
            }

            return 1;
        }
    }
}
