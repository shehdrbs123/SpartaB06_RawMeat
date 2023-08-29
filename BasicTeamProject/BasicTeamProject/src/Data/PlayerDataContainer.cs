using BasicTeamProject.Scene;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BasicTeamProject.Data
{
    public class PlayerDataContainer : DataReader
    {
        private Dictionary<string, PlayerPasingData> _playerDatas;
        private List<PlayerPasingData> _playerDataList;

        public PlayerDataContainer()
        {
            _playerDataList = new List<PlayerPasingData>();
            _playerDatas = new Dictionary<string, PlayerPasingData>();
        }

        public override void Process(string[] data)
        {
            PlayerPasingData ppd = new PlayerPasingData();
            ppd.job = (Player.Job)Enum.Parse(typeof(Player.Job), data[0]);
            ppd.MaxHp = int.Parse(data[1]);
            ppd.MaxMp = int.Parse(data[2]);
            ppd.Att = int.Parse(data[3]);
            ppd.Def = int.Parse(data[4]);
            ppd.Critical = int.Parse(data[5]);
            ppd.Dodge = int.Parse(data[6]);

            int maxIndex = int.Parse(data[7]);
            ppd.Skills = new List<Skill>();
            for (int i = 0; i < maxIndex; ++i)
            {
                Skill skill = DataManager.Instance.CreateSkill(data[8 + i]);
                ppd.Skills.Add(skill);
            }


            _playerDatas.Add(data[0], ppd);
            _playerDataList.Add(ppd);
        }

        public void PlayerSetting(string key)
        {
            DataManager.Instance.Player.Setting(_playerDatas[key]);
        }
        public void PlayerSetting(int key)
        {
            DataManager.Instance.Player.Setting(_playerDataList[key - 1]);
        }
    }
}
