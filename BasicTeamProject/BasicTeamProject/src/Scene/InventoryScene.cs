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
            _FunctionList.Add("MainScene");
            _FunctionList.Add("MainScene");
        }

        protected override void afterOperate()
        {
            base.afterOperate();
            int key;
            _dataManager.InputMemory.SetRange(1, 2);
            while (!_dataManager.InputMemory.TryGetKey(out key))
            {
                Console.WriteLine("잘못 입력하셨습니다");
                Console.Write(">>");
            }
            _dataManager.InputMemory.PreInput = key;
            _dataManager.InputMemory.InputComplete = true;
        }

        protected override void WriteView()
        {
            Console.WriteLine("인벤토리");
            Console.WriteLine("보유 중인 아이템을 관리할 수 있습니다.");
            enter();
            Console.WriteLine("[아이템 목록]");
            _dataManager.Inventory.ShowAll();
            enter();
            Console.WriteLine("2. 장착 관리");
            Console.WriteLine("1. 나가기");
            enter();
        }
    }
}
