using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BasicTeamProject.Data
{
    internal class MonsterDataContainer : DataReader
    {
        private Dictionary<string, Monster> _monsters;

        public MonsterDataContainer()
        {
            _monsters = new Dictionary<string, Monster>();
        }


        public override void Process(string[] data)
        {
            Monster mon = new Monster();
            mon.NameID = data[0];
            mon.Level = int.Parse(data[1]);
            mon.MaxHP = int.Parse(data[2]);
            mon.CurrentHP = mon.MaxHP;
            mon.MaxMP = int.Parse(data[3]);
            mon.CurrentMP = mon.MaxMP;
            mon.Att = int.Parse(data[4]);
            mon.Def = int.Parse(data[5]);
            mon.Critical = float.Parse(data[6]);
            mon.Dodge = float.Parse(data[7]);

            int maxIndex = int.Parse(data[8]);
            for (int i = 0; i < maxIndex; ++i)
            {
                Skill skill = DataManager.Instance.CreateSkill(data[9+ i]);
                mon.Skills.Add(skill);
            }


            _monsters.Add(data[0], mon);
        }

        public Monster CreateMonster(string name)
        {
            return new Monster(_monsters[name]);
        }
    }
}
