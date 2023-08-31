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
            skill.HP = int.Parse(data[1]);
            skill.MP = int.Parse(data[2]);
            skill.CoolTime = 0;
            skill.ResetCoolTime = int.Parse(data[3]);
            skill.Type = (TypeOfAbility)Enum.Parse(typeof(TypeOfAbility), data[4]);
            skill.isBuff = Boolean.Parse(data[5]);
            skill.Duration = 0;
            skill.ResetDuration = int.Parse(data[6]);
            skill.isWide = Boolean.Parse(data[7]);
            skill.isPer = Boolean.Parse(data[8]);

            if (skill.isPer == true)
                skill.Value = float.Parse(data[9]) / 100f;
            else
                skill.Value = float.Parse(data[9]);

            _skills.Add(data[0], skill);
        }

        public Skill CreateSkill(string name)
        {
            return new Skill(_skills[name]);
        }
    }
}
