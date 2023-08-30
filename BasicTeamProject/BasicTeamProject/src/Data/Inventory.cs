using System;
using System.Reflection;
using static BasicTeamProject.Data.Item;

namespace BasicTeamProject.Data;

public class Inventory
{
    public Inventory()
    {
        _inven = new Dictionary<ItemType, Dictionary<string, List<Item>>>();

        for(ItemType i = 0; i < ItemType.End; ++i)
        {
            _inven[i] = new Dictionary<string, List<Item>>();
        }
    }

    public int GetItemCount(ItemType type, int index)
    {
        string select = "";
        if (type == ItemType.End)
        {
            ItemType i = 0;
            foreach (var Dic in _inven)
            {
                foreach (var items in Dic.Value)
                {
                    if (index - items.Value.Count > 0)
                        index -= items.Value.Count;
                    else
                    {
                        type = i;
                        select = items.Key;
                        index -= 1;
                        break;
                    }
                }
                if (select != "")
                    break;
                ++i;
            }
        }
        else
        {
            foreach (var items in _inven[type])
            {
                if (index - items.Value.Count > 0)
                    index -= items.Value.Count;
                else
                {
                    select = items.Key;
                    index -= 1;
                    break;
                }
            }
        }
        return _inven[type][select][index].Count;
    }
    public void AddItem(Item item)
    {
        while (true)
        {
            List<Item> a;
            if (_inven[item.Type].TryGetValue(item.NameID, out a))
            {
                //넣는갯수에 따라 빈공간에 아이템을 넣을것.
                int Count = item.Count;
                for (int i = 0; i < a.Count && Count != 0; ++i)
                {
                    if (a[i].Count + Count > item.MaxCount)
                    {
                        Count -= item.MaxCount - a[i].Count;
                        a[i].Count = item.MaxCount;
                    }
                    else
                    {
                        a[i].Count += Count;
                        Count = 0;
                    }
                }

                //넣는갯수가 많으면 아이템을 추가로 만들것
                while (Count > 0)
                {
                    a.Add(new Item(item, Math.Min(Count, item.MaxCount)));
                    Count -= Math.Min(item.MaxCount, Count);
                }

                break;
            }
            else
            {
                _inven[item.Type].Add(item.NameID, new List<Item>());
            }
        }
    }
    public void AddItem(string name, int count)
    {
        Item item = DataManager.Instance.CreateItem(name, count);
        while (true)
        {
            List<Item> a;
            if (_inven[item.Type].TryGetValue(item.NameID, out a))
            {
                //넣는갯수에 따라 빈공간에 아이템을 넣을것.
                int Count = item.Count;
                for (int i = 0; i < a.Count && Count != 0; ++i)
                {
                    if (a[i].Count + Count > item.MaxCount)
                    {
                        Count -= item.MaxCount - a[i].Count;
                        a[i].Count = item.MaxCount;
                    }
                    else
                    {
                        a[i].Count += Count;
                        Count = 0;
                    }
                }

                //넣는갯수가 많으면 아이템을 추가로 만들것
                while (Count > 0)
                {
                    a.Add(new Item(item, Math.Min(Count, item.MaxCount)));
                    Count -= Math.Min(item.MaxCount, Count);
                }

                break;
            }
            else
            {
                _inven[item.Type].Add(item.NameID, new List<Item>());
            }
        }
    }

    public void AddItem(string name, int count, bool isEquip)
    {
        Item item = DataManager.Instance.CreateItem(name, count);
        item.IsEquipped = isEquip;
        while (true)
        {
            List<Item> a;
            if (_inven[item.Type].TryGetValue(item.NameID, out a))
            {
                //넣는갯수에 따라 빈공간에 아이템을 넣을것.
                int Count = item.Count;
                for (int i = 0; i < a.Count && Count != 0; ++i)
                {
                    if (a[i].Count + Count > item.MaxCount)
                    {
                        Count -= item.MaxCount - a[i].Count;
                        a[i].Count = item.MaxCount;
                    }
                    else
                    {
                        a[i].Count += Count;
                        Count = 0;
                    }
                }

                //넣는갯수가 많으면 아이템을 추가로 만들것
                while (Count > 0)
                {
                    a.Add(new Item(item, Math.Min(Count, item.MaxCount)));
                    Count -= Math.Min(item.MaxCount, Count);
                }

                break;
            }
            else
            {
                _inven[item.Type].Add(item.NameID, new List<Item>());
            }
        }
    }
    public int ShowItem(ItemType type, Item.ShowType showType = ShowType.End)
    {
        int Count = 1;
        foreach (var items in _inven[type])
        {
            foreach (var item in items.Value)
            {
                item.ShowInfo(Count++, showType);
            }
        }
        return Count - 1;
    }
    public int ShowItem(int index, Item.ShowType showType = ShowType.End)
    {
        int Count = 1;
        foreach (var items in _inven[(ItemType)index])
        {
            foreach (var item in items.Value)
            {
                item.ShowInfo(Count++, showType);
            }
        }
        return Count - 1;
    }
    public int ShowAll(Item.ShowType showType = ShowType.End)
    {
        int Count = 1;
        foreach (var Dic in _inven)
        {
            foreach (var items in Dic.Value)
            {
                foreach (var item in items.Value)
                {
                    item.ShowInfo(Count++, showType);
                }
            }
        }
        return Count - 1;
    }
    public void ShowNoIndexAll()
    {
        foreach (var Dic in _inven)
        {
            foreach (var items in Dic.Value)
            {
                foreach (var item in items.Value)
                {
                    item.ShowInfo(0);
                }
            }
        }
    }
    public int GetAllItemCount()
    {
        int count = 0;
        foreach (var Dic in _inven)
        {
            foreach (var items in Dic.Value)
            {
                foreach (var item in items.Value)
                {
                    count++;
                }
            }
        }
        return count;
    }
    public int DeleteItem(int index, ref int count, ItemType type = ItemType.End)
    {
        //End면 전체보기에서 고른거고.. 타입을 정해주면 그 타입에서 고른거고..
        //반환값을 안받으면 버린거고 받으면 판거고
        ItemType i = 0;
        string select = "";
        if (type == ItemType.End)
        {  
            foreach (var Dic in _inven)
            {
                foreach (var items in Dic.Value)
                {
                    if (index - items.Value.Count > 0)
                        index -= items.Value.Count;
                    else
                    {
                        select = items.Key;
                        index -= 1;
                        break;
                    }
                }

                if (select != "")
                    break;
                ++i;
            }
        }
        else
        {
            i = type;
            foreach (var items in _inven[type])
            {
                if (index - items.Value.Count > 0)
                    index -= items.Value.Count;
                else
                {
                    select = items.Key;
                    index -= 1;
                    break;
                }
            }
        }
        if (select == "")
            return 0;
        Item item = _inven[i][select][index];
        int gold = item.Gold * 70 / 100;
        if (item.Count > count)
        {
            item.Count -= count;
        }
        else
        {
            count = item.Count;
            if(item.IsEquipped)
                DataManager.Instance.Player.ToggleEquip(item);
            _inven[i][select].RemoveAt(index);
        }

        return count * gold;
    }
    public Item? GetItem(int index, ItemType type = ItemType.End)
    {
        ItemType i = 0;
        string select = "";
        if (type == ItemType.End)
        {
            foreach (var Dic in _inven)
            {
                foreach (var items in Dic.Value)
                {
                    if (index - items.Value.Count > 0)
                        index -= items.Value.Count;
                    else
                    {
                        select = items.Key;
                        index -= 1;
                        break;
                    }
                }

                if (select != "")
                    break;
                ++i;
            }
        }
        else
        {
            i = type;
            foreach (var items in _inven[type])
            {
                if (index - items.Value.Count > 0)
                    index -= items.Value.Count;
                else
                {
                    select = items.Key;
                    index -= 1;
                    break;
                }
            }
        }
        if (select == "")
            return null;

        return _inven[i][select][index];
    }
    public bool GetEquippedIndex(EquipType type, out int index)
    {
        index = 0;
        foreach (var Dic in _inven)
        {
            foreach (var items in Dic.Value)
            {
                foreach (var item in items.Value)
                {
                    index++;
                    if (item.EquipType == type && item.IsEquipped)
                        return true;
                }
            }
        }
        return false;
    }

    public string GetSaveData()
    {
        string str = "";
        foreach (var Dic in _inven)
        {
            foreach (var items in Dic.Value)
            {
                foreach (var item in items.Value)
                {
                    str += item.NameID + "|" + item.Count.ToString() + "|" + item.IsEquipped.ToString() + "|";
                }
            }
        }
        str = str.Remove(str.Length - 1);
        return str;
    }
    public void SetData(string[] data)
    {
        for (ItemType i = 0; i < ItemType.End; ++i)
        {
            _inven[i].Clear();
        }
        for (int i = 0; i < data.Length;)
        {
            string name = data[i];
            int count = int.Parse(data[i + 1]);
            bool equip = Boolean.Parse(data[i + 2]);
            AddItem(name, count, equip);
            i += 3;
        }
    }
    Dictionary<ItemType, Dictionary<string, List<Item>>> _inven;
}