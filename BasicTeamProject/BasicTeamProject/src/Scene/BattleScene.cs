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
        protected override void SetFunctionList()
        {
            _FunctionList.Add("BattleSelectScene");
            _FunctionList.Add("BattleSkillSelectScene");
        }

        protected override void PreOperate()
        {
            base.PreOperate();
            if (_dataManager.Monsters.Count == 0)
            {
                _dataManager.CreateDungeon(1);
            }
            _dataManager.InputMemory.SetRange(0,_FunctionList.Count);
        }

        protected override void WriteView()
        {
            Console.WriteLine("전투 시작");
            enter();
            foreach (var monster in _dataManager.Monsters)
            {
                if (monster.CurrentHP <= 0) continue;
                Console.WriteLine($"Lv.{monster.Level} {monster.NameID} \tHP: {monster.CurrentHP} / {monster.MaxHP}");
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
