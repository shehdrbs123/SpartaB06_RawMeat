using BasicTeamProject.Data;
namespace BasicTeamProject.Scene;
/// <summary>
/// 작업자 : 노동균
/// 클래스 역할 : 유저이름을 입력받아 유저캐릭터에 적용 해주는 클래스
/// </summary>
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
        Console.WriteLine("이름을 입력해 주세요 (1. 불러오기)");
        Console.Write(">>");
        string name = Console.ReadLine();
        _dataManager.Player.NameID = name;
        _dataManager.InputMemory.InputComplete = true;

        _dataManager.InputMemory.PreInput = 1;

        if (name == "1")
        {
            _dataManager.InputMemory.PreInput = 2;
        }
    }
    protected override void SetFunctionList()
    {
        _FunctionList.Add("JobSelectScene");
        _FunctionList.Add("LoadScene");
    }
}
