using BasicTeamProject.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BasicTeamProject.Scene
{
    public  class LoadScene : Scene
    {
        protected override void SetFunctionList()
        {
            _FunctionList.Add("MainScene");
            _FunctionList.Add("CreateUserScene");
        }

        protected override void WriteView()
        {
            _dataManager.InputMemory.InputComplete = true;
            if (!_dataManager.DataLoad())
            {
                Console.WriteLine("데이타가 없습니다");
                Console.ReadLine();
                _dataManager.InputMemory.PreInput = 2;
                return;
            }
            Console.WriteLine("불러오기가 완료되었습니다.");
            Console.ReadLine();
            _dataManager.InputMemory.PreInput = 1;
        }
    }
}
