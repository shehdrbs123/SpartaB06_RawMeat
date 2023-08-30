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
            Console.WriteLine("스파르타 던전에 오신 여러분 환영합니다.");
        }

        protected override void afterOperate()
        {
            base.afterOperate();
            Console.WriteLine("직업을 입력해 주세요.");
            Console.WriteLine("1.전사, 2.도적, 3.마법사");
            Console.Write(">>");
            int job;
            while (!_dataManager.InputMemory.TryGetKey(1,4,out job))
            {
                Console.WriteLine("잘못 입력하셨습니다.");
                Console.Write(">>");
            }
            
            Player tempUser = _dataManager.Player;
            _dataManager.PlayerSetting(job);

            _dataManager.InputMemory.InputComplete = true;
            _dataManager.InputMemory.PreInput = 1;
        }
    }
}
