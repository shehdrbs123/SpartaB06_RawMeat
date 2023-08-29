using BasicTeamProject.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading.Tasks.Dataflow;


namespace BasicTeamProject.Scene
{
    /// <summary>
    /// 작업자 : 노동균
    /// 클래스역할 : Job을 선택하고 유저캐릭터에 직업에 맞게 데이터를 추가 해주는 화면입니다
    /// </summary>
    internal class JobSelectScene : Scene
    {
        protected override void SetFunctionList()
        {
            _FunctionList.Add("MainScene");
        }

        protected override void WriteView()
        {
            Console.WriteLine("스파르타 던전에 오신 여러분 환영합니다");
        }

        protected override void afterOperate()
        {
            base.afterOperate();
            Console.WriteLine("직업을 입력해 주세요");
            Console.WriteLine("1.전사, 2.도적, 3.마법사");
            int job = int.Parse(Console.ReadLine());
            Player tempUser = _dataManager.Player;
            tempUser.MaxExp = _dataManager.GetMaxExp(tempUser.Level);
            
            switch(job)
            {
                case 1:
                    tempUser.job = Data.Player.Job.전사;
                    tempUser.MaxHp = 100;
                    tempUser.MaxMp = 50;
                    tempUser.Att = 5;
                    tempUser.Def = 10;
                    tempUser.Critical = 5;
                    tempUser.Dodge = 10;
                    tempUser.Skills.Add(_dataManager.CreateSkill("강한공격"));
                    tempUser.Skills.Add(_dataManager.CreateSkill("방어모드"));

                    break;
                case 2:
                    tempUser.job = Data.Player.Job.도적;
                    tempUser.MaxHp = 80;
                    tempUser.MaxMp = 80;
                    tempUser.Att = 3;
                    tempUser.Def = 7;
                    tempUser.Critical = 40;
                    tempUser.Dodge = 50;
                    tempUser.Skills.Add(_dataManager.CreateSkill("공허돌진"));
                    tempUser.Skills.Add(_dataManager.CreateSkill("강한대포"));
                    break;
                case 3:
                    tempUser.job = Data.Player.Job.마법사;
                    tempUser.MaxHp = 100;
                    tempUser.MaxMp = 50;
                    tempUser.Att = 5;
                    tempUser.Def = 10;
                    tempUser.Critical = 5;
                    tempUser.Dodge = 10;
                    tempUser.Skills.Add(_dataManager.CreateSkill("브레스"));
                    tempUser.Skills.Add(_dataManager.CreateSkill("꼬리치기"));
                    break;
            }
            _dataManager.InputMemory.InputComplete = true;
            _dataManager.InputMemory.PreInput = 1;
        }
    }
}
