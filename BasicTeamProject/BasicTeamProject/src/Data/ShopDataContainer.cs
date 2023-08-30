using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BasicTeamProject.Data
{
    public class ShopDataContainer : DataReader
    {
        //흠... 어떻게만들까...
        //1.레벨별 상점을 만든다
        //2.(재화이용)상점 리셋기능을 이용해서 상점테이블을 6개 만든다. <- 재밌을듯
        //전부 다 합쳐서 100점이 되게끔 데이타를 잘 만져볼까?
        //

        List<Item> _items;
        List<int> _values;
        int maxValue;

        public ShopDataContainer()
        {
            _items = new List<Item>();
            _values = new List<int>();
        }
        public override void Process(string[] data)
        {
            for(int i = 0; i < data.Length; i += 3)
            {
                _items.Add(DataManager.Instance.CreateItem(data[i], int.Parse(data[i + 1])));
                _values.Add(int.Parse(data[i + 2]));
                maxValue += int.Parse(data[i + 2]);
            }
        }

        public void RenewItems(int ListCount)
        {
            DataManager.Instance.Shop.Clear();

            for (int i = 0; i < ListCount; ++i)
            {
                Random random = new Random();
                int rand = random.Next(0, maxValue + 1);

                int index = 0;
                for (; index < _values.Count; index++)
                {
                    rand -= _values[index];
                    if (rand <= 0)
                    {
                        break;
                    }
                }

                DataManager.Instance.Shop.AddItem(new Item(_items[index]));
            }
        }
    }
}
