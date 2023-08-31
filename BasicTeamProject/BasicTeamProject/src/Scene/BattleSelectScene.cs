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
            Console.WriteLine("전투");
            enter();
            int j = 1;
            for (int i = 0; i < _dataManager.Monsters.Count; i++)
            {
                var monster = _dataManager.Monsters[i];
                if (monster.CurrentHP > 0)
                {
                    Console.WriteLine($"{j++}.Lv.{monster.Level} " +
                        $"{monster.NameID} HP: {monster.CurrentHP}");
                }
            }
            enter();

            Console.WriteLine("[플레이어 정보]");
            enter();
            Console.WriteLine($"Lv.{_dataManager.Player.Level} {_dataManager.Player.NameID} ({_dataManager.Player.job})");
            Console.WriteLine($"HP {_dataManager.Player.CurrentHP}/{_dataManager.Player.MaxHP}");
            Console.WriteLine($"MP {_dataManager.Player.CurrentMP}/{_dataManager.Player.MaxMP}");

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
