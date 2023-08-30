using BasicTeamProject.Data;
namespace BasicTeamProject.Scene;

public class HealScene : Scene
{
    public static int AbilityValue = 100;
    public static TypeOfAbility TypeOfAbility = TypeOfAbility.CurrentHP; 
    private const int DefaultHealPoint = 100;
    private const TypeOfAbility DefaultHealAbility = TypeOfAbility.CurrentHP;

    
    protected override void WriteView()
    {
        int healedValue = 0;
        int beforeHealValue = 0;
        Console.Write("회복 중");
        for (int i = 0; i < 3; ++i)
        {
            Thread.Sleep(1000);
            Console.Write(".");
        }
        enter();

        Heal(out healedValue,out beforeHealValue);

        enter();
        Console.WriteLine("회복 되었습니다.");
        enter();
        Console.WriteLine($"회복량 {AbilityValue}");
        Console.WriteLine($"{beforeHealValue} -> {healedValue}");
        Thread.Sleep(2000);
    }

    protected override void afterOperate()
    {
        base.afterOperate();
        AbilityValue = DefaultHealPoint;
        TypeOfAbility = DefaultHealAbility;
        _dataManager.InputMemory.InputComplete = true;
        _dataManager.InputMemory.PreInput = 1;
        _dataManager.Player.Gold -= 500;
    }

    protected override void SetFunctionList()
    {
        _FunctionList.Add("HospitalScene");
    }
    
    private void Heal(out int AddedValue,out int beforeAddValue)
    {
        Player player = _dataManager.Player;
        player.AddStatus(TypeOfAbility,AbilityValue, out AddedValue, out beforeAddValue);
    }
}