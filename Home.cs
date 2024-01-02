using System.Collections;
using System.Security.Cryptography.X509Certificates;
using static SpartaTextRPG.Home;

namespace SpartaTextRPG
{
    //시스템
    public class System_
    {
        public static System_ instance = new System_();
        //입력 값이 잘못되었는지
        public bool isInputWrong = false;
    }

    internal class Home
    {
        static void Main(string[] args)
        {
            //홈 클래스
            Home home = new Home();
            //캐릭터 클래스
            Player player = new Player();
            //인벤토리
            Inventory inven = new Inventory();

            int input = 0;

            //처음 시작했는지
            bool isFirst = true;

            //지금 현재 어느 창인지
            bool isHome = false;
            bool isState = false;
            bool isInven = false;
            bool isEquipment = false;

            //게임 시작
            while (true)
            {
                //게임 첫 시작시
                if (isFirst)
                {
                    inven.SetItemInfo(); //아이템 정보 업데이트
                    //inven.GetStartItmes(); //시작 아이템 획득
                    Shop.instance.UpdateProduct(); //상점 아이템 리스트 업데이트
                    isFirst = false;
                }

                //선택에 따른 행동
                switch (input)
                {
                    //홈 화면
                    case 0:
                        //메뉴 출력
                        input = ShowMenu(false);
                        break;

                    //상태 창
                    case 1:
                        //플레이어 정보 출력
                        input = player.PrintPlayerInfo();
                        break;

                    //인벤토리
                    case 2:
                        //인벤토리 정보 출력
                        input = inven.PrintInventory();
                        break;

                    //상점
                    case 3:
                        //상점 정보 출력
                        input = Shop.instance.PrintShop();
                        break;

                    //던전 입장
                    case 4:
                        //던전 입장
                        input = Dungeon.instance.PrintDungeonGate();
                        break;

                    //휴식 하기
                    case 5:
                        //던전 입장
                        input = Dungeon.instance.PrintDungeonGate();
                        break;
                }
            }

            //메뉴 보여주기
            int ShowMenu(bool isWrong)
            {
                Console.Clear();

                Console.WriteLine("스파르타 마을에 오신 여러분 환영합니다.");
                Console.WriteLine("이곳에서 던전으로 들어가기전 활동을 할 수 있습니다.");
                Console.WriteLine();
                Console.WriteLine("1. 상태 보기");
                Console.WriteLine("2. 인벤토리");
                Console.WriteLine("3. 상점");
                Console.WriteLine("4. 던전입장");
                Console.WriteLine();
                if (System_.instance.isInputWrong)
                    Console.WriteLine("******잘못된 입력입니다!*******");
                Console.WriteLine("원하시는 행동을 입력해주세요.");
                Console.Write(">>");


                string input = Console.ReadLine();

                //Null입력 체크
                if (home.CheckNullEnter(input))
                    return 0;
                else
                {
                    switch (int.Parse(input))
                    {
                        //상태 창 이동
                        case 1:
                            System_.instance.isInputWrong = false;
                            return int.Parse(input);

                        // 인벤토리 창 이동
                        case 2:
                            System_.instance.isInputWrong = false;
                            return int.Parse(input);

                        //상점 창 이동
                        case 3:
                            System_.instance.isInputWrong = false;
                            return int.Parse(input);

                        //던전 이동
                        case 4:
                            System_.instance.isInputWrong = false;
                            return int.Parse(input);

                        //휴식 하기
                        case 5:
                            System_.instance.isInputWrong = false;
                            return int.Parse(input);

                        default:
                            System_.instance.isInputWrong = true;
                            break;
                    }
                }
                return 0;
            }
        }

        //공백을 입력했는지
        public bool CheckNullEnter(string _input)
        {
            int intInput;
            bool isInt = int.TryParse(_input, out intInput);

            if (!isInt)
            {
                System_.instance.isInputWrong = true;
                return true;
            }

            switch (_input)
            {
                case "":
                    return true;

                default:
                    System_.instance.isInputWrong = false;
                    return false;
            }
        }
    }
}
