using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.IO;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace SpartaTextRPG
{
    internal class DataManager
    {
        public static DataManager instance = new DataManager();

        Home home = new Home();

        //json 저장 파일
        string[] strJson = new string[20];

        //저장이 완료됐는지
        bool isSave = false;
        //로드가 완료됐는지
        bool isLoad = false;
        //저장된 데이터가 존재하는지
        bool haveData = true;

        //json 파일이 저장될 경로
        string filePath = @"C:\\Users\\Leesangmin\\Desktop\\Sparta\\2Week\\SpartaTextRPG\";

        public int PrintCheckSave()
        {
            Console.Clear();
            Console.WriteLine("저장하기");
            Console.WriteLine();
            Console.WriteLine("정말로 저장하시겠습니까?");
            Console.WriteLine();
            Console.WriteLine("1. 예");
            Console.WriteLine("0. 아니오");
            Console.WriteLine();
            if (isSave)
                Console.WriteLine("******저장 완료!*******");
            if (System_.instance.isInputWrong)
                Console.WriteLine("******잘못된 입력입니다!*******");
            Console.WriteLine("원하시는 행동을 입력해주세요.");
            Console.Write(">>");


            string input = Console.ReadLine();

            //Null입력 체크
            if (home.CheckNullEnter(input))
                return 6;
            else
            {
                switch (int.Parse(input))
                {
                    //나가기
                    case 0:
                        System_.instance.isInputWrong = false;
                        isSave = false;
                        return 0;

                    //저장하기
                    case 1:
                        System_.instance.isInputWrong = false;
                        Save();
                        isSave = true;
                        return 6;

                    default:
                        System_.instance.isInputWrong = true;
                        break;
                }
            }
            return 6;
        }

        public int PrintCheckLoad()
        {
            Console.Clear();
            Console.WriteLine("불러오기");
            Console.WriteLine();
            Console.WriteLine("저장된 데이터를 불러오시겠습니까?");
            Console.WriteLine();
            Console.WriteLine("1. 예");
            Console.WriteLine("0. 아니오");
            Console.WriteLine();
            if(!haveData)
                Console.WriteLine("******저장된 데이터가 존재하지 않습니다!*******");
            if (isLoad)
                Console.WriteLine("******데이터를 불러왔습니다!*******");
            if (System_.instance.isInputWrong)
                Console.WriteLine("******잘못된 입력입니다!*******");
            Console.WriteLine("원하시는 행동을 입력해주세요.");
            Console.Write(">>");


            string input = Console.ReadLine();

            //Null입력 체크
            if (home.CheckNullEnter(input))
                return 7;
            else
            {
                switch (int.Parse(input))
                {
                    //나가기
                    case 0:
                        System_.instance.isInputWrong = false;
                        isLoad = false;
                        return 0;

                    //불러오기
                    case 1:
                        System_.instance.isInputWrong = false;
                        Load();
                        return 7;

                    default:
                        System_.instance.isInputWrong = true;
                        break;
                }
            }
            return 7;
        }

        //데이터 저장
        public void Save()
        {
            //저장할 데이터
            strJson[0] = JsonConvert.SerializeObject(Player.instance.equipWeapon);
            strJson[1] = JsonConvert.SerializeObject(Player.instance.equipArmor);
            strJson[2]= JsonConvert.SerializeObject(Inventory.instance.inven);
            strJson[3] = JsonConvert.SerializeObject(Inventory.instance.inven_W);
            strJson[4] = JsonConvert.SerializeObject(Inventory.instance.inven_A);
            strJson[5] = JsonConvert.SerializeObject(Inventory.instance.curArmorNum);
            strJson[6] = JsonConvert.SerializeObject(Inventory.instance.curWeaponNum);
            strJson[7] = JsonConvert.SerializeObject(Inventory.instance.itemList);
            strJson[8] = JsonConvert.SerializeObject(Shop.instance.product);
            strJson[9] = JsonConvert.SerializeObject(Shop.instance.shopProduct);
            strJson[10] = JsonConvert.SerializeObject(Shop.instance.sellItem);
            strJson[11] = JsonConvert.SerializeObject(Player.instance);

            //파일저장
            for (int i =0; i<12; i++)
            {
                File.WriteAllText(filePath + "str" +i.ToString() + ".json", strJson[i].ToString());
            }
        }

        public void Load()
        {
            haveData = true;

            //데이터 파일이 존재하는지 체크
            for (int i = 0; i < 12; i++)
            {
                if (!File.Exists(filePath + "str" + i.ToString() + ".json"))
                {
                    haveData = false;
                }
            }

            if (haveData)
            {
                //파일을 읽어서 strJson에 넣기
                for (int i = 0; i < 12; i++)
                {
                    strJson[i] = File.ReadAllText(filePath + "str" + i.ToString() + ".json");
                }

                Player.instance.equipWeapon = JsonConvert.DeserializeObject<Item>(strJson[0]);
                Player.instance.equipArmor = JsonConvert.DeserializeObject<Item>(strJson[1]);
                Inventory.instance.inven = JsonConvert.DeserializeObject<List<Item>>(strJson[2]);
                Inventory.instance.inven_W = JsonConvert.DeserializeObject<List<Item>>(strJson[3]);
                Inventory.instance.inven_A = JsonConvert.DeserializeObject<List<Item>>(strJson[4]);
                Inventory.instance.curArmorNum = JsonConvert.DeserializeObject<int>(strJson[5]);
                Inventory.instance.curArmorNum = JsonConvert.DeserializeObject<int>(strJson[6]);
                Inventory.instance.itemList = JsonConvert.DeserializeObject<List<Item>>(strJson[7]);
                Shop.instance.product = JsonConvert.DeserializeObject<List<Item>>(strJson[8]);
                Shop.instance.shopProduct = JsonConvert.DeserializeObject<List<Item>>(strJson[9]);
                Shop.instance.sellItem = JsonConvert.DeserializeObject<List<Item>>(strJson[10]);
                Player.instance = JsonConvert.DeserializeObject<Player>(strJson[11]);

                isLoad = true;
            }
        }
    }
}
