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
            enter(); enter();
            Console.WriteLine("[플레이어 상태]");
            _dataManager.Player.ShowAllInfo();
            enter(); enter();
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
            while (!_dataManager.InputMemory.TryGetKey(_dataManager.Inventory.GetAllItemCount() + 1, out key))
            {
                Console.WriteLine("잘못 입력하셨습니다");
                Console.Write(">>");
            }

            if (key != 0)
            {
                var tempItem = _dataManager.Inventory.GetItem(key);
                if (tempItem.EquipType != EquipType.End)
                {
                    if (_dataManager.Inventory.GetEquippedIndex(tempItem.EquipType, out index) && index != key)
                        _dataManager.Player.ToggleEquip(_dataManager.Inventory.GetItem(index));

                    _dataManager.Player.ToggleEquip(tempItem);
                }
            }
            _dataManager.InputMemory.PreInput = (key == 0 ? 0 : 1);
            _dataManager.InputMemory.InputComplete = true;
        }
    }
}
