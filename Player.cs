using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace SpartaTextRPG
{
    //플레이어의 정보 클래스
    public class Player
    {
        Home home = new Home();

        public int level = 1;
        public string playerClass = "모험가";
        public string playerName = "르탄";
        public int damage = 10;
        public int defence = 5;
        public int hp = 100;
        public int gold = 10000;
        public string weapon = "미착용";
        public string armor = "미착용";
        public int equipWeponNum = 0;
        public int equipArmorNum = 0;

        //착용중인 무기 정보
        public Item equipWeapon = new Item();
        //착용중인 방어구 정보
        public Item equipArmor = new Item();

        //싱글톤
        public static Player instance = new Player();

        public int PrintPlayerInfo()
        {
            //플레이어 레벨이 10보다 낮은지
            bool isLevelLow = false;
            if (instance.level < 10)
                isLevelLow = true;

            string[] weaponName = new string[2];

            string[] armorName = new string[2];


            Console.Clear();

            Console.WriteLine("상태보기");
            Console.WriteLine();
            Console.WriteLine("캐릭터의 정보가 표시됩니다.");
            Console.WriteLine();
            if (isLevelLow)
                Console.WriteLine("Lv. {0}{1}", "0", instance.level);
            else
                Console.WriteLine("Lv. {0}", instance.level);
            Console.WriteLine("{0} ({1})", playerName, instance.playerClass);

            if (instance.equipWeapon.damage != 0)
                Console.WriteLine("공격력: {0} (+{1})", instance.damage, instance.equipWeapon.damage);
            else
                Console.WriteLine("공격력: {0}", instance.damage);

            if (instance.equipArmor.defens != 0)
                Console.WriteLine("방어력: {0} (+{1})", instance.defence, instance.equipArmor.defens);
            else
                Console.WriteLine("방어력: {0}", instance.defence);

            Console.WriteLine("체력: {0}", instance.hp);
            Console.WriteLine("Gold: {0}", instance.gold);
            Console.WriteLine();

            if (instance.equipWeapon.damage != 0)
            {
                weaponName = instance.equipWeapon.name.Split("[E]");
                Console.WriteLine("착용중인 무기 : {0}", weaponName[1]);
            }
            else
                Console.WriteLine("착용중인 무기 : {0}", weapon);

            if (instance.equipArmor.defens != 0)
            {
                armorName = instance.equipArmor.name.Split("[E]");
                Console.WriteLine("착용중인 방어구 : {0}", armorName[1]);
            }
            else
                Console.WriteLine("착용중인 방어구 : {0}", armor);

            Console.WriteLine();
            Console.WriteLine("0. 나가기");
            Console.WriteLine();
            if (System_.instance.isInputWrong)
            {
                Console.WriteLine("******잘못된 입력입니다!*******");
            }
            Console.WriteLine("원하시는 행동을 입력해주세요");
            Console.Write(">>");

            string input = Console.ReadLine();

            if (home.CheckNullEnter(input))
                return 1;
            else
            {
                switch (int.Parse(input))
                {
                    case 0:
                        return int.Parse(input);
                    default:
                        System_.instance.isInputWrong = true;
                        break;
                }
            }

            return 1;
        }
    }
}
