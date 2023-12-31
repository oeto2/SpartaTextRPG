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
        public int level = 0;
        public string playerClass = "모험가";
        public int attack = 0;
        public int defence = 0;
        public int hp = 10;
        public int gold = 0;
        public string weapon = "스파르타의 창";
        public string armor = "무쇠갑옷";

        public void PrintPlayerInfo(bool _isInputWrong)
        {
            Console.Clear();

            Console.WriteLine("상태보기");
            Console.WriteLine("캐릭터의 정보가 표시됩니다.");
            Console.WriteLine();
            Console.WriteLine("Lv. {0}", level);
            Console.WriteLine("직업: {0}", playerClass);
            Console.WriteLine("공격력: {0}", attack);
            Console.WriteLine("방어력: {0}", defence);
            Console.WriteLine("체력: {0}", hp);
            Console.WriteLine("Gold: {0}", gold);
            Console.WriteLine("착용중인 무기 : {0}", weapon);
            Console.WriteLine("착용중인 방어구 : {0}", armor);
            Console.WriteLine();
            Console.WriteLine("0. 나가기");
            Console.WriteLine();
            Console.WriteLine("원하시는 행동을 입력해주세요");
            Console.Write(">>");

            if (_isInputWrong)
            {
                Console.WriteLine("************************");
                Console.WriteLine("잘못된 입력입니다!");
            }
        }
    }
}
