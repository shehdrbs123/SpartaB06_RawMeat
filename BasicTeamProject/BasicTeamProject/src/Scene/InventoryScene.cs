using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BasicTeamProject.Scene
{
    internal class InventoryScene : Scene
    {
        protected override void SetFunctionList()
        {
            _FunctionList.Add("InventoryEquipScene");
            _FunctionList.Add("InventoryConsumeScene");
        }

        protected override void WriteView()
        {
            Console.WriteLine("인벤토리");
            Console.WriteLine("보유 중인 아이템을 관리할 수 있습니다.");
            enter(); enter();
            Console.WriteLine("[플레이어 상태]");
            _dataManager.Player.ShowAllInfo();
            enter(); enter();
            Console.WriteLine("[아이템 목록]");
            _dataManager.Inventory.ShowNoIndexAll();
            enter();
            Console.WriteLine("0. 나가기");
            Console.WriteLine("1. 장착 관리");
            Console.WriteLine("2. 소비 관리");
            EndView();
        }
        protected override void afterOperate()
        {
            base.afterOperate();
            int key;
            while (!_dataManager.InputMemory.TryGetKey(0,_FunctionList.Count,out key))
            {
                Console.WriteLine("잘못 입력하셨습니다.");
                Console.Write(">>");
            }
            _dataManager.InputMemory.PreInput = key;
            _dataManager.InputMemory.InputComplete = true;
        }
    }
}
