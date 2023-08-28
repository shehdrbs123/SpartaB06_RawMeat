using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BasicTeamProject.Data
{
    public class Skill
    {
        public Skill()
        {
        }
        public Skill(Skill skill)
        {
            this.NameID = skill.NameID;
            this.Mp = skill.Mp;
            this.CoolTime = skill.CoolTime;
            this.ResetCoolTime = skill.CoolTime;
            this.DamegePer = skill.DamegePer;
        }

        public string NameID { get; set; }
        public int Mp { get; set; }
        public int CoolTime { get; set; }
        public int ResetCoolTime { get; set; }
        public TypeOfAbility Buff { get; set; } = TypeOfAbility.End;
        public int duration { get; set; }
        public float DamegePer { get; set; }
    }
}
