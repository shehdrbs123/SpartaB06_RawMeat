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
            _FunctionList.Add("BattleSelectScene");
            _FunctionList.Add("BattleSelectScene");
            _FunctionList.Add("BattleSelectScene");
        }

        protected override void WriteView()
        {
            Console.WriteLine("전투");
            enter();
            for (int i = 0; i < _dataManager.Monsters.Count; i++)
            {
                var monster = _dataManager.Monsters[i];
                Console.WriteLine($"{i + 1}.레벨: {monster.Level} 이름: {monster.NameID} 체력: {monster.MaxHp}");
            }
            enter();

            Console.WriteLine("[플레이어 정보]");
            enter();
            Console.WriteLine($"레벨: {_dataManager.Player.Level}\n이름: {_dataManager.Player.NameID}" +
                $"({_dataManager.Player.job})\n체력: {_dataManager.Player.CurrentHP}");

            enter();
            Console.WriteLine("0. 취소");
            enter();

        }
    }
}
