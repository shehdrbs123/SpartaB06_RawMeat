using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BasicTeamProject.Data
{

    public class Item
    {
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
        }
        public Item(Item item, int Count)
        {
            this.NameID = item.NameID;
            this.Level = item.Level;
            this.Duration = item.Duration;
            this.Type = item.Type;
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
        public void ShowInfo()
        {
            Console.WriteLine($"{NameID} | {Count}");
        }
    }
}
