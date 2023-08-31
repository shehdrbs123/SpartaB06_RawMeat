using BasicTeamProject.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BasicTeamProject.Scene
{
    public class RewardScene : Scene
    {
        protected override void SetFunctionList()
        {
            _FunctionList.Add("BattleScene");
        }

        protected override void PreOperate()
        {
            base.PreOperate();
            _dataManager.InputMemory.SetRange(0, 1);
        }

        protected override void WriteView()
        {
            Console.WriteLine("축하합니다 던전을 클리어했습니다");

            int gold;
            int exp;

            foreach (var monster in _dataManager.Monsters)
            {
                List<Item> items = _dataManager.GetRewards(monster.NameID, out gold, out exp);
                Console.WriteLine($"[{monster.NameID}를 죽인 보상]");
                Console.WriteLine($"{_dataManager.Player.Gold} G -> {_dataManager.Player.Gold += gold} G");
                Console.WriteLine($"{_dataManager.Player.CurrentExp} EXP -> {_dataManager.Player.CurrentExp += exp} EXP");

                foreach (var item  in items)
                {
                    Console.WriteLine($"{item.NameID}를 얻었습니다");
                    _dataManager.Inventory.AddItem(item);
                }
            }

            while (_dataManager.GetMaxExp() <= _dataManager.Player.CurrentExp)
            {
                _dataManager.Player.LevelUP();
            }

            _dataManager.Monsters.Clear();
            enter();
            Console.WriteLine("계속 진행하시겠습니까?");
            enter();
            Console.WriteLine("1. 계속 진행");
            enter();
            Console.WriteLine("0. 나가기");
        }

        //protected override void afterOperate()
        //{
        //    base.afterOperate();


        //}
    }
}
