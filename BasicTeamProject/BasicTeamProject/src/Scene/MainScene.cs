namespace BasicTeamProject.Scene;

public class MainScene : Scene
{
    protected override void SetFunctionList()
    {
        FunctionList.Add("StatusScene");
    }


    protected override void PreOperate()
    {
        base.PreOperate();
    }

    protected override void WriteView()
    {
        Console.WriteLine("스파르타 던전에 오신 여러분 환영합니다");
        Console.WriteLine("이제 전투를 시작 할 수 있습니다.");
        Console.WriteLine();
        Console.WriteLine("1. 상태 보기");
        Console.WriteLine("2. 전투 시작");
        Console.WriteLine();
    }
}