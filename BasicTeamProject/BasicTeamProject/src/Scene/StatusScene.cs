using System.Transactions;

namespace BasicTeamProject.Scene;

public class StatusScene : Scene
{
    protected override void SetFunctionList()
    {
        FunctionList.Add("MainScene");
    }

    protected override void WriteView()
    {
        Console.WriteLine("상태 보기");
        Console.WriteLine("캐릭터의 정보가 표시됩니다.");
        enter();
        //캐릭터 상태 가져와서 보여주기
        
        enter();
        
        Console.WriteLine("1. 나가기");
        enter();

    }
}