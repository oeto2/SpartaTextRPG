using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace SpartaTextRPG
{
    internal class Shop
    {
        public static Shop instance = new Shop();

        Home home = new Home();

        public List<Item> product = new List<Item>();

        //플레이어가 물품을 구매했는지
        public bool isBuy = false;

        //플레이어가 돈이 충분한지
        public bool enughMoney = true;

        //이미 판매된 아이템인지
        public bool isAreadySell = false;

        //상점 물품 업데이트
        public void UpdateProduct()
        {
            for (int i = 3; i < 9; i++)
            {
                product.Add(Inventory.instance.item[i]);
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
            foreach (Item i in product)
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
            foreach (Item i in product)
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
            foreach (Item i in product)
            {
                if (i.type == "A")
                {
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
            foreach (Item i in product)
            {
                if (i.type == "W")
                {
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
                Console.WriteLine("******이미 판매된 아이템입니다!*******");
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

        //상점에 해당 상품이 있는지 체크
        public bool CheckHaveProduct(int _itemNum)
        {
            int proIndex = _itemNum - 1;
            int itemCount = 0;

            foreach (Item i in product)
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

            foreach (Item i in product)
            {
                //해당 아이템이 존재 한다면
                if (proIndex == itemCount)
                {
                    //플레이어가 돈이 충분한지 확인
                    if (Player.instance.gold >= i.price)
                    {
                        //사려는 아이템이 이미 판매되었는지 확인
                        if(i.isSell)
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
                        if(i.isSell)
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
    }

}
