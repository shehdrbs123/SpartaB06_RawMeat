using BasicTeamProject.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BasicTeamProject.Scene
{
    public class ShopInvenEquipScene : Scene
    {
        protected override void SetFunctionList()
        {
            _FunctionList.Add("ShopScene");
            _FunctionList.Add("ShopInvenEquipScene");
        }
        protected override void afterOperate()
        {
            base.afterOperate();
            Console.Write("팔거를 입력해주세요");
            Console.Write(">>");
            int key = 0;

            if (int.TryParse(Console.ReadLine(), out key))
            {
                if (1 <= key && key < count)
                {
                    int itemCount = 1;
                    Console.Write("개수를 입력해주세요");
                    Console.Write(">>");
                    if (int.TryParse(Console.ReadLine(), out itemCount))
                    {
                        if (1 <= itemCount)
                        {
                            int gold = _dataManager.Inventory.DeleteItem(key, ref itemCount, ItemType.Equip);
                            if(gold > 0)
                            {
                                Console.WriteLine($"{itemCount}개 판매완료!");
                                Console.WriteLine($"{_dataManager.Player.Gold} G -> {_dataManager.Player.Gold += gold} G");
                                Console.ReadLine();
                            }
                        }
                    }
                }
            }
            
                _dataManager.InputMemory.InputComplete = true;
                _dataManager.InputMemory.PreInput = 2;

            if (key == 0)
                _dataManager.InputMemory.PreInput = 1;
        }
        protected override void WriteView()
        {
            Console.WriteLine("아이템을 판매하기 위해 장비창을 열었습니다.");
            enter();
            Console.WriteLine("[아이템 목록]");
            count = _dataManager.Inventory.ShowItem(ItemType.Equip, Item.ShowType.Sell);
            enter();
            Console.WriteLine("0. 나가기");
            enter();
        }
        int count;
    }
    public class ShopInvenConsumableScene : Scene
    {
        protected override void SetFunctionList()
        {
            _FunctionList.Add("ShopScene");
            _FunctionList.Add("ShopInvenConsumableScene");
        }
        protected override void afterOperate()
        {
            base.afterOperate();
            Console.Write("팔거를 입력해주세요");
            Console.Write(">>");
            int key = 0;
            if (int.TryParse(Console.ReadLine(), out key))
            {
                if (1 <= key && key < count)
                {
                    int itemCount = 1;
                    Console.Write("개수를 입력해주세요");
                    Console.Write(">>");
                    if (int.TryParse(Console.ReadLine(), out itemCount))
                    {
                        if (1 <= itemCount)
                        {
                            int gold = _dataManager.Inventory.DeleteItem(key, ref itemCount, ItemType.Consumable);
                            if (gold > 0)
                            {
                                Console.WriteLine($"{itemCount}개 판매완료!");
                                Console.WriteLine($"{_dataManager.Player.Gold} G -> {_dataManager.Player.Gold += gold} G");
                                Console.ReadLine();
                            }
                        }
                    }
                }
            }
            _dataManager.InputMemory.InputComplete = true;
            _dataManager.InputMemory.PreInput = 2;

            if (key == 0)
                _dataManager.InputMemory.PreInput = 1;
        }
        protected override void WriteView()
        {
            Console.WriteLine("아이템을 판매하기 위해 소비창을 열었습니다.");
            enter();
            Console.WriteLine("[아이템 목록]");
            count = _dataManager.Inventory.ShowItem(ItemType.Consumable, Item.ShowType.Sell);
            enter();
            Console.WriteLine("0. 나가기");
            enter();
        }
        int count;
    }

    public class ShopInvenAllScene : Scene
    {
        protected override void SetFunctionList()
        {
            _FunctionList.Add("ShopScene");
            _FunctionList.Add("ShopInvenAllScene");
        }
        protected override void afterOperate()
        {
            base.afterOperate();
            Console.Write("팔거를 입력해주세요");
            Console.Write(">>");
            int key = 0;
            if (int.TryParse(Console.ReadLine(), out key))
            {
                if (1 <= key && key < count)
                {
                    int itemCount = 1;
                    Console.Write("개수를 입력해주세요");
                    Console.Write(">>");
                    if (int.TryParse(Console.ReadLine(), out itemCount))
                    {
                        if (1 <= itemCount)
                        {
                            int gold = _dataManager.Inventory.DeleteItem(key, ref itemCount);
                            if (gold > 0)
                            {
                                Console.WriteLine($"{itemCount}개 판매완료!");
                                Console.WriteLine($"{_dataManager.Player.Gold} G -> {_dataManager.Player.Gold += gold} G");
                                Console.ReadLine();
                            }
                        }
                    }
                }
            }
            _dataManager.InputMemory.InputComplete = true;
            _dataManager.InputMemory.PreInput = 2;

            if (key == 0)
                _dataManager.InputMemory.PreInput = 1;
        }
        protected override void WriteView()
        {
            Console.WriteLine("아이템을 판매하기 위해 인벤토리를 열었습니다.");
            enter();
            Console.WriteLine("[아이템 목록]");
            count = _dataManager.Inventory.ShowAll(Item.ShowType.Sell);
            enter();
            Console.WriteLine("0. 나가기");
            enter();
        }
        int count;
    }
}
