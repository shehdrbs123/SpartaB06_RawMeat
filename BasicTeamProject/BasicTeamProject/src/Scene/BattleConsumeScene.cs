using BasicTeamProject.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BasicTeamProject.Scene
{
    internal class BattleConsumeScene : Scene
    {
        int itemCount = 0;
        protected override void SetFunctionList()
        {
            _FunctionList.Add("BattleScene");
            _FunctionList.Add("RealBattleScene");
        }

        protected override void WriteView()
        {
            Console.WriteLine("보유 중인 소비아이템을 사용할 수 있습니다.");
            enter(); enter();
            Console.WriteLine("[플레이어 상태]");
            _dataManager.Player.ShowAllInfo();
            enter(); enter();
            Console.WriteLine("[소비 아이템]");
            itemCount = _dataManager.Inventory.ShowItem(ItemType.Consumable);
            enter();
            Console.WriteLine("0. 취소");
            enter();
            Console.WriteLine("어떤 아이템을 복용하시겠습니까?");
            Console.Write(">>");
        }

        protected override void afterOperate()
        {
            base.afterOperate();
            TypeOfAbility ability = TypeOfAbility.CurrentHP;
            int abilityValue = 0;
            int key = 0;
            int count = 1;

            while (!_dataManager.InputMemory.TryGetKey(0, itemCount + 1, out key))
            {
                Console.WriteLine("잘못 입력하셨습니다.");
                Console.Write(">>");
            }

            _dataManager.InputMemory.InputComplete = true;
            if (key > 0)
            {
                Item healItem = _dataManager.Inventory.GetItem(key, ItemType.Consumable);

                healItem.GetItemAbility(out ability, out abilityValue);

                HealScene.AbilityValue = abilityValue;
                HealScene.TypeOfAbility = ability;

                _dataManager.Inventory.DeleteItem(key, ref count, ItemType.Consumable);

                int beforeAddValue = 0;
                int addedValue = 0;
                _dataManager.Player.AddStatus(ability, abilityValue, out addedValue, out beforeAddValue);

                Console.ReadLine(); 
                _dataManager.InputMemory.PreInput = 2;
                BattleSelectScene.selectedMonster = 1;
                _dataManager.Player.Attack = false;
            }
            else
               _dataManager.InputMemory.PreInput = 1;
        }
    }
}
