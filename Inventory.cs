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
        //해당 상품이 팔렸는지
        public bool isSell = false;
    }

    //아이템 창
    internal class Inventory
    {
        //싱글톤
        public static Inventory instance = new Inventory();

        Home home = new Home();

        //종합 인벤토리
        public List<Item> inven = new List<Item>();
        //무기 인벤토리
        public List<Item> inven_W = new List<Item>();
        //방어구 인벤토리
        public List<Item> inven_A = new List<Item>();

        public Item[] item = new Item[10];

        //현재 보유중인 무기의 갯수
        public int curWeaponNum = 0;
        //현재 보유중인 방어구의 갯수
        public int curArmorNum = 0;

        //게임에서 사용될 아이템 정보 업데이트
        public void SetItemInfo()
        {
            //Class 배열 초기화
            for (int i = 0; i < item.Length; i++)
            {
                instance.item[i] = new Item();
            }

            instance.item[0].name = "무쇠 갑옷";
            instance.item[0].defens = 5;
            instance.item[0].price = 500;
            instance.item[0].info = "무쇠로 만들어져 튼튼한 갑옷입니다.";
            instance.item[0].type = "A";

            instance.item[1].name = "스파르타의 창";
            instance.item[1].damage = 7;
            instance.item[1].price = 1000;
            instance.item[1].info = "스파르타의 전사들이 사용했다는 전설의 창입니다.";
            instance.item[1].type = "W";

            instance.item[2].name = "낡은 검";
            instance.item[2].damage = 2;
            instance.item[2].price = 100;
            instance.item[2].info = "쉽게 볼 수 있는 낡은 검 입니다.";
            instance.item[2].type = "W";

            instance.item[3].name = "수련자의 갑옷";
            instance.item[3].defens = 5;
            instance.item[3].price = 1000;
            instance.item[3].info = "수련에 도움을 주는 갑옷입니다.";
            instance.item[3].type = "A";

            instance.item[4].name = "무쇠 갑옷";
            instance.item[4].defens = 9;
            instance.item[4].price = 2000;
            instance.item[4].info = "수련에 도움을 주는 갑옷입니다.";
            instance.item[4].type = "A";

            instance.item[5].name = "스파르타 갑옷";
            instance.item[5].defens = 9;
            instance.item[5].price = 3500;
            instance.item[5].info = "스파르타의 전사들이 사용했다는 전설의 갑옷입니다.";
            instance.item[5].type = "A";

            instance.item[6].name = "낡은 검";
            instance.item[6].damage = 2;
            instance.item[6].price = 600;
            instance.item[6].info = "쉽게 볼 수 있는 낡은 검 입니다.";
            instance.item[6].type = "W";

            instance.item[7].name = "청동 도끼";
            instance.item[7].damage = 5;
            instance.item[7].price = 1500;
            instance.item[7].info = "어디선가 사용됐던거 같은 도끼입니다.";
            instance.item[7].type = "W";

            instance.item[8].name = "스파르타의 창";
            instance.item[8].damage = 7;
            instance.item[8].price = 3000;
            instance.item[8].info = "스파르타의 전사들이 사용했다는 전설의 창입니다.";
            instance.item[8].type = "W";
        }

        //시작 아이템 획득
        public void GetStartItmes()
        {
            int count = 0;
            count++;
            instance.inven.Add(instance.item[0]);
            instance.inven.Add(instance.item[1]);
            instance.inven.Add(instance.item[2]);

            UpdateInven_W();
            UpdateInven_A();
        }

        //무기 리스트 갱신
        public void UpdateInven_W()
        {
            if(instance.curWeaponNum == 0)
            {
                foreach (Item i in instance.inven)
                {
                    if (i.type == "W")
                    {
                        instance.inven_W.Add(i);
                        instance.curWeaponNum++;
                    }
                }
            }
            else
            {
                int indexNum = 0;
                foreach (Item i in instance.inven)
                {
                    if (i.type == "W")
                    {
                        indexNum++;
                    }

                    if (indexNum == (instance.curWeaponNum + 1))
                    {
                        //해당 아이템 획득
                        inven_W.Add(i);
                        instance.curWeaponNum++;
                    }
                }
            }
        }

        //방어구 리스트 갱신
        public void UpdateInven_A()
        {
            if (instance.curArmorNum == 0)
            {
                foreach (Item i in instance.inven)
                {
                    if (i.type == "A")
                    {
                        instance.inven_A.Add(i);
                        instance.curArmorNum++;
                    }
                }
            }
            else
            {
                int indexNum = 0;
                foreach (Item i in instance.inven)
                {
                    if (i.type == "A")
                    {
                        indexNum++;
                    }

                    if (indexNum == (instance.curArmorNum + 1))
                    {
                        //해당 아이템 획득
                        inven_A.Add(i);
                        instance.curArmorNum++;
                    }
                }
            }
        }

        //아이템 획득
        public void GetItem(Item _item)
        {
            //해당 아이템 인벤에 추가
            instance.inven.Add(_item);
            //리스트 갱신
            UpdateInven_A();
            UpdateInven_W();
        }

        //아이템 장착
        public void EquipItem(int _itemIndex, string _itemType)
        {
            int i = _itemIndex - 1;

            //이미 해당 무기를 착용중인지
            bool alreadyEquip_W = false;
            //이미 해당 방어구를 착용중인지
            bool alreadyEquip_A = false;

            //무기를 장착할 경우
            if (_itemType == "W")
            {
                //장착할 무기
                Item weapon = instance.inven_W[i];

                //이미 장착중인 아이템이 존재한다면
                if (weapon.name.Contains("[E]"))
                {
                    alreadyEquip_W = true;
                    string[] itemName = weapon.name.Split("[E]");

                    //이름 변경
                    weapon.name = itemName[1];

                    //해당 아이템 장착 해제
                    instance.inven_W[i].name = weapon.name;
                    Player.instance.weapon = "미착용";
                    Player.instance.damage = 0;
                }
                else
                {
                    //착용전에 장착중인 아이템이 존재하는지 확인
                    foreach (Item item in instance.inven_W)
                    {
                        //장착중인 아이템이 존재한다면
                        if (item.name.Contains("[E]"))
                        {
                            //찾은 아이템
                            Item findItem = item;
                            string[] itemName = findItem.name.Split("[E]");
                            string reName = itemName[1];

                            //현재 장착중인 아이템 해제
                            for (int j = 0; j < instance.inven_W.Count; j++)
                            {
                                if (instance.inven_W[j].name == findItem.name)
                                {
                                    instance.inven_W[j].name = reName;
                                }
                            }
                        }
                    }

                    //무기 착용
                    instance.inven_W[i].name = "[E]" + instance.inven_W[i].name;
                    Player.instance.weapon = instance.inven_W[i].name;
                    Player.instance.damage += instance.inven_W[i].damage;
                }
            }

            //방어구를 장착할 경우
            else if (_itemType == "A")
            {
                //장착할 방어구
                Item armor = instance.inven_A[i];

                //이미 장착중인 아이템이 존재한다면
                if (armor.name.Contains("[E]"))
                {
                    alreadyEquip_W = true;
                    string[] itemName = armor.name.Split("[E]");

                    //이름 변경
                    armor.name = itemName[1];

                    //해당 아이템 장착 해제
                    instance.inven_A[i].name = armor.name;
                    Player.instance.armor = "미착용";
                    Player.instance.defence = 0;
                }
                else
                {
                    //착용전에 장착중인 아이템이 존재하는지 확인
                    foreach (Item item in instance.inven_A)
                    {
                        //장착중인 아이템이 존재한다면
                        if (item.name.Contains("[E]"))
                        {
                            //찾은 아이템
                            Item findItem = item;
                            string[] itemName = findItem.name.Split("[E]");
                            string reName = itemName[1];

                            //현재 장착중인 아이템 해제
                            for (int j = 0; j < instance.inven_A.Count; j++)
                            {
                                if (instance.inven_A[j].name == findItem.name)
                                {
                                    instance.inven_A[j].name = reName;
                                }
                            }
                        }
                    }

                    //방어구 착용
                    instance.inven_A[i].name = "[E]" + instance.inven_A[i].name;
                    Player.instance.armor = instance.inven_A[i].name;
                    Player.instance.defence = instance.inven_A[i].defens;
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

            //무기 리스트 보여주기
            Console.WriteLine();
            Console.WriteLine("[무기]");
            foreach(Item i in instance.inven_W)
            {
                Console.WriteLine("- {0} | 공격력 +{1} | {2}", i.name, i.damage, i.info);
            }

            //방어구 리스트 보여주기
            Console.WriteLine();
            Console.WriteLine("[방어구]");
            foreach (Item i in instance.inven_A)
            {
                Console.WriteLine("- {0} | 방어력 +{1} | {2}", i.name, i.defens, i.info);
            }

            Console.WriteLine();
            Console.WriteLine("0. 나가기");
            Console.WriteLine("1. 무기 장착");
            Console.WriteLine("2. 방어구 장착");
            Console.WriteLine();
            if (System_.instance.isInputWrong)
            {
                Console.WriteLine("******잘못된 입력입니다!*******");
            }
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

                    default:
                        System_.instance.isInputWrong = true;
                        break;
                }
            }
            return 2;
        }

        //아이템을 보유하고 있는지 체크
        public bool CheckHaveItem(int _itemNum, string _itemType)
        {
            int weapone_i = 1;
            int armor_i = 1;

            if (_itemType == "W")
            {
                foreach (Item i in instance.inven)
                {
                    //해당 인덱스를 가진 아이템이 존재한다면
                    if (weapone_i == _itemNum)
                    {
                        return true;
                    }
                    weapone_i++;
                }
            }

            else if (_itemType == "A")
            {
                foreach (Item i in instance.inven)
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
            foreach (var item in instance.inven)
            {
                if (item.type == "W")
                {
                    Console.WriteLine("- {3}.{0} | 공격력 +{1} | {2}", item.name, item.damage, item.info, weaponCount);
                }
            }
            Console.WriteLine();
            Console.WriteLine("0. 나가기");
            Console.WriteLine();
            if (System_.instance.isInputWrong)
            {
                Console.WriteLine("******잘못된 입력입니다!*******");
            }
            Console.WriteLine("장착하고 싶은 아이템의 번호를 입력하세요.");
            Console.Write(">>");

            string input = Console.ReadLine();
            if (home.CheckNullEnter(input))
                PrintWeaponeEquipment();

            else
            {
                if ((int.Parse(input) > (instance.curWeaponNum)) || int.Parse(input) < 0)
                {
                    System_.instance.isInputWrong = true;
                    PrintWeaponeEquipment();
                }
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

                    switch (int.Parse(input))
                    {
                        //나가기
                        case 0:
                            PrintInventory();
                            break;
                    }
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
            foreach (var item in instance.inven)
            {
                if (item.type == "A")
                {
                    Console.WriteLine("- {3}.{0} | 방어력 +{1} | {2}", item.name, item.defens, item.info, armorCount);
                    armorCount++;
                }
            }
            Console.WriteLine();
            Console.WriteLine("0. 나가기");
            Console.WriteLine();
            if (System_.instance.isInputWrong)
            {
                Console.WriteLine("******잘못된 입력입니다!*******");
            }
            Console.WriteLine("장착하고 싶은 아이템의 번호를 입력하세요.");
            Console.Write(">>");

            string input = Console.ReadLine();
            if (home.CheckNullEnter(input))
                PrintArmorEquipment();
            else
            {
                if ((int.Parse(input) > (armorCount - 1)) || int.Parse(input) < 0)
                {
                    System_.instance.isInputWrong = true;
                    PrintArmorEquipment();
                }
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
                            PrintInventory();
                            break;
                    }
                }
            }

            return 0;
        }
    }
}
