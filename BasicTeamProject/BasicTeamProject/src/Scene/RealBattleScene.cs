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
        private Data.Monster monster;

        protected override void SetFunctionList()
        {
            _FunctionList.Add("BattleSelectScene");
        }

        protected override void PreOperate()
        {
            base.PreOperate();
            preInputNum = _dataManager.InputMemory.PreInput;
            monster = _dataManager.Monsters[preInputNum - 1];

            monster.CurrentHp -= (int)_dataManager.Player.Att;
            if (monster.CurrentHp > 0) 
            {
                _dataManager.Player.CurrentHP -= (int)monster.Att;
            } 
        }

        protected override void WriteView()
        {
            Console.WriteLine("전투 로그");
            enter();

            Console.WriteLine($"{_dataManager.Player.NameID}의 공격");
            Thread.Sleep(300);
            Console.WriteLine($"Lv.{monster.Level} {monster.NameID}을(를) 맞췄습니다. [대미지 : {(int)_dataManager.Player.Att}]");
            Thread.Sleep(300);

            enter();
            Console.WriteLine($"Lv.{monster.Level} {monster.NameID}");
            Console.WriteLine($"HP {monster.CurrentHp}");
            Thread.Sleep(300);

            enter();
            if (monster.CurrentHp > 0)
            {
                Console.WriteLine($"{monster.NameID}의 공격");
                Thread.Sleep(300);
                Console.WriteLine($"[받은 대미지 : {monster.Att}]");
                Thread.Sleep(300);
            }

            enter();
            Console.WriteLine($"Lv.{_dataManager.Player.Level} {_dataManager.Player.NameID}");
            Console.WriteLine($"HP {_dataManager.Player.CurrentHP}");
            Thread.Sleep(300);

            enter();
            Console.WriteLine("0. 다음");
            enter();

        }
    }
}
