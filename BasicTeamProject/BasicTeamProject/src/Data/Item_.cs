using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BasicTeamProject.Data
{
    public enum TypeOfAbility
    {
        MaxHp,
        NowHp,
        MaxMp,
        NowMp,
        Att,
        Def,
        Critical,
        Dodge,
        Exp
    }
    public enum ItemType
    {

    }
    public class Item_
    {
        public Item_()
        {

        }
        public Item_(Item_ item)
        {
            this.NameID = item.NameID;
            this.Level = item.Level;
            this.Duration = item.Duration;
            this.Type = item.Type;
            this.MaxHp = item.MaxHp;
            this.MaxMp = item.MaxMp;
            this.NowHp = item.NowHp;
            this.NowMp = item.NowMp;
            this.Att = item.Att;
            this.Def = item.Def;
            this.Critical = item.Critical;
            this.Dodge = item.Dodge;
            this.Exp = item.Exp;
        }
        public string NameID { get; set; }
        public int Level { get; set; }
        public int Duration { get; set; }
        public ItemType Type { get; set; }
        public int MaxHp { get; set; }
        public int MaxMp { get; set; }
        public int NowHp { get; set; }
        public int NowMp { get; set; }
        public float Att { get; set; }
        public float Def { get; set; }
        public float Critical { get; set; }
        public float Dodge { get; set; }
        public int Exp { get; set; }
    }
}
