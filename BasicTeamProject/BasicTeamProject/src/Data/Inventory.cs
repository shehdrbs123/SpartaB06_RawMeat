using System;
using System.Reflection;

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

    public int ShowItem(ItemType type)
    {
        int Count = 1;
        foreach (var items in _inven[type])
        {
            foreach (var item in items.Value)
            {
                Console.Write($"{Count++}. ");
                item.ShowInfo();
            }
        }
        return Count;
    }
    public int ShowItem(int index)
    {
        int Count = 1;
        foreach (var items in _inven[(ItemType)index])
        {
            foreach (var item in items.Value)
            {
                Console.Write($"{Count++}. ");
                item.ShowInfo();
            }
        }
        return Count;
    }
    public int ShowAll()
    {
        int Count = 1;
        foreach (var Dic in _inven)
        {
            foreach (var items in Dic.Value)
            {
                foreach (var item in items.Value)
                {
                    Console.Write($"{Count++}. ");
                    item.ShowInfo();
                }
            }
        }
        return Count;
    }

    public void ShowNoIndexAll()
    {
        foreach (var Dic in _inven)
        {
            foreach (var items in Dic.Value)
            {
                foreach (var item in items.Value)
                {
                    item.ShowInfo();
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

    public int DeleteItem(int index, int count = 1, ItemType type = ItemType.End)
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
        int gold = item.Gold;
        if (item.Count > count)
        {
            item.Count -= count;
        }
        else
        {
            count = item.Count;
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

    Dictionary<ItemType, Dictionary<string, List<Item>>> _inven;
}