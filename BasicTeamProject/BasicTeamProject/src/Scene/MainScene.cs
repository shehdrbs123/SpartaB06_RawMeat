namespace BasicTeamProject.Scene;

public class MainScene : Scene
{
    protected override void SetFunctionList()
    {
        _FunctionList.Add("StatusScene");
        _FunctionList.Add("InventoryScene");
        _FunctionList.Add("ShopScene");
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
        Console.WriteLine("3. 상점 가기");
        
        // 공격씬 
        
    }
}   