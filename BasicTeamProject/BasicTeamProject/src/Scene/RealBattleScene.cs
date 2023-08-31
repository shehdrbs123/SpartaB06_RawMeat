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
        private bool mobDodge = false;
        private bool mobCrit = false;
        private bool playerDodge = false;
        private bool playerCrit = false;

        private Monster monster;
        Random random = new Random();

        protected override void SetFunctionList()
        {
            _FunctionList.Add("BattleScene");
        }

        protected override void PreOperate()
        {
            base.PreOperate();
            preInputNum = BattleSelectScene.selectedMonster;
            monster = _dataManager.Monsters[preInputNum - 1];
            if (random.Next(0, 101) > monster.Dodge)
            {
                mobDodge = false;
                if (random.Next(0, 101) < _dataManager.Player.Critical + _dataManager.Player.ExtraCritical)
                {
                    monster.CurrentHP -= (int)((_dataManager.Player.GetDamage() * 1.5f) - monster.Def);
                    playerCrit = true;
                }
                else
                {
                    monster.CurrentHP -= (int)(_dataManager.Player.GetDamage() - monster.Def);
                    playerCrit = false;
                }
            }
            else 
            {
                mobDodge = true;
                Console.WriteLine("공격이 빗나갔습니다");
            }

            if (random.Next(0, 101) > _dataManager.Player.Dodge)
            {
                playerDodge = false;
                if (random.Next(0, 101) < monster.Critical)
                {
                    _dataManager.Player.CurrentHP -= (int)((monster.Att * 1.5f) - (_dataManager.Player.Def + _dataManager.Player.ExtraDef));
                    mobCrit = true;
                }
                else
                {
                    _dataManager.Player.CurrentHP -= (int)(monster.Att - (_dataManager.Player.Def + _dataManager.Player.ExtraDef));
                    mobCrit = false;
                }
            }
            else
            {
                playerDodge = true;
                Console.WriteLine("회피에 성공했습니다");
            }

            _dataManager.InputMemory.SetRange(0,1);
        }

        protected override void WriteView()
        {
            Console.WriteLine("전투 로그");
            enter();
            if (!mobDodge)
            {
                Console.WriteLine($"{_dataManager.Player.NameID}의 공격");
                Thread.Sleep(600);
                if (playerCrit)
                {
                    Console.WriteLine("치명타 적중!");
                    Console.WriteLine($"Lv.{monster.Level} {monster.NameID}을(를) 맞췄습니다. " +
                        $"[대미지 : {(int)((_dataManager.Player.GetDamage() * 1.5f) - monster.Def)}]");
                }
                else
                {
                    Console.WriteLine($"Lv.{monster.Level} {monster.NameID}을(를) 맞췄습니다. " +
                        $"[대미지 : {(int)(_dataManager.Player.GetDamage() - monster.Def)}]");
                }
            }
            Thread.Sleep(600);

            enter();
            Console.WriteLine($"Lv.{monster.Level} {monster.NameID}");
            Console.WriteLine($"HP {monster.CurrentHP}");
            Thread.Sleep(600);

            enter();
            if (monster.CurrentHP > 0 && !playerDodge)
            {
                Console.WriteLine($"{monster.NameID}의 공격");
                Thread.Sleep(600);
                if (mobCrit)
                {
                    Console.WriteLine("치명적인 공격을 받았습니다!");
                    Console.WriteLine($"[받은 대미지 : {(int)((monster.Att * 1.5f) - (_dataManager.Player.Def + _dataManager.Player.ExtraDef))}]");
                }
                else Console.WriteLine($"[받은 대미지 : {(int)(monster.Att - (_dataManager.Player.Def + _dataManager.Player.ExtraDef))}]");
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
                BattleSelectScene.remainingMonster--;
            }

            int key;
            int.TryParse(Console.ReadLine(), out key);
            while (key != 0)
            {
                Console.WriteLine("잘못 입력하셨습니다.");
                Thread.Sleep(300);
            }
            _dataManager.InputMemory.InputComplete = true;
            _dataManager.InputMemory.PreInput = 1;
        }
    }
}
