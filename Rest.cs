using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpartaTextRPG
{
    internal class Rest
    {
        public static Rest instance = new Rest();
        Home home = new Home();

        //플레이어가 휴식을 했는지
        bool isRest = false;

        //플레이어가 돈이 충분한지
        bool enoughGold = true;

        //원래 체력, 골드
        int beHp = 0;
        int beGold = 0;

        //휴식 하기
        public int PrintRest()
        {
            isRest = false;
            beHp = Player.instance.hp;
            beGold = Player.instance.gold;

            Console.Clear();
            Console.WriteLine("휴식하기");
            Console.WriteLine("500 G 를 내면 체력을 회복할 수 있습니다. (보유 골드 : {0} G)", Player.instance.gold);
            Console.WriteLine();
            Console.WriteLine("1. 휴식하기");
            Console.WriteLine("0. 나가기");
            Console.WriteLine();
            if(!enoughGold)
                Console.WriteLine("******Gold 가 부족합니다.*******");
            if (System_.instance.isInputWrong)
                Console.WriteLine("******잘못된 입력입니다!*******");
            Console.WriteLine("원하는 행동을 입력해주세요.");
            Console.Write(">>");
            string input = Console.ReadLine();

            if (home.CheckNullEnter(input))
                return 5;
            else
            {
                switch (int.Parse(input))
                {
                    case 0:
                        enoughGold = true;
                        return int.Parse(input);

                    case 1:
                        if (Player.instance.gold >= 500)
                        {
                            enoughGold = true;
                            StartRest();
                        }
                        else
                            enoughGold = false;
                            return 5;
                        break;

                    default:
                        System_.instance.isInputWrong = true;
                        break;
                }
            }
            return 5;
        }

        //휴식 시작
        public int StartRest()
        {
            //Player가 휴식하지 않았다면
            if(!isRest)
            {
                Player.instance.hp = 100;
                Player.instance.gold -= 500;
                isRest = true;
            }

            Console.Clear();
            Console.WriteLine("******휴식을 완료했습니다******");
            Console.WriteLine();
            Console.WriteLine("체력 {0} -> {1}", beHp, Player.instance.hp);
            Console.WriteLine("Gold {0} G -> {1} G", beGold, Player.instance.gold);
            Console.WriteLine();
            Console.WriteLine("0. 나가기");
            Console.WriteLine();
            if (System_.instance.isInputWrong)
                Console.WriteLine("******잘못된 입력입니다!*******");
            Console.WriteLine("원하는 행동을 입력해주세요.");
            Console.Write(">>");
            string input = Console.ReadLine();

            if (home.CheckNullEnter(input))
                StartRest();
            else
            {
                switch (int.Parse(input))
                {
                    case 0:
                        return int.Parse(input);

                    default:
                        System_.instance.isInputWrong = true;
                        StartRest();
                        break;
                }
            }
            return 5;
        }
    }
}
