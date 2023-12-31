using System.Collections;
using System.Security.Cryptography.X509Certificates;
using static SpartaTextRPG.Home;

namespace SpartaTextRPG
{
    internal class Home
    {
        //시스템
        public class System
        {
            //입력 값이 잘못되었는지
            public bool isInputWrong = false;
        }

        static void Main(string[] args)
        {
            //시스템
            System system = new System();
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


            //게임 시작
            while (true)
            {
                //게임 첫 시작시
                if (isFirst)
                {
                    inven.SetItemInfo(); //아이템 정보 업데이트
                    inven.GetStartItmes(); //시작 아이템 획득
                    isFirst = false;
                }

                //선택에 따른 행동
                switch (input)
                {
                    //홈 화면
                    case 0:

                        isHome = true;
                        isState = false;
                        isInven = false;

                        //홈 화면 입력 값
                        int homeInput;

                        //메뉴 출력
                        ShowMenu();
                        homeInput = int.Parse(Console.ReadLine());

                        //입력 체크
                        if (CheckInput(homeInput))
                        {
                            system.isInputWrong = false;
                            input = homeInput;
                        }
                        else
                        {
                            system.isInputWrong = true;
                            input = 0;
                        }
                        break;

                    //상태 창
                    case 1:
                        isState = true;
                        isHome = false;
                        isInven = false;


                        //상태 창 입력 값
                        int stateInput = 0;

                        //플레이어 정보 출력
                        player.PrintPlayerInfo(system.isInputWrong);
                        stateInput = int.Parse(Console.ReadLine());

                        //입력 체크
                        if (CheckInput(stateInput))
                        {
                            system.isInputWrong = false;
                            input = stateInput;
                        }
                        else
                        {
                            system.isInputWrong = true;
                            input = 1;
                        }
                        break;

                    case 2:
                        isInven = true;
                        isState = false;
                        isHome = false;

                        //인벤 창 입력 값
                        int InvenInput = 0;

                        //인벤 정보 출력
                        inven.PrintInventory();
                        InvenInput = int.Parse(Console.ReadLine());

                        //입력 체크
                        if (CheckInput(InvenInput))
                        {
                            system.isInputWrong = false;
                            input = InvenInput;
                        }
                        else
                        {
                            system.isInputWrong = true;
                            input = 2;
                        }

                        break;

                    case 3:
                        break;

                    case 4:
                        break;
                }
            }

            //메뉴 보여주기
            void ShowMenu()
            {
                Console.Clear();

                Console.WriteLine("스파르타 마을에 오신 여러분 환영합니다.");
                Console.WriteLine("이곳에서 던전으로 들어가기전 활동을 할 수 있습니다.");
                Console.WriteLine();
                Console.WriteLine("1. 상태 보기");
                Console.WriteLine("2. 인벤토리");
                Console.WriteLine("3. 상점");

                if (system.isInputWrong)
                {
                    Console.WriteLine("************************");
                    Console.WriteLine("잘못된 입력입니다!");
                }
            }


            //입력이 재대로 됐는지 확인하는 함수
            bool CheckInput(int _input)
            {
                //홈 화면일 경우
                if (isHome)
                {
                    if (_input > 0 && _input <= 4)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                //상태창 일 경우
                else if (isState)
                {
                    if (_input == 0)
                        return true;
                    else
                        return false;
                }
                //인벤토리일 경우
                else if (isInven)
                {
                    if (_input == 0)
                        return true;
                    else
                        return false;
                }
                else
                {
                    return false;
                }

            }
        }

    }
}
