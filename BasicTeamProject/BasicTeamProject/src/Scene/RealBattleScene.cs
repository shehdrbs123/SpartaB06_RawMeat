using BasicTeamProject.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BasicTeamProject.Scene
{
    public class RealBattleScene : Scene
    {
        private int preInputNum;
        private Monster monster;

        protected override void SetFunctionList()
        {
            _FunctionList.Add("BattleScene");
        }

        protected override void PreOperate()
        {
            base.PreOperate();
            preInputNum = BattleSelectScene.selectedMonster;
            monster = _dataManager.Monsters[preInputNum - 1];

            monster.CurrentHP -= (int)_dataManager.Player.GetDamage();
            if (monster.CurrentHP > 0) 
            {
                _dataManager.Player.CurrentHP -= (int)monster.Att;
            }
            _dataManager.InputMemory.SetRange(0,1);
        }

        protected override void WriteView()
        {
            Console.WriteLine("전투 로그");
            enter();

            Console.WriteLine($"{_dataManager.Player.NameID}의 공격");
            Thread.Sleep(600);
            Console.WriteLine($"Lv.{monster.Level} {monster.NameID}을(를) 맞췄습니다. " +
                $"[대미지 : {(int)_dataManager.Player.GetDamage()}]");
            Thread.Sleep(600);

            enter();
            Console.WriteLine($"Lv.{monster.Level} {monster.NameID}");
            Console.WriteLine($"HP {monster.CurrentHP}");
            Thread.Sleep(600);

            enter();
            if (monster.CurrentHP > 0)
            {
                Console.WriteLine($"{monster.NameID}의 공격");
                Thread.Sleep(600);
                Console.WriteLine($"[받은 대미지 : {monster.Att}]");
                Thread.Sleep(600);
            }

            enter();
            Console.WriteLine($"Lv.{_dataManager.Player.Level} {_dataManager.Player.NameID} ({_dataManager.Player.job})");
            Console.WriteLine($"HP {_dataManager.Player.CurrentHP}/{_dataManager.Player.MaxHP}");
            Console.WriteLine($"MP {_dataManager.Player.CurrentMP}/{_dataManager.Player.MaxMP}");
            Thread.Sleep(600);

            enter();
            Console.WriteLine("0. 다음");
            enter();
        }

        protected override void afterOperate()
        {
            base.afterOperate();
            if (monster.CurrentHP <= 0)
            {
                var temp = monster;
                _dataManager.Monsters.Remove(monster);
                _dataManager.Monsters.Add(temp);
            }
            _dataManager.Player.TurnCheck();
            _dataManager.InputMemory.InputComplete = true;
            _dataManager.InputMemory.PreInput = 1;
        }
    }
}
