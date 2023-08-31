using BasicTeamProject.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BasicTeamProject.Scene
{
    internal class BattleSkillSelectScene : Scene
    {
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
            Console.WriteLine("[몬스터]");
            for (int i = 0; i < _dataManager.Monsters.Count; i++)
            {
                var monster = _dataManager.Monsters[i];
                if (monster.CurrentHP <= 0) continue;
                Console.WriteLine($"Lv.{monster.Level} {monster.NameID} \tHP: {monster.CurrentHP} / {monster.MaxHP}");
            }
            enter(); enter();

            Console.WriteLine("[플레이어 정보]");
            enter();
            Console.WriteLine($"HP {_dataManager.Player.CurrentHP}/{_dataManager.Player.MaxHP}");
            Console.WriteLine($"MP {_dataManager.Player.CurrentMP}/{_dataManager.Player.MaxMP}");
            enter();
            Console.WriteLine($"[ 스킬 : {_dataManager.Player.Skills.Count} ]");
            enter();
            for(int i = 0; i < _dataManager.Player.Skills.Count; i++)
            {
                var skill = _dataManager.Player.Skills[i];
                skill.ShowInfo(i+1);
            }
            enter();
            Console.WriteLine("0. 취소");
            enter();
            Console.WriteLine("사용하실 스킬을 입력해주세요.");
            Console.Write(">>");
        }

        protected override void afterOperate()
        {
            base.afterOperate();
            int key;
            while (!_dataManager.InputMemory.TryGetKey(_dataManager.Player.Skills.Count + 1, out key))
            {
                Console.WriteLine("잘못 입력하셨습니다.");
                Console.Write(">>");
            }

            if (key != 0)
                _dataManager.Player.CurrentSkill = key;

            if (_dataManager.Player.GetSkillTargetAble())
            {
                _dataManager.InputMemory.PreInput = 2;
            }
            else
            {
                _dataManager.InputMemory.PreInput = (key == 0 ? 1 : 3);
                BattleSelectScene.selectedMonster = 1;
            }
            _dataManager.InputMemory.InputComplete = true;
        }
    }
}
