﻿using BasicTeamProject.Data;

namespace BasicTeamProject.Scene
{
    internal class InventoryEquipScene : Scene
    {
        private int ItemTotalCount = 0;
        protected override void SetFunctionList()
        {
            _FunctionList.Add("InventoryScene");
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
            ItemTotalCount = _dataManager.Inventory.ShowItem(ItemType.Equip);
            enter();
            Console.WriteLine("0. 나가기");
            EndView();
        }

        protected override void afterOperate()
        {
            base.afterOperate();
            int key, index;
            while (!_dataManager.InputMemory.TryGetKey(ItemTotalCount + 1, out key))
            {
                Console.WriteLine("잘못 입력하셨습니다.");
                Console.Write(">>");
            }

            if (key != 0)
            {
                var changeItem = _dataManager.Inventory.GetItem(key);

                if (_dataManager.Inventory.GetEquippedIndex(changeItem.EquipType, out index) && index != key)
                {
                    Item currentItem = _dataManager.Inventory.GetItem(index, ItemType.Equip);
                    _dataManager.Player.ToggleEquip(currentItem);
                }
                _dataManager.Player.ToggleEquip(changeItem);

            }
            _dataManager.InputMemory.PreInput = (key == 0 ? 1 : 2);
            _dataManager.InputMemory.InputComplete = true;
        }
    }
}
