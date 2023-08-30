namespace BasicTeamProject.Scene;

public class HospitalScene : Scene
{
    protected override void WriteView()
    { 
        Console.WriteLine("치료소에 어서 오십시오");
        Console.WriteLine("무엇을 하시겠습니까?");
        enter();
        
        Console.WriteLine("[소지금]");
        Console.WriteLine($"{_dataManager.Player.Gold} G");
        enter();
        
        Console.WriteLine("1.치료받기 : 500 G");
        Console.WriteLine("2.회복아이템");
        enter();
        Console.WriteLine("0.나가기");
        
    }

    protected override void PreOperate()
    {
        base.PreOperate();
        _dataManager.InputMemory.SetRange(0,_FunctionList.Count);
        
    }

    protected override void afterOperate()
    {
        base.afterOperate();
        if (_dataManager.InputMemory.PreInput == 1)
        {
            if (_dataManager.Player.Gold < 500)
            {
                _dataManager.InputMemory.InputComplete = true;
                _dataManager.InputMemory.PreInput = 3;
                Console.WriteLine("돈이 부족합니다");
                Thread.Sleep(1000);
            }
            else
            {
                _dataManager.Player.Gold -= 500;
            }
        }
    }

    protected override void SetFunctionList()
    {
        _FunctionList.Add("HealScene");
        _FunctionList.Add("HealItemSelectScene");
        _FunctionList.Add("HospitalScene");
    }
}