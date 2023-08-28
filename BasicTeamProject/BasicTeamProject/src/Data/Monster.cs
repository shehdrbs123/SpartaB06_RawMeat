using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BasicTeamProject.Data
{
    internal class Monster
    {
        public Monster() 
        {
        }
        public Monster(Monster mon)
        {
            this.NameID = mon.NameID;
            this.Level = mon.Level;
            this.MaxHp = mon.MaxHp;
            this.MaxMp = mon.MaxMp;
            this.Att = mon.Att;
            this.Def = mon.Def;
            this.Critical = mon.Critical;
            this.Dodge = mon.Dodge;
            this.Exp = mon.Exp;
            for (int i = 0; i < mon.Skills.Count; i++)
            {
                Skill skill = new Skill(mon.Skills[i]);
                Skills.Add(skill);
            }
        }
        public string NameID { get; set; }
        public int Level { get; set; }
        public int MaxHp { get; set; }
        public int MaxMp { get; set; }
        public float Att { get; set; }
        public float Def { get; set; }
        public float Critical { get; set; }
        public float Dodge { get; set; }
        public int Exp { get; set; }
        public List<Skill> Skills { get; set; } = new List<Skill>();

    }
}
