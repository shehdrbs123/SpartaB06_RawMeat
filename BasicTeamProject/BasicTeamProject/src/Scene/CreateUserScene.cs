using BasicTeamProject.Data;
namespace BasicTeamProject.Scene;
public class CreateUserScene : Scene
{
    protected override void WriteView()
    {
        Console.WriteLine("스파르타 던전에 오신 여러분 환영합니다");
        Console.WriteLine("이제 전투를 시작 할 수 있습니다.");
    }
    protected override void afterOperate()
    {
        base.afterOperate();
        Console.Write("이름을 입력해 주세요");
        Console.Write(">>");
        string name = Console.ReadLine();
        _dataManager.Player.NameID = name;
        _dataManager.InputMemory.InputComplete = true;
        _dataManager.InputMemory.preInput = 1;
    }
    protected override void SetFunctionList()
    {
        FunctionList.Add("MainScene");
    }
}
