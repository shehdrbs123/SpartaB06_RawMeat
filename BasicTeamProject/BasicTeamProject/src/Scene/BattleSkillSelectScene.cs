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
            _FunctionList.Add("BattleSkillSelectScene");
        }

        protected override void WriteView()
        {
            Console.WriteLine("Battle!!");
            enter();
            for (int i = 0; i < _dataManager.Monsters.Count; i++)
            {
                var monster = _dataManager.Monsters[i];
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

            for(int i = 0; i < _dataManager.Player.Skills.Count; i++)
            {
                var skill = _dataManager.Player.Skills[i];
                Console.WriteLine($"{i+1}");
                skill.ShowInfo();
                enter();
            }
            
            Console.WriteLine("0. 취소");
            enter();
        }
    }
}
