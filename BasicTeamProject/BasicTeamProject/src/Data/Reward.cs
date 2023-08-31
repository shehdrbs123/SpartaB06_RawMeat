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

        public int Exp { get; set; }
        public List<Item> Items { get; set; } = new List<Item>();
        public List<float> DropPer { get; set; } = new List<float>();

        public List<Item> GetRewards(out int gold, out int exp)
        {
            Random random = new Random();
            gold = random.Next(MinGold, MaxGold + 1);
            exp = Exp;
            List<Item> items = new List<Item>();
            for(int i = 0; i < Items.Count; ++i)
            {
                if (DropPer[i] > random.NextSingle() * 100f)
                {
                    items.Add(new Item(Items[i]));
                }
            }
            return items;
        }
    }
}
