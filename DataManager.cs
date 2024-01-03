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
        
        //json 저장 파일
        string[] strJson = new string[20];

        string filePath = @"C:\\Users\\Leesangmin\\Desktop\\Sparta\\2Week\\SpartaTextRPG\";

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
            //파일 로드하기
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
        }
    }
}
