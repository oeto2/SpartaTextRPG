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

        //휴식 하기
        public int PrintRest()
        {
            Console.Clear();
            Console.WriteLine("휴식하기");
            Console.WriteLine("500 G 를 내면 체력을 회복할 수 있습니다. (보유 골드 : {0} G)", Player.instance.gold);
            Console.WriteLine();
            Console.WriteLine("1. 휴식하기");
            Console.WriteLine("0. 휴식하기");
            Console.WriteLine();
            if (System_.instance.isInputWrong)
                Console.WriteLine("******잘못된 입력입니다!*******");
            Console.WriteLine("원하는 행동을 입력해주세요.");
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

                    default:
                        System_.instance.isInputWrong = true;
                        break;
                }
            }
        }
    }
}
