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

            for (int i = 0; i < _dataManager.Monsters.Count(); i++)
            {
                Monster monster = _dataManager.Monsters[i];
                if (monster.CurrentHP > 0)
                    monster.ShowInfo(i);
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
            enter();
        }
    }
}
