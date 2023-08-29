using BasicTeamProject.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BasicTeamProject.Scene
{
    public class ShopInvenEquip : Scene
    {
        protected override void SetFunctionList()
        {
            _FunctionList.Add("ShopScene");
        }
        protected override void afterOperate()
        {
            base.afterOperate();
            Console.Write("팔거를 입력해주세요");
            Console.Write(">>");
            int key = 0;
            if (int.TryParse(Console.ReadLine(), out key))
            {
                if (1 <= key && key <= count)
                {
                    int itemCount = 1;
                    Console.Write("개수를 입력해주세요");
                    Console.Write(">>");
                    if (int.TryParse(Console.ReadLine(), out itemCount))
                    {
                        if (1 <= itemCount)
                        {
                          _dataManager.Player.MaxHp +=  _dataManager.Inventory.DeleteItem(key, itemCount, ItemType.Equip);
                        }
                    }
                }
            }
            _dataManager.InputMemory.InputComplete = true;
            _dataManager.InputMemory.PreInput = 1;
        }
        protected override void WriteView()
        {
            count = _dataManager.Inventory.ShowItem(ItemType.Equip);
            //여기서 카운트를 한번 더 받고... 팔아야됨?
        }
        int count;
    }
    public class ShopInvenConsumable : Scene
    {
        protected override void SetFunctionList()
        {
            _FunctionList.Add("ShopScene");
        }
        protected override void afterOperate()
        {
            base.afterOperate();
            Console.Write("팔거를 입력해주세요");
            Console.Write(">>");
            int key = 0;
            if (int.TryParse(Console.ReadLine(), out key))
            {
                if (1 <= key && key <= count)
                {
                    int itemCount = 1;
                    Console.Write("개수를 입력해주세요");
                    Console.Write(">>");
                    if (int.TryParse(Console.ReadLine(), out itemCount))
                    {
                        if (1 <= itemCount)
                        {
                            _dataManager.Player.MaxHp += _dataManager.Inventory.DeleteItem(key, itemCount, ItemType.Consumable);
                        }
                    }
                }
            }
            _dataManager.InputMemory.InputComplete = true;
            _dataManager.InputMemory.PreInput = 1;
        }
        protected override void WriteView()
        {
            count = _dataManager.Inventory.ShowItem(ItemType.Consumable);
        }
        int count;
    }

    public class ShopInvenAll : Scene
    {
        protected override void SetFunctionList()
        {
            _FunctionList.Add("ShopScene");
        }
        protected override void afterOperate()
        {
            base.afterOperate();
            Console.Write("팔거를 입력해주세요");
            Console.Write(">>");
            int key = 0;
            if (int.TryParse(Console.ReadLine(), out key))
            {
                if (1 <= key && key <= count)
                {
                    int itemCount = 1;
                    Console.Write("개수를 입력해주세요");
                    Console.Write(">>");
                    if (int.TryParse(Console.ReadLine(), out itemCount))
                    {
                        if (1 <= itemCount)
                        {
                            _dataManager.Player.MaxHp += _dataManager.Inventory.DeleteItem(key, itemCount);
                        }
                    }
                }
            }
            _dataManager.InputMemory.InputComplete = true;
            _dataManager.InputMemory.PreInput = 1;
        }
        protected override void WriteView()
        {
            count = _dataManager.Inventory.ShowAll();
        }
        int count;
    }
}
