using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace BasicTeamProject.Data
{
    public class ClassUpDataContainer : DataReader
    {
        Dictionary<Player.Job, int> _levelData;
        Dictionary<Player.Job, List<Skill>> _skillData;
        Dictionary<Player.Job, Player.Job> _jobData;

        public ClassUpDataContainer()
        {
            _levelData = new Dictionary<Player.Job, int>();
            _skillData = new Dictionary<Player.Job, List<Skill>>();
            _jobData = new Dictionary<Player.Job, Player.Job>();
        }
        public override void Process(string[] data)
        {
            Player.Job job = (Player.Job)Enum.Parse(typeof(Player.Job), data[0]);
            _jobData.Add(job, (Player.Job)Enum.Parse(typeof(Player.Job), data[1]));
            _levelData.Add(job, int.Parse(data[2]));
            int skills = int.Parse(data[3]);
            _skillData.Add(job, new List<Skill>());
            for(int i = 0; i < skills; i++)
            {
                _skillData[job].Add(DataManager.Instance.CreateSkill(data[4 + i]));
            }
        }

        public int GetClassUpLevel(Player player)
        {
            if (_levelData.ContainsKey(player.job))
            {
                return _levelData[player.job];
            }
            return 0;
        }
        public bool ClassUpCheck(Player player)
        {
            if (_levelData.ContainsKey(player.job))
            {
                if (player.Level >= _levelData[player.job])
                {
                    return true;
                }
            }
            return false;
        }
        public void ClassUp(Player player)
        {
            int i = 0;
            if (ClassUpCheck(player))
            {
                Console.WriteLine("이상한 기운이 감돈다!!!");
                Console.ReadLine();
                Console.WriteLine("전직할것같애!");
                Console.ReadLine();
                Console.WriteLine("으악!");
                Console.ReadLine();
                Console.WriteLine($"{player.job.ToString()} -> {_jobData[player.job].ToString()} 으로 전직했다!");
                Console.ReadLine();
                player.Skills.Clear();
                foreach (var skill in _skillData[player.job])
                {
                    player.Skills.Add(new Skill(skill));
                }
                player.job = _jobData[player.job];
            }

        }
    }
}
