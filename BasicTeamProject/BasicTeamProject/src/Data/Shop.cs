﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BasicTeamProject.Data
{
    public class Shop
    {

        List<Item?> _items;

        public Shop()
        {
            _items = new List<Item?>();
        }

        public void ShowShopItem()
        {
            int i = 1;
            foreach(Item item in _items)
            {
                if (item is not null)
                    item.ShowInfo(i, Item.ShowType.Goods);
                else
                    Console.WriteLine("판매 완료");
                ++i;
            }
        }
        public int GetListCount()
        {
            return _items.Count;
        }

        public int GetItemCount(int index)
        {
            index -= 1;

            if (_items[index] is not null)
                return _items[index].Count;

            return 0;
        }
        public Item? SellItem(int index, int count = 1)
        {
            index -= 1;

            if (_items[index] is null || count == 0)
                return null;

            if (_items[index].Count < count)
                count = _items[index].Count;

            Item item = new Item(_items[index], count);

            _items[index].Count -= count;
            if(_items[index].Count == 0)
            {
                _items[index] = null;
            }

            return item;
        }

        public Item? GetlItem(int index)
        {
            index -= 1;

            if (_items[index] is null)
                return null;

            return _items[index];
        }
        public bool RenewItems()
        {
            if (DataManager.Instance.Player.Gold < 500)
                return false;

            DataManager.Instance.RenewItems(6);

            return true;
        }
        public void Clear()
        {
            _items.Clear();
        }

        public void AddItem(Item? item)
        {
            _items.Add(item);
        }

        public string GetSaveData()
        {
            string str = "";
            foreach (Item item in _items)
            {
                if (item is not null)
                    str += item.NameID + "|" + item.Count.ToString() + "|" + item.IsEquipped.ToString() + "|";
                else
                    str += "NULL|";
            }
            str = str.Remove(str.Length - 1);
            return str;
        }
        public void SetData(string[] data)
        {
            _items.Clear();
            for (int i = 0; i < data.Length;)
            {
                if (data[i] == "NULL")
                {
                    AddItem(null);
                    ++i;
                }
                else
                {
                    string name = data[i];
                    int count = int.Parse(data[i + 1]);
                    bool equip = Boolean.Parse(data[i + 2]);
                    AddItem(DataManager.Instance.CreateItem(name, count));
                    i += 3;
                }
            }
        }
    }
}
