using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BasicTeamProject.Data
{
    internal class SkillDataContainer : DataReader
    {
        private Dictionary<string, Skill> _skills;

        public SkillDataContainer()
        {
            _skills = new Dictionary<string, Skill>();
        }


        public override void Process(string[] data)
        {
            Skill skill = new Skill();
            skill.NameID = data[0];
            skill.Mp = int.Parse(data[1]);
            skill.CoolTime = int.Parse(data[2]);
            skill.ResetCoolTime = int.Parse(data[2]);
            if (data[3] != "Att")
                skill.Buff = (TypeOfAbility)Enum.Parse(typeof(TypeOfAbility), data[3]);
            skill.duration = int.Parse(data[4]);
            skill.DamegePer = float.Parse(data[5]) / 100f;

            _skills.Add(data[0], skill);
        }

        public Skill CreateSkill(string name)
        {
            return new Skill(_skills[name]);
        }
    }
}
