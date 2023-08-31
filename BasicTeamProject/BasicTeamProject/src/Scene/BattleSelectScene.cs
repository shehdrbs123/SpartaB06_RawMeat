using BasicTeamProject.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BasicTeamProject.Scene
{
    public class BattleSelectScene : Scene
    {
        public static int remainingMonster;
        public static int selectedMonster = 1;
        protected override void SetFunctionList()
        {
            _FunctionList.Add("BattleScene");
            _FunctionList.Add("BattleSelectScene");
            _FunctionList.Add("RealBattleScene");
        }
        
        protected override void WriteView()
        {
            Console.WriteLine("Battle!!");
            enter();
            int j = 1;
            Console.WriteLine("[몬스터]");
            for (int i = 0; i < _dataManager.Monsters.Count; i++)
            {
                var monster = _dataManager.Monsters[i];
                if (monster.CurrentHP > 0)
                {
                    monster.ShowInfo(i+1);
                }
            }
            enter();

            Console.WriteLine("[플레이어 정보]");
            enter();
            _dataManager.Player.ShowBattleInfo();

            enter();
            Console.WriteLine("0. 취소");
            enter();
        }

        protected override void afterOperate()
        {
            base.afterOperate();
            int key;
            if (int.TryParse(Console.ReadLine(), out key))
            {
                if (1 <= key && key <= remainingMonster)
                {
                    selectedMonster = key;
                    _dataManager.InputMemory.InputComplete = true;
                    _dataManager.InputMemory.PreInput = 3;
                }
                else if (key > remainingMonster)
                {
                    Console.WriteLine("잘못 입력하셨습니다.");
                    Thread.Sleep(300);
                    _dataManager.InputMemory.InputComplete = true;
                    _dataManager.InputMemory.PreInput = 2;
                }
            }
            
            if (key == 0)
            {
                _dataManager.InputMemory.InputComplete = true;
                _dataManager.InputMemory.PreInput = 1;
            }
        }
    }
}
