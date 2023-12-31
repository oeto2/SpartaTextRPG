using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Linq.Expressions;
using System.Net.WebSockets;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace SpartaTextRPG
{
    public class Item
    {
        //아이템 이름
        public string name;
        //공격력
        public int damage;
        //방어력
        public int defens;
        //가격
        public int price;
        //정보
        public string info;
        //아이템 타입
        public string type;
    }

    //아이템 창
    internal class Inventory
    {
        //인벤토리
        public List<Item> inven = new List<Item>();

        public Item[] item = new Item[10];

        //게임에서 사용될 아이템 정보 업데이트
        public void SetItemInfo()
        {
            //Class 배열 초기화
            for (int i = 0; i < item.Length; i++)
            {
                item[i] = new Item();
            }

            item[0].name = "무쇠 갑옷";
            item[0].defens = 5;
            item[0].price = 500;
            item[0].info = "무쇠로 만들어져 튼튼한 갑옷입니다.";
            item[0].type = "A";

            item[1].name = "스파르타의 창";
            item[1].damage = 7;
            item[1].price = 1000;
            item[1].info = "스파르타의 전사들이 사용했다는 전설의 창입니다.";
            item[1].type = "W";

            item[2].name = "낡은 검";
            item[2].damage = 2;
            item[2].price = 100;
            item[2].info = "쉽게 볼 수 있는 낡은 검 입니다.";
            item[2].type = "W";
        }

        //시작 아이템 획득
        public void GetStartItmes()
        {
            int count = 0;
            count++;
            inven.Add(item[0]);
            inven.Add(item[1]);
            inven.Add(item[2]);
        }

        //아이템 획득
        public void GetItem(string _name)
        {
            switch (_name)
            {
                case "무쇠갑옷":
                    inven.Add(item[0]);
                    break;
                case "스파르타의 창":
                    inven.Add(item[1]);
                    break;
                case "낡은 검":
                    inven.Add(item[2]);
                    break;
            }
        }

        //장착중인 아이템인지 체크
        public void CheckEquipItem(string _itemName, string _itemType)
        {
            foreach(Item i in inven)
            {
            }
        }

        //아이템 장착
        public void EquipItem(string _name)
        {
            switch (_name)
            {
                case "무쇠 갑옷":
                    item[0].name = "[E]" + item[0].name;
                    Player.instance.armor = item[0].name;
                    Player.instance.defence += item[0].defens;
                    Player.instance.equipArmorNum++;
                    break;

                case "스파르타의 창":
                    //이미 착용중인 아이템이 있다면
                    if(Player.instance.equipArmorNum == 1)
                    {
                        //해당아이템을 장착해제 후 변경
                    }
                    else
                    {

                    }

                    item[1].name = "[E]" + item[1].name;
                        Player.instance.weapon = item[1].name;
                        Player.instance.attack = item[1].damage;
                        Player.instance.equipWeponNum++;
                    break;
                case "낡은 검":
                    item[2].name = "[E]" + item[2].name;
                    Player.instance.weapon = item[2].name;
                    Player.instance.attack = item[2].damage;
                    Player.instance.equipWeponNum++;
                    break;
            }
        }


        //현재 인벤토리 보여주기
        public int PrintInventory()
        {
            Console.Clear();
            Console.WriteLine("인벤토리");
            Console.WriteLine();
            Console.WriteLine("보유중인 아이템을 관리할 수 있습니다");
            Console.WriteLine();
            Console.WriteLine("[아이템 목록]");

            //아이템 보유 목록
            foreach (var item in inven)
            {
                switch (item.type)
                {
                    default:
                        break;

                    //무기일 경우
                    case "W":
                        Console.WriteLine("{0} | 공격력 +{1} | {2}", item.name, item.damage, item.info);
                        continue;

                    //방어구일 경우
                    case "A":
                        Console.WriteLine("{0} | 방어력 +{1} | {2}", item.name, item.defens, item.info);
                        continue;
                }
            }

            Console.WriteLine();
            Console.WriteLine("0. 나가기");
            Console.WriteLine("1. 장착관리");
            Console.WriteLine();
            Console.WriteLine("원하시는 행동을 입력해주세요.");
            Console.Write(">>");
            int input = int.Parse(Console.ReadLine());

            switch (input)
            {
                //나가기
                case 0:
                    return input;

                //장착관리
                case 1:
                    bool isEquipt = PrintEquipment(false);
                    //장착 성공시
                    if (isEquipt)
                        return 2;
                    break;
            }
            return 2;
        }

        //아이템을 보유하고 있는지 체크
        public bool CheckHaveItem(string _itemName)
        {
            foreach (Item i in inven)
            {
                if (i.name == _itemName)
                {
                    return true;
                }
            }

            return false;
        }

        //장비 장착 화면
        public bool PrintEquipment(bool inputWrong)
        {
            Console.Clear();
            Console.WriteLine("장비장착");
            Console.WriteLine();
            Console.WriteLine("원하는 아이템을 장착할 수 있습니다.");
            Console.WriteLine();
            Console.WriteLine("[아이템 목록]");

            //아이템 보유 목록
            foreach (var item in inven)
            {
                int count = 1;
                switch (item.type)
                {
                    default:
                        break;

                    //무기일 경우
                    case "W":
                        Console.WriteLine("{0} | 공격력 +{1} | {2}", item.name, item.damage, item.info);
                        continue;

                    //방어구일 경우
                    case "A":
                        Console.WriteLine("{0} | 방어력 +{1} | {2}", item.name, item.defens, item.info);
                        continue;
                }
            }
            Console.WriteLine();
            Console.WriteLine("0. 나가기");
            if(inputWrong)
            {
                Console.WriteLine();
                Console.Write("******해당 아이템을 보유하고 있지 않습니다!******");
            }
            Console.WriteLine();
            Console.WriteLine("장착하고 싶은 아이템의 이름을 입력하세요.");
            Console.Write(">>");

            string input = Console.ReadLine();

            //해당 아이템을 가지고 있는지 확인
            bool isHave = CheckHaveItem(input);
            if (isHave)
            {
                //해당아이템 장착
                EquipItem(input);
                return true;
            }
            else if(0 == int.Parse(input))
            {
                return true;
            }
            else
            {
                PrintEquipment(true);
            }

            return false;
        }
    }
}
