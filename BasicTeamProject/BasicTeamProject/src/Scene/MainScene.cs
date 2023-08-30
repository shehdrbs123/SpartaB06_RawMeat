namespace BasicTeamProject.Scene;

public class MainScene : Scene
{
    protected override void SetFunctionList()
    {
        _FunctionList.Add("StatusScene");
        _FunctionList.Add("InventoryScene");
        _FunctionList.Add("BattleScene");
        _FunctionList.Add("ShopScene");
        _FunctionList.Add("HospitalScene");
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
        Console.WriteLine("2. 인벤토리");
        Console.WriteLine("3. 전투 시작");
        Console.WriteLine("4. 상점 가기");
        Console.WriteLine("5. 병원 가기");
        Console.WriteLine();        
    }
}   