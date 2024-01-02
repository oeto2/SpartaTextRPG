using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using System.Xml;

namespace SpartaTextRPG
{
    internal class Shop
    {
        public static Shop instance = new Shop();

        Home home = new Home();

        public List<Item> product = new List<Item>();

        //상점 물품 리스트
        public List<Item> shopProduct = new List<Item>();

        //판매 아이템 리스트
        public List<Item> sellItem = new List<Item>();

        //플레이어가 물품을 구매했는지
        public bool isBuy = false;

        //플레이어가 돈이 충분한지
        public bool enughMoney = true;

        //이미 판매된 아이템인지
        public bool isAreadySell = false;

        //상점 물품 업데이트
        public void UpdateProduct()
        {
            for (int i = 3; i <= 10; i++)
            {
                instance.product.Add(Inventory.instance.item[i]);
            }
        }

        //상점 창 
        public int PrintShop()
        {
            Console.Clear();

            Console.WriteLine("상점");
            Console.WriteLine("필요한 아이템을 얻을 수 있는 상점입니다.");
            Console.WriteLine();
            Console.WriteLine("[보유골드]");
            Console.WriteLine("{0} G", Player.instance.gold);
            Console.WriteLine();
            Console.WriteLine("[방어구 목록]");

            //상점 방어구 목록 보여주기
            foreach (Item i in instance.product)
            {
                if (i.type == "A")
                {
                    //판매되지 않은 경우
                    if (!i.isSell)
                        Console.WriteLine("- {0} | 방어력 +{1} | {2} | {3} G", i.name, i.defens, i.info, i.price);
                    else
                        Console.WriteLine("- {0} | 방어력 +{1} | {2} | {3}", i.name, i.defens, i.info, "구매 완료");
                }
            }
            Console.WriteLine();
            Console.WriteLine("[무기 목록]");
            //상점 무기 목록 보여주기
            foreach (Item i in instance.product)
            {
                if (i.type == "W")
                {
                    //판매되지 않은 경우
                    if (!i.isSell)
                        Console.WriteLine("- {0} | 공격력 +{1} | {2} | {3} G", i.name, i.damage, i.info, i.price);
                    else
                        Console.WriteLine("- {0} | 공격력 +{1} | {2} | {3}", i.name, i.damage, i.info, "구매 완료");
                }
            }
            Console.WriteLine();
            Console.WriteLine("0. 나가기");
            Console.WriteLine("1. 아이템 구매");
            Console.WriteLine("2. 아이템 판매");
            Console.WriteLine();
            if (System_.instance.isInputWrong)
            {
                Console.WriteLine("******잘못된 입력입니다!*******");
            }
            Console.WriteLine("원하시는 행동을 입력해주세요.");
            Console.Write(">>");


            string input = Console.ReadLine();
            //Null입력 체크
            if (home.CheckNullEnter(input))
                return 3;
            else
            {
                switch (int.Parse(input))
                {
                    //나가기
                    case 0:
                        System_.instance.isInputWrong = false;
                        return 0;

                    //아이템 구매
                    case 1:
                        System_.instance.isInputWrong = false;
                        PrintShop_Buy();
                        break;

                    //아이템 판매
                    case 2:
                        System_.instance.isInputWrong = false;
                        PrintShop_Sell();
                        break;


                    default:
                        System_.instance.isInputWrong = true;
                        break;
                }
            }
            return 3;
        }

        //상점 구매창 
        public int PrintShop_Buy()
        {
            Console.Clear();

            Console.WriteLine("상점 - 아이템 구매");
            Console.WriteLine("필요한 아이템을 얻을 수 있는 상점입니다.");
            Console.WriteLine();
            Console.WriteLine("[보유골드]");
            Console.WriteLine("{0} G", Player.instance.gold);
            Console.WriteLine();
            Console.WriteLine("[방어구 목록]");

            //아이템 번호
            int itemNum = 1;



            //상점 방어구 목록 보여주기
            foreach (Item i in instance.product)
            {
                if (i.type == "A")
                {
                    instance.shopProduct.Add(i);

                    if (!i.isSell)
                        Console.WriteLine("- {4}.{0} | 방어력 +{1} | {2} | {3} G", i.name, i.defens, i.info, i.price, itemNum);
                    else
                        Console.WriteLine("- {4}.{0} | 방어력 +{1} | {2} | {3}", i.name, i.defens, i.info, "구매완료", itemNum);

                    itemNum++;
                }
            }
            Console.WriteLine();
            Console.WriteLine("[무기 목록]");
            //상점 무기 목록 보여주기
            foreach (Item i in instance.product)
            {
                if (i.type == "W")
                {
                    instance.shopProduct.Add(i);

                    if (!i.isSell)
                        Console.WriteLine("- {4}.{0} | 공격력 +{1} | {2} | {3} G", i.name, i.damage, i.info, i.price, itemNum);
                    else
                        Console.WriteLine("- {4}.{0} | 공격력 +{1} | {2} | {3}", i.name, i.damage, i.info, "구매완료", itemNum);
                    itemNum++;
                }
            }
            Console.WriteLine();
            Console.WriteLine("0. 나가기");
            Console.WriteLine();

            if (isAreadySell)
                Console.WriteLine("******이미 구매한 아이템입니다!*******");
            if (isBuy)
                Console.WriteLine("******해당 상품을 구매했습니다!*******");
            else if (!enughMoney)
                Console.WriteLine("******소지금이 부족합니다!*******");
            if (System_.instance.isInputWrong)
                Console.WriteLine("******잘못된 입력입니다!*******");

            Console.WriteLine("원하시는 행동을 입력해주세요.");
            Console.Write(">>");

            string input = Console.ReadLine();
            //Null입력 체크
            if (home.CheckNullEnter(input))
                PrintShop_Buy();
            else
            {
                bool isHave = CheckHaveProduct(int.Parse(input));
                if (isHave && int.Parse(input) != 0)
                {
                    //해당아이템 구매
                    BuyProduct(int.Parse(input));
                }

                switch (int.Parse(input))
                {
                    //나가기
                    case 0:
                        System_.instance.isInputWrong = false;
                        enughMoney = true;
                        isAreadySell = false;
                        isBuy = false;
                        return 0;

                    default:
                        if (!isHave)
                            System_.instance.isInputWrong = true;
                        PrintShop_Buy();
                        break;
                }
            }
            return 0;
        }

        //상점 판매창 
        public int PrintShop_Sell()
        {
            Console.Clear();

            Console.WriteLine("상점 - 아이템 판매");
            Console.WriteLine("판매할 아이템 번호를 입력해주세요.");
            Console.WriteLine();
            Console.WriteLine("[보유골드]");
            Console.WriteLine("{0} G", Player.instance.gold);

            //아이템 번호
            int itemNum = 1;

            //무기 리스트 보여주기
            Console.WriteLine();
            Console.WriteLine("[무기]");
            foreach (Item i in Inventory.instance.inven_W)
            {
                //이미 해당 아이템이 리스트에 존재하는지
                Item findItem = sellItem.Find(x => x.name.Equals(i.name));

                //존재하지 않다면 리스트에 추가
                if (findItem == null)
                    sellItem.Add(i);

                Console.WriteLine("-{3}.{0} | 공격력 +{1} | {2} | {4} G", i.name, i.damage, i.info, itemNum, i.price);
                itemNum++;
            }

            //방어구 리스트 보여주기
            Console.WriteLine();
            Console.WriteLine("[방어구]");
            foreach (Item i in Inventory.instance.inven_A)
            {
                //이미 해당 아이템이 리스트에 존재하는지
                Item findItem = sellItem.Find(x => x.name.Equals(i.name));
                //존재하지 않다면 리스트에 추가
                if (findItem == null)
                    sellItem.Add(i);

                Console.WriteLine("-{3}.{0} | 방어력 +{1} | {2} | {4} G", i.name, i.defens, i.info, itemNum, i.price);
                itemNum++;
            }
            Console.WriteLine();
            Console.WriteLine("0. 나가기");
            Console.WriteLine();

            if (System_.instance.isInputWrong)
                Console.WriteLine("******잘못된 입력입니다!*******");

            Console.WriteLine("원하시는 행동을 입력해주세요.");
            Console.Write(">>");

            string input = Console.ReadLine();
            //Null입력 체크
            if (home.CheckNullEnter(input))
                PrintShop_Buy();
            else
            {
                bool isHave = CheckSellItem(int.Parse(input));
                if (isHave && int.Parse(input) != 0)
                {
                    //해당아이템 판매
                    SellProduct(sellItem[(int.Parse(input) - 1)]);
                }

                switch (int.Parse(input))
                {
                    //나가기
                    case 0:
                        System_.instance.isInputWrong = false;
                        return 0;

                    default:
                        if (!isHave)
                            System_.instance.isInputWrong = true;
                        PrintShop_Sell();
                        break;
                }
            }
            return 0;
        }

        //판매리스트에 해당아이템이 존재하는지 확인
        public bool CheckSellItem(int _sellNumber)
        {
            if (sellItem.Count >= _sellNumber)
            {
                return true;
            }

            return false;
        }

        //상점에 해당 상품이 있는지 체크
        public bool CheckHaveProduct(int _itemNum)
        {
            int proIndex = _itemNum - 1;
            int itemCount = 0;

            foreach (Item i in instance.product)
            {
                if (proIndex == itemCount)
                {
                    return true;
                }
                itemCount++;
            }

            return false;
        }

        //아이템 구매
        public void BuyProduct(int _itemNum)
        {
            int proIndex = _itemNum - 1;
            int itemCount = 0;

            foreach (Item i in instance.shopProduct)
            {
                //해당 아이템이 존재 한다면
                if (proIndex == itemCount)
                {
                    //플레이어가 돈이 충분한지 확인
                    if (Player.instance.gold >= i.price)
                    {
                        //사려는 아이템이 이미 판매되었는지 확인
                        if (i.isSell)
                        {
                            isAreadySell = true;
                            isBuy = false;
                            enughMoney = true;
                        }
                        //아이템 구매
                        else
                        {
                            Inventory.instance.GetItem(i);
                            Player.instance.gold -= i.price;
                            i.isSell = true;
                            enughMoney = true;
                            isBuy = true;
                            isAreadySell = false;
                        }
                    }

                    //소지금 부족
                    else
                    {
                        //이미 판매된 아이템이라면
                        if (i.isSell)
                        {
                            isAreadySell = true;
                            enughMoney = true;
                            isBuy = false;
                        }
                        else
                        {
                            isAreadySell = false;
                            enughMoney = false;
                            isBuy = false;
                        }
                    }
                }
                itemCount++;
            }
        }

        //아이템 판매
        public void SellProduct(Item _item)
        {
            //무기 아이템일 경우
            if (_item.type == "W")
            {
                for (int i = Inventory.instance.inven_W.Count - 1; i >= 0; i--)
                {
                    //판매하려는 아이템이 무기 인벤토리에 존재
                    if (_item.name == Inventory.instance.inven_W[i].name)
                    {
                        //착용중인 무기 였다면
                        if (_item.name.Contains("[E]"))
                        {
                            string[] itemName = _item.name.Split("[E]");

                            //플레이어의 정보에도 변경사항 업데이트 해줌
                            Player.instance.equipWeapon = new Item();
                            Player.instance.damage -= _item.damage;

                            //상점 표기에도 수정
                            Item findItem = instance.product.Find(x => x.name.Equals(_item.name));
                            if (findItem != null)
                                findItem.name = itemName[1];
                        }

                        //상점 판매리스트 업데이트
                        sellItem.Remove(_item);
                        //해당 아이템 가격의 85%만큼 획득.
                        Player.instance.gold += (int)((float)_item.price * 0.85f);
                        Inventory.instance.curWeaponNum--;
                        //아이템 제거
                        Inventory.instance.inven_W.RemoveAt(i);
                        Inventory.instance.inven.Remove(_item);
                    }
                }
            }

            //방어구 아이템일 경우
            else if (_item.type == "A")
            {
                for (int i = Inventory.instance.inven_A.Count - 1; i >= 0; i--)
                {
                    //판매하려는 아이템이 방어구 인벤토리에 존재
                    if (_item.name == Inventory.instance.inven_A[i].name)
                    {
                        //착용중인 방어구 였다면
                        if (_item.name.Contains("[E]"))
                        {
                            string[] itemName = _item.name.Split("[E]");

                            //플레이어의 정보에도 변경사항 업데이트 해줌
                            Player.instance.equipArmor = new Item();
                            Player.instance.defence -= _item.defens;

                            //상점 표기에도 수정
                            Item findItem = instance.product.Find(x => x.name.Equals(_item.name));
                            if (findItem != null)
                                findItem.name = itemName[1];
                        }

                        //상점 판매리스트 업데이트
                        sellItem.Remove(_item);
                        //해당 아이템 가격의 85%만큼 획득.
                        Player.instance.gold += (int)((float)_item.price * 0.85f);
                        Inventory.instance.curArmorNum--;
                        //아이템 제거
                        Inventory.instance.inven_A.RemoveAt(i);
                        Inventory.instance.inven.Remove(_item);
                    }
                }
            }
        }
    }
}
