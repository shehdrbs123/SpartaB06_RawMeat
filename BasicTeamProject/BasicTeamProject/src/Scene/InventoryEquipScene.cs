using BasicTeamProject.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BasicTeamProject.Scene
{
    internal class InventoryEquipScene : Scene
    {
        protected override void SetFunctionList()
        {
            _FunctionList.Add("InventoryEquipScene");
        }

        protected override void WriteView()
        {
            Console.WriteLine("인벤토리 - 장착 관리");
            Console.WriteLine("보유 중인 아이템을 관리할 수 있습니다.");
            enter();
            Console.WriteLine("[아이템 목록]");
            _dataManager.Inventory.ShowAll();
            enter();
            Console.WriteLine("0. 나가기");
            EndView();
        }

        protected override void afterOperate()
        {
            base.afterOperate();
            int key, index;
            _dataManager.InputMemory.SetRange(0, _dataManager.Inventory.GetAllItemCount());
            while (!_dataManager.InputMemory.TryGetKey(out key))
            {
                Console.WriteLine("잘못 입력하셨습니다");
                Console.Write(">>");
            }

            if (key != 0)
            {
                var tempItem = _dataManager.Inventory.GetItem(key);
                if (tempItem.EquipType != EquipType.End)
                {
                    if (!tempItem.IsEquipped && _dataManager.Inventory.GetEquippedIndex(tempItem.EquipType, out index))
                        _dataManager.Inventory.GetItem(index).IsEquipped = false;

                    _dataManager.Inventory.GetItem(key).IsEquipped = !_dataManager.Inventory.GetItem(key).IsEquipped;
                }
                tempItem.ShowInfo();
                Console.WriteLine(tempItem.EquipType.ToString());
            }

            _dataManager.InputMemory.PreInput = (key == 0 ? 0 : 1);
            _dataManager.InputMemory.InputComplete = true;
        }
    }
}
