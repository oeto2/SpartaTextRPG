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
        Home home = new Home();

        //종합 인벤토리
        public List<Item> inven = new List<Item>();
        //무기 인벤토리
        public List<Item> inven_W = new List<Item>();
        //방어구 인벤토리
        public List<Item> inven_A = new List<Item>();

        public Item item2 = new Item();

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

            UpdateInven_W();
            UpdateInven_A();
        }

        //무기 리스트 갱신
        public void UpdateInven_W()
        {
            foreach (Item i in inven)
            {
                if (i.type == "W")
                {
                    inven_W.Add(i);
                }
            }
        }

        //방어구 리스트 갱신
        public void UpdateInven_A()
        {
            foreach (Item i in inven)
            {
                if (i.type == "A")
                {
                    inven_A.Add(i);
                }
            }
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
        public void EquipItem(int _itemIndex, string _itemType)
        {
            int i = _itemIndex -1;

            //이미 해당 무기를 착용중인지
            bool alreadyEquip_W = false;
            //이미 해당 방어구를 착용중인지
            bool alreadyEquip_A = false;

            //무기를 장착할 경우
            if (_itemType == "W")
            {
                //장착할 무기
                Item weapon = inven_W[i];

                if (weapon.name.Contains("[E]"))
                {
                    alreadyEquip_W = true;
                }
                else
                {
                    inven_W[i].name = "[E]" + inven_W[i].name;
                    Player.instance.weapon = inven_W[i].name;
                    Player.instance.damage = inven_W[i].damage;
                }
            }

            //방어구를 장착할 경우
            else if (_itemType == "A")
            {
                //장착할 방어구
                Item armor = inven_A[i];

                if (armor.name.Contains("[E]"))
                {
                    alreadyEquip_A = true;
                }
                else
                {
                    inven_A[i].name = "[E]" + inven_A[i].name;
                    Player.instance.armor = inven_A[i].name;
                    Player.instance.defence = inven_A[i].damage;
                }
            }    
        }


        //인벤토리 창 보여주기
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
                    case  "W":
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
            Console.WriteLine("1. 무기 장착");
            Console.WriteLine("2. 방어구 장착");
            Console.WriteLine();
            Console.WriteLine("원하시는 행동을 입력해주세요.");
            Console.Write(">>");
            string input = Console.ReadLine();
            if (home.CheckNullEnter(input))
                return 2;
            else
            {
                switch (int.Parse(input))
                {
                    //나가기
                    case 0:
                        return int.Parse(input);

                    //장착 관리화면 이동
                    case 1:
                        int wEquipInput = PrintWeaponeEquipment();
                        return wEquipInput;

                    //방어구 장착
                    case 2:
                        int aEquipInput = PrintArmorEquipment();
                        return aEquipInput;
                }
            }
            return 2;
        }

        //아이템을 보유하고 있는지 체크
        public bool CheckHaveItem(int _itemNum, string _itemType)
        {
            int weapone_i = _itemNum - 1;
            int armor_i = _itemNum - 1;

            if(_itemType == "W")
            {
                foreach (Item i in inven)
                {
                    //해당 인덱스를 가진 아이템이 존재한다면
                    if ( weapone_i == _itemNum)
                    {
                        return true;
                    }
                    weapone_i++;
                }
            }

            else if (_itemType == "A")
            {
                foreach (Item i in inven)
                {
                    //해당 인덱스를 가진 아이템이 존재한다면
                    if (armor_i == _itemNum)
                    {
                        return true;
                    }
                    armor_i++;
                }
            }
            return false;
        }

        //장비 장착 초이스 화면
        public int PrintSelectEquip()
        {
            Console.Clear();
            Console.WriteLine("장비장착");
            Console.WriteLine();
            Console.WriteLine("어느 타입의 장비를 장착하시겠습니까?");
            Console.WriteLine();
            Console.WriteLine("0. 나가기");
            Console.WriteLine("1. 무기");
            Console.WriteLine("2. 방어구");
            Console.WriteLine();
            Console.WriteLine("원하시는 장비 타입을 입력해주세요.");
            Console.Write(">>");
            string input = Console.ReadLine();
            if (home.CheckNullEnter(input))
                return 0;
            else
            {
                switch (int.Parse(input))
                {
                    //나가기
                    case 0:
                        return int.Parse(input);

                    //무기 장착화면 이동
                    case 1:
                        return 1;

                    //방어구 장착화면 이동
                    case 2:
                        return 2;
                }
            }
            return 2;

        }

        //무기 장착 화면
        public int PrintWeaponeEquipment()
        {
            Console.Clear();
            Console.WriteLine("무기장착");
            Console.WriteLine();
            Console.WriteLine("보유중인 무기 아이템을 장착할 수 있습니다.");
            Console.WriteLine();
            Console.WriteLine("[무기 목록]");

            int weaponCount = 1;

            //무기 아이템 보유 목록
            foreach (var item in inven)
            {
                if(item.type == "W")
                {
                    Console.WriteLine("{3}.{0} | 공격력 +{1} | {2}", item.name, item.damage, item.info, weaponCount);
                    weaponCount++;
                }
            }
            Console.WriteLine();
            Console.WriteLine("0. 나가기");
            Console.WriteLine();
            Console.WriteLine("장착하고 싶은 아이템의 번호를 입력하세요.");
            Console.Write(">>");

            string input = Console.ReadLine();
            if (home.CheckNullEnter(input))
                PrintWeaponeEquipment();
            else
            {
                //해당 아이템을 가지고 있는지 확인
                bool isHave = CheckHaveItem(int.Parse(input), "W");
                if (isHave && int.Parse(input) != 0)
                {
                    //해당아이템 장착
                    EquipItem(int.Parse(input), "W");
                    PrintWeaponeEquipment();
                }
                
                switch(int.Parse(input))
                {
                    //나가기
                    case 0:
                        return 0;
                }
            }

            return 0;
        }

        //방어구 장착 화면
        public int PrintArmorEquipment()
        {
            Console.Clear();
            Console.WriteLine("방어구 장착");
            Console.WriteLine();
            Console.WriteLine("보유중인 방어구 아이템을 장착할 수 있습니다.");
            Console.WriteLine();
            Console.WriteLine("[방어구 목록]");

            int armorCount = 1;

            //방어구 아이템 보유 목록
            foreach (var item in inven)
            {
                if (item.type == "A")
                {
                    Console.WriteLine("{3}.{0} | 방어력 +{1} | {2}", item.name, item.defens, item.info, armorCount);
                    armorCount++;
                }
            }
            Console.WriteLine();
            Console.WriteLine("0. 나가기");
            Console.WriteLine();
            Console.WriteLine("장착하고 싶은 아이템의 번호를 입력하세요.");
            Console.Write(">>");

            string input = Console.ReadLine();
            if (home.CheckNullEnter(input))
                PrintArmorEquipment();
            else
            {
                //해당 아이템을 가지고 있는지 확인
                bool isHave = CheckHaveItem(int.Parse(input), "A");
                if (isHave && int.Parse(input) != 0)
                {
                    //해당아이템 장착
                    EquipItem(int.Parse(input), "A");
                    PrintArmorEquipment();
                }

                switch (int.Parse(input))
                {
                    //나가기
                    case 0:
                        return 0;
                }
            }

            return 0;
        }
    }
}
