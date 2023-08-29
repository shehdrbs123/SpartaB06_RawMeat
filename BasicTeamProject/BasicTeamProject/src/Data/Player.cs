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
    public int Level { get; set; }
    public int MaxHp { get; set; }
    public int MaxMp { get; set; }
    public float Att { get; set; }
    public float Def { get; set; }
    public float Critical { get; set; }
    public float Dodge { get; set; }
    public int Exp { get; set; }
    public List<Skill> Skills { get; set; } = new List<Skill>();

    public int Gold { get; set; }
}