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
    public int MaxExp { get; set; }
    public float Att { get; set; }
    public float Def { get; set; }
    public float Critical { get; set; }
    public float Dodge { get; set; }
    public List<Skill> Skills { get; set; } = new List<Skill>();
    public int Gold { get; set; } = 1500;
}