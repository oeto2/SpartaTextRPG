using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace SpartaTextRPG
{
    internal class Shop
    {
        public static Shop instance = new Shop();

        public List<Item> product = new List<Item>();

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
                    Console.WriteLine("- {0} | 방어력 +{1} | {2} | {3} G", i.name, i.defens, i.info, i.price);
                }
            }
            Console.WriteLine();
            Console.WriteLine("[무기 목록]");
            //상점 무기 목록 보여주기
            foreach (Item i in product)
            {
                if (i.type == "W")
                {
                    Console.WriteLine("- {0} | 공격력 +{1} | {2} | {3} G", i.name, i.damage, i.info, i.price);
                }
            }

            if (System_.instance.isInputWrong)
            {
                Console.WriteLine("******잘못된 입력입니다!*******");
            }
            Console.WriteLine();
            Console.WriteLine("0. 나가기");
            Console.WriteLine("1. 아이템 구매");
            Console.WriteLine();
            Console.WriteLine("원하시는 행동을 입력해주세요.");
            Console.Write(">>");


            string input = Console.ReadLine();

            return 0;
        }
    }

}
