using BasicTeamProject.Data;

namespace BasicTeamProject.Scene;

public class InventoryConsumeScene : Scene
{
    private int itemCount;
    protected override void WriteView()
    {
        Console.WriteLine("인벤토리 - 소비 관리");
        Console.WriteLine("보유 중인 소비아이템을 사용할 수 있습니다.");
        enter(); enter();
        Console.WriteLine("[플레이어 상태]");
        _dataManager.Player.ShowAllInfo();
        enter(); enter();
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
        TypeOfAbility ability = TypeOfAbility.CurrentHP;
        int abilityValue = 0;
        int key=0;
        int count = 1;
        
        while (!_dataManager.InputMemory.TryGetKey(0, itemCount + 1, out key))
        {
            Console.WriteLine("잘못 입력하셨습니다");
            Console.Write(">>");
        }

        Item healItem = _dataManager.Inventory.GetItem(key, ItemType.Consumable);

       
        healItem.GetItemAbility(out ability, out abilityValue);
        
        HealScene.AbilityValue = abilityValue;
        HealScene.TypeOfAbility = ability;

        _dataManager.Inventory.DeleteItem(key, ref count, ItemType.Consumable);

        int beforeAddValue = 0;
        int addedValue = 0;
        _dataManager.Player.AddStatus(ability,abilityValue,out addedValue,out beforeAddValue);
        Console.WriteLine("아이템을 사용하였습니다");

        _dataManager.InputMemory.InputComplete = true;
        _dataManager.InputMemory.PreInput = 1;

    }

    protected override void SetFunctionList()
    {
        _FunctionList.Add("InventoryScene");
    }
}