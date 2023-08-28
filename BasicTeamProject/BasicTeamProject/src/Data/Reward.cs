using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BasicTeamProject.Data
{
    public class Reward
    {
        public int MinGold { get; set; }
        public int MaxGold { get; set; }

        public List<Item_> Items { get; set; } = new List<Item_>();
        public List<float> DropPer { get; set; } = new List<float>();

        public List<Item_> GetReward(out int gold)
        {
            Random random = new Random();
            gold = random.Next(MinGold, MaxGold + 1);
            
            List<Item_> items = new List<Item_>();
            for(int i = 0; i < Items.Count; ++i)
            {
                if (DropPer[i] > random.NextSingle() * 100f)
                {
                    items.Add(new Item_(Items[i]));
                }
            }
            return items;
        }
    }
}
