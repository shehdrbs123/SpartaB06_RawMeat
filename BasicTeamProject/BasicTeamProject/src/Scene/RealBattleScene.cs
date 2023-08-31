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
        private int getDamage;
        private bool readyForAttack = false;

        private Monster selectedMonster;
        Random random = new Random();

        protected override void SetFunctionList()
        {
            _FunctionList.Add("BattleScene");
            _FunctionList.Add("RewardScene");
        }

        protected override void PreOperate()
        {
            base.PreOperate();
            _dataManager.InputMemory.SetRange(0, 1);
            preInputNum = BattleSelectScene.selectedMonster;

            int realIndex = -1;
            while(preInputNum != 0)
            {
                realIndex++;
                if (!_dataManager.Monsters[realIndex].isDead)
                {
                    --preInputNum;
                }
            }
            selectedMonster = _dataManager.Monsters[realIndex];
        }

        protected override void WriteView()
        {
            Console.WriteLine("전투 로그");

            enter();
            bool wide;
            readyForAttack = _dataManager.Player.PlayerAct(out getDamage, out wide);

            if (readyForAttack)
            {
                if (wide)
                {
                    foreach(var monster in _dataManager.Monsters)
                    {
                        if (monster.isDead)
                            continue;
                        if (random.Next(0, 101) > monster.Dodge)
                        {
                            if (random.Next(0, 101) < _dataManager.Player.Critical + _dataManager.Player.ExtraCritical)
                            {
                                Console.WriteLine("치명타 적중!");
                                Console.WriteLine($"{monster.NameID}을(를) 맞췄습니다. " +
                                    $"[대미지 : {Math.Clamp((int)((getDamage * 1.5f) - monster.Def), 0, int.MaxValue)}]");
                                monster.CurrentHP -= Math.Clamp((int)((getDamage * 1.5f) - monster.Def), 0, int.MaxValue);
                            }
                            else
                            {
                                Console.WriteLine($"{monster.NameID}을(를) 맞췄습니다. " +
                                    $"[대미지 : {Math.Clamp((int)(getDamage - monster.Def), 0, int.MaxValue)}]");
                                monster.CurrentHP -= Math.Clamp((int)(getDamage - monster.Def), 0, int.MaxValue);
                            }
                        }
                        else
                        {
                            Console.WriteLine("공격이 빗나갔습니다");
                        }
                    }
                }
                else
                {
                    if (random.Next(0, 101) > selectedMonster.Dodge)
                    {
                        if (random.Next(0, 101) < _dataManager.Player.Critical + _dataManager.Player.ExtraCritical)
                        {
                            Console.WriteLine("치명타 적중!");
                            Console.WriteLine($"{selectedMonster.NameID}을(를) 맞췄습니다. " +
                                $"[대미지 : {Math.Clamp((int)((getDamage * 1.5f) - selectedMonster.Def), 0, int.MaxValue)}]");
                            selectedMonster.CurrentHP -= Math.Clamp((int)((getDamage * 1.5f) - selectedMonster.Def), 0, int.MaxValue);
                        }
                        else
                        {
                            Console.WriteLine($"{selectedMonster.NameID}을(를) 맞췄습니다. " +
                                $"[대미지 : {Math.Clamp((int)(getDamage - selectedMonster.Def), 0, int.MaxValue)}]");
                            selectedMonster.CurrentHP -= Math.Clamp((int)(getDamage - selectedMonster.Def), 0, int.MaxValue);
                        }
                    }
                    else
                    {
                        Console.WriteLine("공격이 빗나갔습니다");
                    }
                }
            }
            _dataManager.Player.TurnCheck();
            Thread.Sleep(500);
            enter();


            foreach (var monster in _dataManager.Monsters)
            {
                if (monster.CurrentHP <= 0)
                {
                    continue;
                }

                int damage;
                bool monsterAttackCheck = monster.MonsterAct(out damage, out wide);

                if (monsterAttackCheck)
                {
                    if (random.Next(0, 101) > _dataManager.Player.Dodge)
                    {
                        Console.WriteLine($"{monster.NameID}의 공격");

                        if (random.Next(0, 101) < monster.Critical)
                        {
                            Console.WriteLine("치명적인 공격을 받았습니다!");
                            Console.WriteLine($"[받은 대미지 : {Math.Clamp((int)((damage * 1.5f) - (_dataManager.Player.Def + _dataManager.Player.ExtraDef)), 0, int.MaxValue)}]");
                            _dataManager.Player.CurrentHP -= Math.Clamp((int)((damage * 1.5f) - (_dataManager.Player.Def + _dataManager.Player.ExtraDef)), 0, int.MaxValue);
                        }
                        else
                        {
                            Console.WriteLine($"[받은 대미지 : {Math.Clamp((int)(damage - (_dataManager.Player.Def + _dataManager.Player.ExtraDef)), 0, int.MaxValue)}]");
                            _dataManager.Player.CurrentHP -= Math.Clamp((int)(damage - (_dataManager.Player.Def + _dataManager.Player.ExtraDef)), 0, int.MaxValue);
                        }
                    }
                    else
                        Console.WriteLine("회피에 성공했습니다");
                }
                Thread.Sleep(500);
                monster.TurnCheck();

            }

           
            if (_dataManager.Player.CurrentHP > 0)
            {
                enter();
                Console.WriteLine($"Lv.{_dataManager.Player.Level} {_dataManager.Player.NameID} ({_dataManager.Player.job})");
                Console.WriteLine($"HP {_dataManager.Player.CurrentHP}/{_dataManager.Player.MaxHP}");
                Console.WriteLine($"MP {_dataManager.Player.CurrentMP}/{_dataManager.Player.MaxMP}");
                Thread.Sleep(500);
                enter();
            }
        }

        protected override void afterOperate()
        {
            base.afterOperate();
            if (_dataManager.Player.CurrentHP <= 0)
            {
                enter();
                Console.WriteLine("죽었다..");
                enter();
                _dataManager.Player.CurrentHP = _dataManager.Player.MaxHP / 2;
                Console.WriteLine($"{_dataManager.Player.Gold} G -> {_dataManager.Player.Gold /= 2} G");
                Console.WriteLine($"{_dataManager.Player.Gold} EXP -> 0 EXP");
                _dataManager.Player.CurrentExp = 0;
                enter();
                Console.WriteLine("마을로 간다..");
                _dataManager.InputMemory.PreInput = 0;
                _dataManager.InputMemory.InputComplete = true;
                Console.ReadLine();
                return;
            }
            foreach (var monster in _dataManager.Monsters)
            {
                if (monster.CurrentHP <= 0 && !monster.isDead)
                {
                    monster.isDead = true;
                    --BattleSelectScene.remainingMonster;
                }
            }

            if (BattleSelectScene.remainingMonster == 0)
            {
                _dataManager.InputMemory.PreInput = 2;

            }
            else
            {
                _dataManager.InputMemory.PreInput = 1;
            }
            _dataManager.InputMemory.InputComplete = true;

            Console.ReadLine();
        }
    }
}
