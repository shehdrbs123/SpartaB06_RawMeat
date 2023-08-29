using BasicTeamProject.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading.Tasks.Dataflow;

namespace BasicTeamProject.Scene
{
    internal class JobSelectScene : Scene
    {
        protected override void SetFunctionList()
        {
           
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
            tempUser.Level = 1;
            tempUser.Exp = _dataManager.GetMaxExp(tempUser.Level);

            switch(job)
            {
                case 1:
                    tempUser.job = Data.Player.Job.전사;
                    
                    break;
                case 2:
                    break;
                case 3:
                    break;
            }
            _dataManager.Player.NameID = name;
            _dataManager.InputMemory.InputComplete = true;
            _dataManager.InputMemory.preInput = 1;
        }
    }
}
