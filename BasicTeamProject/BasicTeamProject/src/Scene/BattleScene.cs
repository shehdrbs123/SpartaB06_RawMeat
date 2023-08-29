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
            _FunctionList.Add("MainScene");
            _FunctionList.Add("MainScene");
        }

        protected override void PreOperate()
        {
            base.PreOperate();
            if (_dataManager.Monsters == null)
            {
                _dataManager.CreateDungeon(1);
            }
        }

        protected override void WriteView()
        {
            Console.WriteLine("전투 시작");
            foreach (var monster in _dataManager.Monsters)
            {
                Console.WriteLine($"레벨: {monster.Level} 이름: {monster.NameID} 체력: {monster.MaxHp}");
            }

            Console.WriteLine("[플레이어 정보]");
            Console.WriteLine($"레벨: {_dataManager.Player.Level} 이름: {_dataManager.Player.NameID}" +
                $"({_dataManager.Player.job}) 체력: {_dataManager.Player.CurrentHP}");

            Console.WriteLine("1. 공격");
            Console.WriteLine("0. 나가기");
        }
    }
}
