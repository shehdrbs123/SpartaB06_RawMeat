﻿using BasicTeamProject.Data;
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
            _FunctionList.Add("BattleSelectScene");
        }

        protected override void PreOperate()
        {
            base.PreOperate();
            if (_dataManager.Monsters.Count == 0)
            {
                _dataManager.CreateDungeon(1);
            }
        }

        protected override void WriteView()
        {
            Console.WriteLine("전투 시작");
            enter();
            foreach (var monster in _dataManager.Monsters)
            {
                if (monster.CurrentHp <= 0) continue;
                Console.WriteLine($"Lv.{monster.Level} {monster.NameID} HP: {monster.MaxHp}");
            }
            enter();

            Console.WriteLine("[플레이어 정보]");
            enter();
            Console.WriteLine($"Lv.{_dataManager.Player.Level} {_dataManager.Player.NameID}" +
                $"({_dataManager.Player.job})\nHP: {_dataManager.Player.CurrentHP}");

            enter();
            Console.WriteLine("0. 나가기");
            Console.WriteLine("1. 공격");
            enter();
        }
    }
}
