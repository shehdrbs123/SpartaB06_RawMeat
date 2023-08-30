using BasicTeamProject.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BasicTeamProject.Scene
{
    internal class ShopBuyScene : Scene
    {
        protected override void SetFunctionList()
        {
            _FunctionList.Add("ShopScene");
            _FunctionList.Add("ShopBuyScene");
        }

        protected override void WriteView()
        {
            Console.WriteLine("상점이다!");
            enter();
            Console.WriteLine("0.나가기");
            _dataManager.Shop.ShowShopItem();
            Console.WriteLine("9.리셋하기 ( 500 G )");
        }

        protected override void afterOperate()
        {
            base.afterOperate();
            Console.Write("살거를 입력해주세요.");
            Console.Write(">>");
            int key = 0;
            if (int.TryParse(Console.ReadLine(), out key))
            {
                if (1 <= key && key <= _dataManager.Shop.GetListCount())
                {
                    int itemCount = 1;
                    int gold = 0;
                    if (_dataManager.Shop.GetItemCount(key) > 1)
                    {
                        Console.Write("개수를 입력해주세요.");
                        Console.Write(">>");
                        int.TryParse(Console.ReadLine(), out itemCount);
                    }

                    Item? item = _dataManager.Shop.GetlItem(key);
                    if (item is not null)
                    {
                        if (_dataManager.Player.Gold < item.Gold * itemCount)
                        {
                            itemCount = _dataManager.Player.Gold / item.Gold;
                        }
                        item = _dataManager.Shop.SellItem(key, itemCount);
                        if (item is not null)
                        {
                            gold = item.Gold;
                            _dataManager.Inventory.AddItem(item);
                            Console.WriteLine($"{item.NameID} {item.Count}개 구매완료!");
                            Console.WriteLine($"{_dataManager.Player.Gold} G -> {_dataManager.Player.Gold -= gold * item.Count} G");
                        }
                        else
                        {
                            Console.WriteLine("돈이 부족하다!");
                        }
                    }
                    else
                    {
                        Console.WriteLine("이미 팔린 아이템이다!!");
                    }

                    Console.ReadLine();
                }
                else if(key == 9)
                {
                    if (_dataManager.Player.Gold >= 500)
                    {
                        _dataManager.Shop.RenewItems();
                        Console.WriteLine("상점 리셋 완료!");
                        Console.WriteLine($"{_dataManager.Player.Gold} G -> {_dataManager.Player.Gold -= 500} G");
                    }
                    else
                        Console.WriteLine("돈이 부족하다!");

                    Console.ReadLine();
                }
            }


            _dataManager.InputMemory.InputComplete = true;
            _dataManager.InputMemory.PreInput = (key == 0) ? 1 : 2;
        }
    }
}
