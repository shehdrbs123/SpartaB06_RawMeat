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
            this.MaxHP = item.MaxHP;
            this.MaxMP = item.MaxMP;
            this.CurrentHP = item.CurrentHP;
            this.CurrentMP = item.CurrentMP;
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
            this.MaxHP = item.MaxHP;
            this.MaxMP = item.MaxMP;
            this.CurrentHP = item.CurrentHP;
            this.CurrentMP = item.CurrentMP;
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
        public int MaxHP { get; set; }
        public int MaxMP { get; set; }
        public int CurrentHP { get; set; }
        public int CurrentMP { get; set; }
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

            if (Type == ItemType.Equip)
                list[0] += "(" + Type.ToString() + ")";
            if (Type == ItemType.Consumable)
                list[0] += "(소모품)";

            int countOfKorean = 0;
            for (int j = 0; j < list[0].Length; j++)
            {
                byte oF = (byte)((list[0][j] & 0xFF00) >> 7);
                if (oF != 0)
                    ++countOfKorean;
            }
            list[0] = list[0].PadRight(25 - countOfKorean);
            Console.Write(list[0]);

            if (MaxHP != 0)
                list.Add("| 최대체력 : " + MaxHP);
            if (MaxMP != 0)
                list.Add("| 최대마나 : " + MaxMP );
            if (Att != 0)
                list.Add("| 공격력 : " + Att);
            if (Def != 0)
                list.Add("| 방어력 : " + Def);
            if (Critical != 0)
                list.Add("| 크리티컬 : " + Critical);
            if (Dodge != 0)
                list.Add("| 회피 : " + Dodge);
            if (CurrentHP != 0 && MaxHP == 0)
                list.Add("| 체력 " + CurrentHP + "증가");
            if (CurrentMP != 0 && MaxMP == 0)
                list.Add("| 마나 " + CurrentMP + "증가");
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
                    list.Add("| 개당가격 : " + Gold * 70 / 100);
                else
                    list.Add("| 가격 : " + Gold * 70 / 100);
            }

            for (int i = 1; i < list.Count; ++i)
            {
                countOfKorean = 0;
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
