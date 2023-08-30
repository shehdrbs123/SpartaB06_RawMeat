using BasicTeamProject.Data;
namespace BasicTeamProject.Scene;

public class HealScene : Scene
{
    public static int HealPoint = 100;
    private const int DefaultHealPoint = 100;
    protected override void WriteView()
    {
        Player player = _dataManager.Player; 
        int preHP = _dataManager.Player.CurrentHP;
        Console.Write("회복 중");
        for (int i = 0; i < 3; ++i)
        {
            Thread.Sleep(1000);
            Console.Write(".");
        }
        enter();

        player.CurrentHP = Math.Clamp(player.CurrentHP + HealPoint, 0, player.MaxHP);

        enter();
        Console.WriteLine("회복 되었습니다.");
        enter();
        Console.WriteLine($"회복량 {HealPoint}");
        Console.WriteLine($"{preHP} -> {_dataManager.Player.CurrentHP}");
        Thread.Sleep(2000);
    }

    protected override void afterOperate()
    {
        base.afterOperate();
        HealPoint = DefaultHealPoint;
        _dataManager.InputMemory.InputComplete = true;
        _dataManager.InputMemory.PreInput = 1;
    }

    protected override void SetFunctionList()
    {
        _FunctionList.Add("HospitalScene");
    }
}