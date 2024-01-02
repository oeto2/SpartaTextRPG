using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpartaTextRPG
{
    internal class Dungeon
    {
        //싱글톤
        public static Dungeon instance = new Dungeon();

        Home home = new Home();

        //던전 입구
        public int PrintDungeonGate()
        {
            Console.Clear();
            Console.WriteLine("던전입장");
            Console.WriteLine("이곳에서 던전으로 들어가기전 활동을 할 수 있습니다.");
            Console.WriteLine();
            Console.WriteLine("1. 쉬운 던전 | 방어력 5 이상 권장");
            Console.WriteLine("2. 일반 던전 | 방어력 11 이상 권장");
            Console.WriteLine("3. 어려운 던전 | 방어력 17 이상 권장");
            Console.WriteLine("0. 나가기");
            Console.WriteLine();
            Console.WriteLine("원하시는 행동을 입력해주세요.");
            Console.Write(">>");

            string input = Console.ReadLine();

            //Null입력 체크
            if (home.CheckNullEnter(input))
                return 4;
            else
            {
                switch (int.Parse(input))
                {
                    //나가기
                    case 0:
                        System_.instance.isInputWrong = false;
                        return int.Parse(input);

                    //쉬운 던전이동
                    case 1:
                        System_.instance.isInputWrong = false;
                        return int.Parse(input);

                    // 일반 던전 이동
                    case 2:
                        System_.instance.isInputWrong = false;
                        return int.Parse(input);

                    //어려운 던전 이동
                    case 3:
                        System_.instance.isInputWrong = false;
                        return int.Parse(input);

                    default:
                        System_.instance.isInputWrong = true;
                        return 4;
                }
            }
        }
    }
}
