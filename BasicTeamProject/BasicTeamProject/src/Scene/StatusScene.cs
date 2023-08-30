using BasicTeamProject.Data;
using System.Transactions;

namespace BasicTeamProject.Scene;

public class StatusScene : Scene
{
    protected override void SetFunctionList()
    {
        _FunctionList.Add("MainScene");
    }

    protected override void PreOperate()
    {
        base.PreOperate();
        _dataManager.InputMemory.SetRange(1,1);
    }

    protected override void WriteView()
    {
        Console.WriteLine("상태 보기");
        Console.WriteLine("캐릭터의 정보가 표시됩니다.");
        enter();
        //캐릭터 상태 가져와서 보여주기
        _dataManager.Player.ShowAllInfo();
        enter();
        
        Console.WriteLine("1. 나가기");
        enter();

    }
}