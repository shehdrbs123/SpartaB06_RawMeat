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
        protected override void SetFunctionList()
        {
            _FunctionList.Add("RealBattleScene");
        }
        

        protected override void WriteView()
        {
            Console.WriteLine("전투");
            enter();
            for (int i = 0; i < _dataManager.Monsters.Count; i++)
            {
                var monster = _dataManager.Monsters[i];
                if (monster.CurrentHP <= 0) continue;
                Console.WriteLine($"{i + 1}.Lv.{monster.Level} {monster.NameID} HP: {monster.MaxHP}");
            }
            enter();

            Console.WriteLine("[플레이어 정보]");
            enter();
            Console.WriteLine($"Lv.{_dataManager.Player.Level} {_dataManager.Player.NameID}" +
                $"({_dataManager.Player.job})\n체력: {_dataManager.Player.CurrentHP}");

            enter();
            Console.WriteLine("0. 취소");
            enter();
        }
    }
}
