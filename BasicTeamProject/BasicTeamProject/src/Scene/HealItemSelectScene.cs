using BasicTeamProject.Data;

namespace BasicTeamProject.Scene;

public class HealItemSelectScene : Scene
{
    private int itemCount;
    protected override void WriteView()
    {
        Console.WriteLine("치료소에 어서 오십시오");
        Console.WriteLine("무엇을 하시겠습니까?");
        enter();
        
        Console.WriteLine("[소비 아이템]");
        itemCount = _dataManager.Inventory.ShowItem(ItemType.Consumable);
        enter();
        Console.WriteLine("0. 돌아가기");
        enter();
        Console.WriteLine("어떤 아이템을 복용하시겠습니까?");
        Console.Write(">>");
    }

    protected override void afterOperate()
    {
        base.afterOperate();
        int key;
        while (!_dataManager.InputMemory.TryGetKey(0, itemCount+1, out key))
        {
            Console.WriteLine("잘못 입력하셨습니다");
            Console.Write(">>");
        }

        int count = 1;
        _dataManager.Inventory.DeleteItem(key, ref count, ItemType.Consumable);

        _dataManager.InputMemory.InputComplete = true;
        _dataManager.InputMemory.PreInput = key == 0 ? 1 : 2;
        
    }

    protected override void SetFunctionList()
    {
        _FunctionList.Add("HospitalScene");
        _FunctionList.Add("HealScene");
    }
}