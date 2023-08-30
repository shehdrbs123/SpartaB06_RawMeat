using BasicTeamProject.Data;

namespace BasicTeamProject.Scene;

public class HospitalScene : Scene
{
    private const int price = 500;
    protected override void WriteView()
    { 
        Console.WriteLine("치료소에 어서 오십시오");
        Console.WriteLine("무엇을 하시겠습니까?");
        enter();
        
        Console.WriteLine("[소지금]");
        Console.WriteLine($"{_dataManager.Player.Gold} G");
        enter();
        
        Console.WriteLine($"1. 치료받기 : {price} G");
        enter();
        Console.WriteLine("0.나가기");
        
    }

    protected override void PreOperate()
    {
        base.PreOperate();
        _dataManager.InputMemory.SetRange(0,_FunctionList.Count);
        
    }

    protected override void SetFunctionList()
    {
        _FunctionList.Add("HealScene");
        _FunctionList.Add("HospitalScene");
    }

    protected override void afterOperate()
    {
        base.afterOperate();
        Player player = _dataManager.Player;
        int key = 0;

        EndView();
        while (!_dataManager.InputMemory.TryGetKey(out key))
        {
            Console.WriteLine("잘못 입력하셨습니다");
            Console.Write(">>");
        }
        
        _dataManager.InputMemory.InputComplete = true;
        if (key == 1)
        {
            if (player.Gold < price)
            {
                _dataManager.InputMemory.PreInput = 2;
                Console.WriteLine("돈이 부족합니다");
                Thread.Sleep(2000);                
            }
        }
    }
}