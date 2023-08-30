using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BasicTeamProject.Scene
{
    public class SaveScene : Scene
    {
        protected override void SetFunctionList()
        {
            _FunctionList.Add("MainScene");
        }

        protected override void WriteView()
        {
            _dataManager.DataSave();
            Console.WriteLine("저장이 완료되었습니다.");
            Console.ReadLine();
            _dataManager.InputMemory.InputComplete = true;
            _dataManager.InputMemory.PreInput = 1;
        }
    }
}
