﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BasicTeamProject.Scene
{
    public class ShopScene : Scene
    {
        protected override void SetFunctionList()
        {
            _FunctionList.Add("ShopBuyScene");
            _FunctionList.Add("ShopInvenEquipScene");
            _FunctionList.Add("ShopInvenConsumableScene");
            _FunctionList.Add("ShopInvenAllScene");
        }
        protected override void PreOperate()
        {
            base.PreOperate();
            _dataManager.InputMemory.SetRange(0, _FunctionList.Count);
        }

        protected override void WriteView()
        {
            Console.WriteLine("스파르타 상점에 오신 여러분 환영합니다.");
            enter();
            Console.WriteLine("1. 구매 하기");
            Console.WriteLine("2. 장비창 보기");
            Console.WriteLine("3. 소비창 보기");
            Console.WriteLine("4. 전체 보기");
            enter();
            Console.WriteLine("0. 나가기");
            enter();
        }

        protected override void afterOperate()
        {
            base.afterOperate();
        }
    }
}
