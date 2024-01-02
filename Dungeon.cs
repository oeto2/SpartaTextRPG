using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Security.AccessControl;
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
            if (System_.instance.isInputWrong)
                Console.WriteLine("******잘못된 입력입니다!*******");
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
                        int dunInput = PrintClearDungeon(1, Player.instance.hp, Player.instance.gold);
                        return dunInput;

                    // 일반 던전 이동
                    case 2:
                        System_.instance.isInputWrong = false;
                        PrintClearDungeon(2, Player.instance.hp, Player.instance.gold);
                        break;

                    //어려운 던전 이동
                    case 3:
                        System_.instance.isInputWrong = false;
                        PrintClearDungeon(3, Player.instance.hp, Player.instance.gold);
                        break;

                    default:
                        System_.instance.isInputWrong = true;
                        return 4;
                }
            }
            return 4;
        }

        //던전 클리어
        public int PrintClearDungeon(int _dungeonLevel, int _curHP, int _curGold)
        {
            //플레이어 사망시 입력 값
            int d_input = 4;

            Console.Clear();

            //던전 난이도별 동작
            switch (_dungeonLevel)
            {
                //쉬운 던전
                case 1:
                    d_input = DungeonManager(_dungeonLevel, _curHP, _curGold);
                    break;

                //일반 던전
                case 2:
                    DungeonManager(_dungeonLevel, _curHP, _curGold);
                    break;

                //어려운 던전
                case 3:
                    DungeonManager(_dungeonLevel, _curHP, _curGold);
                    break;
            }

            Console.WriteLine();
            if (d_input == 0)
                Console.WriteLine("0. 새로시작");
            else
                Console.WriteLine("0. 나가기");
            Console.WriteLine();
            if (System_.instance.isInputWrong)
                Console.WriteLine("******잘못된 입력입니다!*******");
            Console.WriteLine("원하시는 행동을 입력해주세요");
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
                        if (d_input == 0)
                            return 0;
                        else
                            return 4;

                    default:
                        System_.instance.isInputWrong = true;
                        return 4;
                }
            }
        }

        //던전 난이도별 동작
        public int DungeonManager(int _dungeonLevel, int _curHP, int _curGold)
        {
            //던전 클리어 여부
            bool isClaer = false;
            //플레이어가 죽었는지
            bool isDead = false;

            switch (_dungeonLevel)
            {
                //쉬운 던전
                case 1:
                    //던전 권장 방어력
                    int needDefence = 5;
                    //던전 클리어 여부
                    isClaer = false;

                    //던전 클리어 조건 미달
                    if (Player.instance.defence < needDefence)
                    {
                        //40% 확률로 던전 실패
                        Random rand = new Random();
                        int num = rand.Next(5); //0,1,2,3,4
                        if (num < 1)
                            isClaer = true;
                        else
                            isClaer = false;
                    }
                    else
                        isClaer = true;

                    //던전 클리어 시
                    if (isClaer)
                    {
                        //클리어 골드
                        int clearGold = 1700;

                        Console.WriteLine("축하합니다!!");
                        Console.WriteLine("쉬운 던전을 클리어 하였습니다.");
                        Console.WriteLine();
                        Console.WriteLine("[탐험 결과]");

                        Random rand = new Random();
                        //방어력 차이
                        int defence_dff = needDefence - Player.instance.defence;

                        //방어력 비례로 체력 감소
                        int minusHp = rand.Next(20 + defence_dff, 36 + defence_dff);
                        Console.WriteLine("체력 {0} -> {1}", _curHP, _curHP - minusHp);

                        //공격력 비례로 골드 추가 획득
                        int addGold = rand.Next(Player.instance.damage, Player.instance.damage * 2);
                        clearGold += addGold;
                        Console.WriteLine("Gold {0} G -> {1} G", _curGold, _curGold + clearGold);

                        Player.instance.hp -= minusHp;
                        Player.instance.gold += clearGold;

                        //플레이어 사망
                        if (Player.instance.hp < 1)
                        {
                            isDead = true;
                            Console.Clear();
                            Console.WriteLine("게임 오버");
                            Console.WriteLine("플레이어가 사망했습니다.");
                            Player.instance.PlayerDead();
                            return 0;
                        }
                    }

                    //던전 실패
                    else
                    {
                        Console.WriteLine("던전 클리어 실패..");
                        Console.WriteLine("쉬운 던전을 클리어하지 못했습니다.");
                        Console.WriteLine();
                        Console.WriteLine("[탐험 결과]");
                        //체력 절반 감소
                        Console.WriteLine("체력 {0} -> {1}", _curHP, _curHP / 2);
                        Player.instance.hp /= 2;
                    }

                    break;

                //일반 던전
                case 2:
                    //던전 권장 방어력
                    needDefence = 11;
                    //던전 클리어 여부
                    isClaer = false;

                    //던전 클리어 조건 미달
                    if (Player.instance.defence < needDefence)
                    {
                        //40% 확률로 던전 실패
                        Random rand = new Random();
                        int num = rand.Next(5); //0,1,2,3,4
                        if (num < 1)
                            isClaer = true;
                        else
                            isClaer = false;
                    }
                    else
                        isClaer = true;

                    //던전 클리어 시
                    if (isClaer)
                    {
                        //클리어 골드
                        int clearGold = 1700;

                        Console.WriteLine("축하합니다!!");
                        Console.WriteLine("일반 던전을 클리어 하였습니다.");
                        Console.WriteLine();
                        Console.WriteLine("[탐험 결과]");

                        Random rand = new Random();
                        //방어력 차이
                        int defence_dff = needDefence - Player.instance.defence;

                        //방어력 비례로 체력 감소
                        int minusHp = rand.Next(20 + defence_dff, 36 + defence_dff);
                        Console.WriteLine("체력 {0} -> {1}", _curHP, _curHP - minusHp);

                        //공격력 비례로 골드 추가 획득
                        int addGold = rand.Next(Player.instance.damage, Player.instance.damage * 2);
                        clearGold += addGold;
                        Console.WriteLine("Gold {0} G -> {1} G", _curGold, _curGold + clearGold);

                        Player.instance.hp -= minusHp;
                        Player.instance.gold += clearGold;

                        //플레이어 사망
                        if (Player.instance.hp < 1)
                        {
                            Console.Clear();
                            Console.WriteLine("게임 오버");
                            Console.WriteLine("플레이어가 사망했습니다.");
                        }
                    }

                    //던전 실패
                    else
                    {
                        Console.WriteLine("던전 클리어 실패..");
                        Console.WriteLine("일반 던전을 클리어하지 못했습니다.");
                        Console.WriteLine();
                        Console.WriteLine("[탐험 결과]");
                        //체력 절반 감소
                        Console.WriteLine("체력 {0} -> {1}", _curHP, _curHP / 2);
                        Player.instance.hp /= 2;
                    }
                    break;
                case 3:
                    //던전 권장 방어력
                    needDefence = 17;
                    //던전 클리어 여부
                    isClaer = false;

                    //던전 클리어 조건 미달
                    if (Player.instance.defence < needDefence)
                    {
                        //40% 확률로 던전 실패
                        Random rand = new Random();
                        int num = rand.Next(5); //0,1,2,3,4
                        if (num < 1)
                            isClaer = true;
                        else
                            isClaer = false;
                    }
                    else
                        isClaer = true;

                    //던전 클리어 시
                    if (isClaer)
                    {
                        //클리어 골드
                        int clearGold = 2500;

                        Console.WriteLine("축하합니다!!");
                        Console.WriteLine("어려운 던전을 클리어 하였습니다.");
                        Console.WriteLine();
                        Console.WriteLine("[탐험 결과]");

                        Random rand = new Random();
                        //방어력 차이
                        int defence_dff = needDefence - Player.instance.defence;

                        //방어력 비례로 체력 감소
                        int minusHp = rand.Next(20 + defence_dff, 36 + defence_dff);
                        Console.WriteLine("체력 {0} -> {1}", _curHP, _curHP - minusHp);

                        //공격력 비례로 골드 추가 획득
                        int addGold = rand.Next(Player.instance.damage, Player.instance.damage * 2);
                        clearGold += addGold;
                        Console.WriteLine("Gold {0} G -> {1} G", _curGold, _curGold + clearGold);

                        Player.instance.hp -= minusHp;
                        Player.instance.gold += clearGold;

                        //플레이어 사망
                        if (Player.instance.hp < 1)
                        {
                            Console.Clear();
                            Console.WriteLine("게임 오버");
                            Console.WriteLine("플레이어가 사망했습니다.");
                        }
                    }

                    //던전 실패
                    else
                    {
                        Console.WriteLine("던전 클리어 실패..");
                        Console.WriteLine("어려운 던전을 클리어하지 못했습니다.");
                        Console.WriteLine();
                        Console.WriteLine("[탐험 결과]");
                        //체력 절반 감소
                        Console.WriteLine("체력 {0} -> {1}", _curHP, _curHP / 2);
                        Player.instance.hp /= 2;
                    }
                    break;
            }

            if (!isDead)
                return 4;
            else
                return 0;
        }
    }
}
