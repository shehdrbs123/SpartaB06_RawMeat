using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BasicTeamProject.Data
{
    internal class ItemDataContainer : DataReader
    {
        private Dictionary<string, Item> _items;

        public ItemDataContainer()
        {
            _items = new Dictionary<string, Item>();
        }
        public override void Process(string[] data)
        {
            Item item = new Item();
            item.NameID = data[0];
            item.Level = int.Parse(data[1]);
            item.MaxCount = int.Parse(data[2]); 
            item.Duration = int.Parse(data[3]);
            item.Type = (ItemType)Enum.Parse(typeof(ItemType), data[4]);
            item.EquipType = (EquipType)Enum.Parse(typeof(EquipType), data[5]);
            item.Gold = int.Parse(data[6]);
            int maxIndex = int.Parse(data[7]);
            for(int i = 0; i < maxIndex; i++)
            {
                switch ((TypeOfAbility)Enum.Parse(typeof(TypeOfAbility), data[8 + i * 2]))
                {
                    case TypeOfAbility.MaxHp: 
                        item.MaxHp +=       int.Parse(data[8 + i * 2 + 1]);
                        item.CurrentHp +=   int.Parse(data[8 + i * 2 + 1]);
                        break;
                    case TypeOfAbility.CurrentHp:
                        item.CurrentHp +=   int.Parse(data[8 + i * 2 + 1]);
                        break;
                    case TypeOfAbility.MaxMp:
                        item.MaxMp +=       int.Parse(data[8 + i * 2 + 1]);
                        item.CurrentMp +=   int.Parse(data[8 + i * 2 + 1]);
                        break;
                    case TypeOfAbility.CurrentMp:
                        item.CurrentMp +=   int.Parse(data[8 + i * 2 + 1]);
                        break;
                    case TypeOfAbility.Att:
                        item.Att +=         int.Parse(data[8 + i * 2 + 1]);
                        break;
                    case TypeOfAbility.Def:
                        item.Def +=         int.Parse(data[8 + i * 2 + 1]);
                        break;
                    case TypeOfAbility.Critical:
                        item.Critical +=    int.Parse(data[8 + i * 2 + 1]);
                        break;
                    case TypeOfAbility.Dodge:
                        item.Dodge +=       int.Parse(data[8 + i * 2 + 1]);
                        break;
                    case TypeOfAbility.Exp:
                        item.Exp +=         int.Parse(data[8 + i * 2 + 1]);
                        break;
                }
            }

            _items.Add(data[0], item);
        }

        public Item CreateItem(string name)
        {
            return new Item(_items[name]);
        }
        public Item CreateItem(string name, int count)
        {
            return new Item(_items[name], count);
        }
    }
}
