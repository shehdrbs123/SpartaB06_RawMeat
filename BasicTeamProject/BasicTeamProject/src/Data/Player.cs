using static BasicTeamProject.Data.Player;

namespace BasicTeamProject.Data;

public class Player
{
    public enum Job
    {
        전사=0,도적,마법사
    }
    public Player()
    {
        
    }
    public string NameID { get; set; }
    public Job job { get; set; }
    public int Level { get; set; } = 1;
    public int CurrentHP { get; set; } = 0;
    public int CurrentMP { get; set; } = 0;
    public int CurrentExp { get; set; } = 0;
    public int MaxHp { get; set; }
    public int MaxMp { get; set; }
    public float Att { get; set; }
    public float Def { get; set; }
    public float Critical { get; set; }
    public float Dodge { get; set; }
    public List<Skill> Skills { get; set; } = new List<Skill>();
    public int Gold { get; set; } = 1500;

    public void Setting(PlayerPasingData Data)
    {
        job = Data.job;
        CurrentHP = Data.MaxHp;
        MaxHp = Data.MaxHp;
        CurrentMP = Data.MaxMp;
        MaxMp = Data.MaxMp;
        Att = Data.Att;
        Def = Data.Def;
        Critical = Data.Critical;
        Dodge = Data.Dodge;
    }

    public void ShowAllInfo()
    {
        Console.WriteLine("[캐릭터 정보]");
        Console.WriteLine($"이름 : {NameID}");
        Console.WriteLine($"Lv. {Level,2}");
        Console.WriteLine($"Chad ( {Enum.GetNames<Job>()[(int)job]} )");
        Console.WriteLine($"공격력 : {Att}");
        Console.WriteLine($"방어력 : {Def}");
        Console.WriteLine($"체 력 : {CurrentHP}/{MaxHp}");
        Console.WriteLine($"Gold : {Gold}");
        Console.WriteLine($"Exp : {CurrentExp}");
    }
}

public struct PlayerPasingData
{
    public Job job;
    public int MaxHp;
    public int MaxMp;
    public float Att;
    public float Def;
    public float Critical;
    public float Dodge;
    public List<Skill> Skills;
}
