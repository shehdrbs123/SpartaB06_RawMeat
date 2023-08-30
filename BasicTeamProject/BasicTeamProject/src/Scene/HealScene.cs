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

    private void Heal(out int healedValue,out int beforeHealValue)
    {
        Player player = _dataManager.Player;
        healedValue = 0;
        beforeHealValue = 0;
        switch (TypeOfAbility)
        {
            case TypeOfAbility.CurrentHP :
                beforeHealValue = player.CurrentHP;
                player.CurrentHP = Math.Clamp(player.CurrentHP + AbilityValue, 0, player.MaxHP);
                healedValue = player.CurrentHP;
                break;
            case TypeOfAbility.CurrentMP :
                beforeHealValue = player.CurrentMP;
                player.CurrentMP = Math.Clamp(player.CurrentMP + AbilityValue, 0, player.MaxMP);
                healedValue = player.CurrentMP;
                break;
            case TypeOfAbility.MaxHP :
                beforeHealValue = player.MaxHP;
                player.MaxHP += AbilityValue;
                healedValue = player.MaxHP;
                break;
            case TypeOfAbility.MaxMP :
                beforeHealValue = player.MaxMP;
                player.MaxMP += AbilityValue;
                healedValue = player.MaxMP;
                break;
            case TypeOfAbility.Att:
                beforeHealValue = (int)player.Att;
                player.Att += AbilityValue;
                healedValue = (int)player.Att;
                break;
            case TypeOfAbility.Critical:
                beforeHealValue = (int)player.Critical;
                player.Critical += AbilityValue;
                healedValue = (int)player.Critical;
                break;
            case TypeOfAbility.Def:
                beforeHealValue = (int)player.Def;
                player.Def += AbilityValue;
                healedValue = (int)player.Def;
                break;
            case TypeOfAbility.Dodge :
                beforeHealValue = (int)player.Dodge;
                player.Dodge += AbilityValue;
                healedValue = (int)player.Dodge;
                break;
            case TypeOfAbility.Exp :
                beforeHealValue = player.CurrentHP;
                player.CurrentExp += AbilityValue;
                healedValue = player.CurrentExp;
                break;
        }
    }
}