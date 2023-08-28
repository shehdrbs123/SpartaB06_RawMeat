using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BasicTeamProject.Data
{
    internal class ItemDataContainer : DataReader
    {
        private Dictionary<string, Item_> _items;

        public ItemDataContainer()
        {
            _items = new Dictionary<string, Item_>();
        }
        public override void Process(string[] data)
        {
            Item_ item = new Item_();
            item.NameID = data[0];
            item.Level = int.Parse(data[1]);
            item.Duration = int.Parse(data[2]);
            item.Type = (ItemType)Enum.Parse(typeof(ItemType), data[3]);
            
            int maxIndex = int.Parse(data[4]);
            for(int i = 0; i < maxIndex; i++)
            {
                switch ((TypeOfAbility)Enum.Parse(typeof(TypeOfAbility), data[5 + i * 2]))
                {
                    case TypeOfAbility.MaxHp: 
                        item.MaxHp += int.Parse(data[5 + i * 2 + 1]);
                        item.NowHp += int.Parse(data[5 + i * 2 + 1]);
                        break;
                    case TypeOfAbility.NowHp:
                        item.NowHp += int.Parse(data[5 + i * 2 + 1]);
                        break;
                    case TypeOfAbility.MaxMp:
                        item.MaxMp += int.Parse(data[5 + i * 2 + 1]);
                        item.NowMp += int.Parse(data[5 + i * 2 + 1]);
                        break;
                    case TypeOfAbility.NowMp:
                        item.NowMp += int.Parse(data[5 + i * 2 + 1]);
                        break;
                    case TypeOfAbility.Att:
                        item.Att += int.Parse(data[5 + i * 2 + 1]);
                        break;
                    case TypeOfAbility.Def:
                        item.Def += int.Parse(data[5 + i * 2 + 1]);
                        break;
                    case TypeOfAbility.Critical:
                        item.Critical += int.Parse(data[5 + i * 2 + 1]);
                        break;
                    case TypeOfAbility.Dodge:
                        item.Dodge += int.Parse(data[5 + i * 2 + 1]);
                        break;
                    case TypeOfAbility.Exp:
                        item.Exp += int.Parse(data[5 + i * 2 + 1]);
                        break;
                }
            }

            _items.Add(data[0], item);
        }

        public Item_ CreateItem(string name)
        {
            return new Item_(_items[name]);
        }
    }
}
