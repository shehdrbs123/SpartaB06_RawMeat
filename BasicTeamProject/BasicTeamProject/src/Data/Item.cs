using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace BasicTeamProject.Data
{

    public class Item
    {   
        public enum ShowType
        {
            Sell,
            Goods,
            End
        }
        public Item()
        {

        }
        public Item(Item item)
        {
            this.NameID = item.NameID;
            this.Level = item.Level;
            this.Duration = item.Duration;
            this.Type = item.Type;
            this.EquipType = item.EquipType;
            this.MaxHp = item.MaxHp;
            this.MaxMp = item.MaxMp;
            this.CurrentHp = item.CurrentHp;
            this.CurrentMp = item.CurrentMp;
            this.Att = item.Att;
            this.Def = item.Def;
            this.Critical = item.Critical;
            this.Dodge = item.Dodge;
            this.Exp = item.Exp;
            this.Count = item.Count;
            this.MaxCount = item.MaxCount;
            this.Gold = item.Gold;
            this.IsEquipped = item.IsEquipped;
        }
        public Item(Item item, int Count)
        {
            this.NameID = item.NameID;
            this.Level = item.Level;
            this.Duration = item.Duration;
            this.Type = item.Type;
            this.EquipType = item.EquipType;
            this.MaxHp = item.MaxHp;
            this.MaxMp = item.MaxMp;
            this.CurrentHp = item.CurrentHp;
            this.CurrentMp = item.CurrentMp;
            this.Att = item.Att;
            this.Def = item.Def;
            this.Critical = item.Critical;
            this.Dodge = item.Dodge;
            this.Exp = item.Exp;
            this.Count = Count;
            this.MaxCount = item.MaxCount;
            this.Gold = item.Gold;
            this.IsEquipped = item.IsEquipped;
        }
        public string NameID { get; set; }
        public int Level { get; set; }
        public int Count { get; set; }
        public int MaxCount { get; set; }
        public int Duration { get; set; }
        public ItemType Type { get; set; }
        public EquipType EquipType { get; set; }
        public int MaxHp { get; set; }
        public int MaxMp { get; set; }
        public int CurrentHp { get; set; }
        public int CurrentMp { get; set; }
        public float Att { get; set; }
        public float Def { get; set; }
        public float Critical { get; set; }
        public float Dodge { get; set; }
        public int Exp { get; set; }
        public int Gold { get; set; }
        public bool IsEquipped { get; set; } = false;

        public void ShowInfo(int num, ShowType type = ShowType.End)
        {
            List<string> list = new List<string>();

            if(num != 0)
                list.Add(num + ".");
            else
                list.Add("");

            if (IsEquipped)
                list[0] += "[E]";

            list[0] += NameID;
            
            if (MaxHp != 0)
                list.Add("| 최대체력 : " + MaxHp);
            if (MaxMp != 0)
                list.Add("| 최대마나 : " + MaxMp );
            if (Att != 0)
                list.Add("| 공격력 : " + Att);
            if (Def != 0)
                list.Add("| 방어력 : " + Def);
            if (Critical != 0)
                list.Add("| 크리티컬 : " + Critical);
            if (Dodge != 0)
                list.Add("| 회피 : " + Dodge);
            if (CurrentHp != 0 && MaxHp == 0)
                list.Add("| 체력 " + CurrentHp + "증가");
            if (CurrentMp != 0 && MaxMp == 0)
                list.Add("| 마나 " + CurrentMp + "증가");
            if (Exp != 0)
                list.Add("| 경험치 " + Exp + "증가");


            if (list.Count == 1)
                list.Add("| 효과 없음");

            if (Count > 1)
            {
                list.Add("| 갯수 : " + Count);
            }
            if (type == ShowType.Goods)
            {
                if (Count > 1)
                    list.Add("| 개당가격 : " + Gold);
                else
                    list.Add("| 가격 : " + Gold);
            }
            if (type == ShowType.Sell)
            {
                if (Count > 1)
                    list.Add("| 개당가격 : " + Gold * 100 / 70);
                else
                    list.Add("| 가격 : " + Gold * 100 / 70);
            }

            for (int i = 0; i < list.Count; ++i)
            {
                int countOfKorean = 0;
                for (int j = 0; j < list[i].Length; j++)
                {
                    byte oF = (byte)((list[i][j] & 0xFF00) >> 7);
                    if (oF != 0)
                        ++countOfKorean;
                }
                list[i] = list[i].PadRight(16 - countOfKorean);
                Console.Write(list[i]);
            }
            Console.WriteLine();
        }
    }
}
