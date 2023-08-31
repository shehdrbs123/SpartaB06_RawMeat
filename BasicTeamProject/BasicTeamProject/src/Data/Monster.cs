using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BasicTeamProject.Data
{
    public class Monster : ISkillStatus
    {
        public Monster() 
        {
        }
        public Monster(Monster mon)
        {
            this.NameID = mon.NameID;
            this.Level = mon.Level;
            this.MaxHP = mon.MaxHP;
            this.CurrentHP = mon.CurrentHP;
            this.MaxMP = mon.MaxMP;
            this.CurrentMP = mon.CurrentMP;
            this.Att = mon.Att;
            this.Def = mon.Def;
            this.Critical = mon.Critical;
            this.Dodge = mon.Dodge;
            for (int i = 0; i < mon.Skills.Count; i++)
            {
                Skill skill = new Skill(mon.Skills[i]);
                Skills.Add(skill);
            }
        }
        public string NameID { get; set; }
        public int Level { get; set; }
        public int MaxHP { get; set; }
        public int CurrentHP { get; set; }
        public int MaxMP { get; set; }
        public int CurrentMP { get; set; }
        public float Att { get; set; }
        public float Def { get; set; }
        public float Critical { get; set; }
        public float Dodge { get; set; }
        public List<Skill> Skills { get; set; } = new List<Skill>();
        public bool isDead { get; set; } = false;


        public bool MonsterAct(out int damege, out bool wide)
        {
            wide = false;
            for (int i = 0; i < Skills.Count; ++i)
            {
                if (Skills[i].GetSkillAble(this))
                {
                    Console.WriteLine($"{NameID}의 {Skills[i].NameID}발동!");
                    Thread.Sleep(600);
                    damege = Skills[i].UseSkill(this, out wide);
                    if (damege > 0)
                    {
                        return true;
                    }
                    else if (damege == -3)
                    {
                        return false;
                    }
                }
            }
            Console.WriteLine($"퍽!!");
            Thread.Sleep(600);
            damege = (int)Att;
            return true;
        }

        public void TurnCheck()
        {
            foreach (Skill skill in Skills)
            {
                if (skill.TurnCheck(this))
                {
                    //이때 버프가 끝난것 따로 뭔가..넣나..
                    Console.WriteLine($"{NameID}의 {skill.NameID} 지속시간이 끝났다!");
                }
            }
        }
    }
}
