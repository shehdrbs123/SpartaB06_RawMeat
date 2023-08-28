using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BasicTeamProject.Data
{
  
    internal class RewardDataContainer : DataReader
    {
        private Dictionary<string, Reward> _rewards;

        public RewardDataContainer()
        {
            _rewards = new Dictionary<string, Reward>();
        }
        public override void Process(string[] data)
        {
            Reward reward = new Reward();
            reward.MinGold = int.Parse(data[1]);
            reward.MaxGold = int.Parse(data[2]);
            int maxIndex = int.Parse(data[3]);
            for (int i = 0; i < maxIndex; ++i)
            {
                Item_ item = DataManager.Instance.CreateItem(data[4 + i * 2]);
                reward.Items.Add(item);
                reward.DropPer.Add(float.Parse(data[4 + i * 2 + 1]));
            }


            _rewards.Add(data[0], reward);
        }

        public List<Item_> GetReward(string name, out int gold)
        {
            return _rewards[name].GetReward(out gold);
        }
    }
}
