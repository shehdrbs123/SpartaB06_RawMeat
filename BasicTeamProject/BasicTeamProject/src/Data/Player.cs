using System;
using static BasicTeamProject.Data.Player;

namespace BasicTeamProject.Data;

public class Player : ISkillStatus
{
    public enum Job
    {
        전사 = 0, 도적, 마법사
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
    public int MaxHP { get; set; }
    public int MaxMP { get; set; }
    public float Att { get; set; }
    public float Def { get; set; }
    public float Critical { get; set; }
    public float Dodge { get; set; }
    public int ExtraMaxHP { get; set; } = 0;
    public int ExtraMaxMP { get; set; } = 0;
    public float ExtraAtt { get; set; } = 0;
    public float ExtraDef { get; set; } = 0;
    public float ExtraCritical { get; set; } = 0;
    public float ExtraDodge { get; set; } = 0;
    public List<Skill> Skills { get; set; } = new List<Skill>();
    public int Gold { get; set; } = 1500;

    public void Setting(PlayerPasingData Data)
    {
        job = Data.job;
        CurrentHP = Data.MaxHP;
        MaxHP = Data.MaxHP;
        CurrentMP = Data.MaxMP;
        MaxMP = Data.MaxMP;
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
        Console.WriteLine($"체 력 : {CurrentHP} / {MaxHP}" + ((ExtraMaxHP != 0) ? $" ({ExtraMaxHP:+0;-#;0})" : ""));
        Console.WriteLine($"마 력 : {CurrentMP} / {MaxMP}" + ((ExtraMaxMP != 0) ? $" ({ExtraMaxMP:+0;-#;0})" : ""));

        Console.WriteLine($"Exp : {CurrentExp}");
        Console.WriteLine();
        Console.WriteLine($"공격력 : {Att}" + ((ExtraAtt != 0) ? $" ({ExtraAtt:+0;-#;0})" : ""));
        Console.WriteLine($"방어력 : {Def}" + ((ExtraDef != 0) ? $" ({ExtraDef:+0;-#;0})" : ""));
        Console.WriteLine($"치명타 : {Critical}" + ((ExtraCritical != 0) ? $" ({ExtraCritical:+0;-#;0})" : ""));
        Console.WriteLine($"회피율 : {Dodge}" + ((ExtraDodge != 0) ? $" ({ExtraDodge:+0;-#;0})" : ""));
        Console.WriteLine();
        Console.WriteLine("[소지금]");
        Console.WriteLine($"Gold : {Gold}");
    }

    public void ToggleEquip(Item item)
    {
        if (item.IsEquipped)
        {
            item.IsEquipped = false;
            ExtraAtt -= item.Att;
            ExtraCritical -= item.Critical;
            ExtraDef -= item.Def;
            ExtraDodge -= item.Dodge;
            ExtraMaxHP -= item.MaxHP;
            ExtraMaxMP -= item.MaxMP;
        }
        else
        {
            item.IsEquipped = true;
            ExtraAtt += item.Att;
            ExtraCritical += item.Critical;
            ExtraDef += item.Def;
            ExtraDodge += item.Dodge;
            ExtraMaxHP += item.MaxHP;
            ExtraMaxMP += item.MaxMP;
        }
    }

    public void TurnCheck()
    {
        foreach(Skill skill in Skills)
        {
            if(skill.TurnCheck(this))
            {
                //이때 버프가 끝난것 따로 뭔가..넣나..
                Console.WriteLine($"{skill.NameID} 지속시간이 끝났다!");
            }
        }
    }
    public int UseSkill(int Index)
    {
       int check =  Skills[Index - 1].UseSkill(this);
        if (check == -999)
        {
            Console.WriteLine("뭔가문제");
            return 0;
        }

        if (check == -1)
            Console.WriteLine("쿨타임");
        else if (check == -2)
            Console.WriteLine("마나부족");
        else if (check == -3)
            Console.WriteLine("버프사용");
        else
            return check;

        return 0;

    }
}

public struct PlayerPasingData
{
    public Job job;
    public int MaxHP;
    public int MaxMP;
    public float Att;
    public float Def;
    public float Critical;
    public float Dodge;
    public List<Skill> Skills;
}
