using BasicTeamProject.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BasicTeamProject.Scene
{
    public class BattleScene : Scene
    {
        private int currentStage = 1;

        protected override void SetFunctionList()
        {
            _FunctionList.Add("BattleSelectScene");
            _FunctionList.Add("BattleSkillSelectScene");
            _FunctionList.Add("BattleConsumeScene");
        }

        protected override void PreOperate()
        {
            base.PreOperate();
            if (_dataManager.Monsters.Count == 0 || BattleSelectScene.remainingMonster == 0)
            {
                _dataManager.CreateDungeon(currentStage++);
                BattleSelectScene.remainingMonster = _dataManager.Monsters.Count;
            }
            _dataManager.InputMemory.SetRange(0,_FunctionList.Count);
        }

        protected override void WriteView()
        {
            Console.WriteLine("전투 시작");
            enter();
            foreach (var monster in _dataManager.Monsters)
            {

                if (!monster.isDead)
                    Console.WriteLine($"Lv.{monster.Level} {monster.NameID} HP: {monster.CurrentHP}");
            }

            enter();

            Console.WriteLine("[플레이어 정보]");
            enter();
            Console.WriteLine($"Lv.{_dataManager.Player.Level} {_dataManager.Player.NameID} ({_dataManager.Player.job})");
            Console.WriteLine($"HP {_dataManager.Player.CurrentHP}/{_dataManager.Player.MaxHP}");
            Console.WriteLine($"MP {_dataManager.Player.CurrentMP}/{_dataManager.Player.MaxMP}");
            enter();

            enter();
            Console.WriteLine("0. 나가기");
            Console.WriteLine("1. 공격");
            Console.WriteLine("2. 스킬");
            Console.WriteLine("3. 소모품 사용");
            enter();
        }
    }
}
