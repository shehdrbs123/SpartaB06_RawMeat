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
    public int CurrentSkill { get; set; } = -1;
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
        foreach (Skill skill in Data.Skills)
        {
            Skills.Add(new Skill(skill));
        }
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
            Att -= item.Att;
            ExtraAtt -= item.Att;
            Critical -= item.Critical;
            ExtraCritical -= item.Critical;
            Def -= item.Def;
            ExtraDef -= item.Def;
            Dodge -= item.Dodge;
            ExtraDodge -= item.Dodge;
            MaxHP -= item.MaxHP;
            ExtraMaxHP -= item.MaxHP;
            MaxMP -= item.MaxMP;
            ExtraMaxMP -= item.MaxMP;
            CurrentHP = Math.Clamp(CurrentHP, 0, MaxHP);
        }
        else
        {
            item.IsEquipped = true;
            Att += item.Att;
            ExtraAtt += item.Att;
            Critical += item.Critical;
            ExtraCritical += item.Critical;
            Def += item.Def;
            ExtraDef += item.Def;
            Dodge += item.Dodge;
            ExtraDodge += item.Dodge;
            MaxHP += item.MaxHP;
            ExtraMaxHP += item.MaxHP;
            MaxMP += item.MaxMP;
            ExtraMaxMP += item.MaxMP;
        }
    }

    public void TurnCheck()
    {
        foreach (Skill skill in Skills)
        {
            if (skill.TurnCheck(this))
            {
                //이때 버프가 끝난것 따로 뭔가..넣나..
                Console.WriteLine($"{skill.NameID} 지속시간이 끝났다!");
            }
        }
    }
    public int UseSkill(int Index)
    {
        int check = Skills[Index - 1].UseSkill(this);
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
    public bool PlayerAct(out int damage)
    {
        if (CurrentSkill > 0)
        {
            damage = UseSkill(CurrentSkill);
            CurrentSkill = -1;
            if (damage > 0)
            {
                Console.WriteLine($"{Skills[CurrentSkill].NameID}!!!!!");
                Thread.Sleep(600);
                return true;
            }
            else if (damage == -1)
            {
                Console.WriteLine($"쿨타임이다! {Skills[CurrentSkill].CoolTime}턴 남음");
                Thread.Sleep(600);
                return false;
            }
            else if (damage == -2)
            {
                Console.WriteLine($"마나가 부족하다! {Skills[CurrentSkill].MP}필요");
                Thread.Sleep(600);
                return false;
            }
            else if (damage == -3)
            {
                Console.WriteLine($"{Skills[CurrentSkill].NameID}!!!!!");
                Thread.Sleep(600);
                return false;
            }
        }
        else
        {
            Console.WriteLine("이얍!");
            Thread.Sleep(600);
            damage = (int)(Att);
        }
        return true;
    }

    public void EndDungeon()
    {
        foreach (Skill skill in Skills)
        {
            skill.ResetSkill(this);
        }
    }

    public void AddStatus(TypeOfAbility ability, int abilityValue, out int addedValue, out int beforeAddValue)
    {
        addedValue = 0;
        beforeAddValue = 0;
        switch (ability)
        {
            case TypeOfAbility.CurrentHP :
                beforeAddValue = CurrentHP;
                CurrentHP = Math.Clamp(CurrentHP + abilityValue, 1, MaxHP);
                addedValue = CurrentHP;
                break;
            case TypeOfAbility.CurrentMP :
                beforeAddValue = CurrentMP;
                CurrentMP = Math.Clamp(CurrentMP + abilityValue, 1, MaxMP);
                addedValue = CurrentMP;
                break;
            case TypeOfAbility.MaxHP :
                beforeAddValue = MaxHP;
                MaxHP += abilityValue;
                addedValue = MaxHP;
                break;
            case TypeOfAbility.MaxMP :
                beforeAddValue = MaxMP;
                MaxMP += abilityValue;
                addedValue = MaxMP;
                break;
            case TypeOfAbility.Att:
                beforeAddValue = (int)Att;
                Att += abilityValue;
                addedValue = (int)Att;
                break;
            case TypeOfAbility.Critical:
                beforeAddValue = (int)Critical;
                Critical += abilityValue;
                addedValue = (int)Critical;
                break;
            case TypeOfAbility.Def:
                beforeAddValue = (int)Def;
                Def += abilityValue;
                addedValue = (int)Def;
                break;
            case TypeOfAbility.Dodge :
                beforeAddValue = (int)Dodge;
                Dodge += abilityValue;
                addedValue = (int)Dodge;
                addedValue = (int)Dodge;
                break;
            case TypeOfAbility.Exp :
                beforeAddValue = CurrentExp;
                CurrentExp += abilityValue;
                addedValue = CurrentExp;
                break;
        }
    }

    public string GetSaveData()
    {
        string str = NameID + "|" + job.ToString() + "|" +
                Level.ToString() + "|" + CurrentHP.ToString() + "|" +
                CurrentMP.ToString() + "|" + CurrentExp.ToString() + "|" +
                MaxHP.ToString() + "|" + MaxMP.ToString() + "|" +
                Att.ToString() + "|" + Def.ToString() + "|" +
                Critical.ToString() + "|" + Dodge.ToString() + "|" +
                ExtraMaxHP.ToString() + "|" + ExtraMaxMP.ToString() + "|" +
                ExtraAtt.ToString() + "|" + ExtraDef.ToString() + "|" +
                ExtraCritical.ToString() + "|" + ExtraDodge.ToString() + "|" +
                Gold.ToString() + "|" + Skills.Count.ToString() + "|";
        for (int i = 0; i < Skills.Count; ++i)
        {
            str += Skills[i].NameID + "|";
        }
        str = str.Remove(str.Length - 1);
        return str;
    }
    public void SetData(string[] data)
    {
        NameID = data[0];
        job = (Job)Enum.Parse(typeof(Job), data[1]);
        Level = int.Parse(data[2]);
        CurrentHP = int.Parse(data[3]);
        CurrentMP = int.Parse(data[4]);
        CurrentExp = int.Parse(data[5]);
        MaxHP = int.Parse(data[6]);
        MaxMP = int.Parse(data[7]);
        Att = int.Parse(data[8]);
        Def = int.Parse(data[9]);
        Critical = int.Parse(data[10]);
        Dodge = int.Parse(data[11]);
        ExtraMaxHP = int.Parse(data[12]);
        ExtraMaxMP = int.Parse(data[13]);
        ExtraAtt = int.Parse(data[14]);
        ExtraDef = int.Parse(data[15]);
        ExtraCritical = int.Parse(data[16]);
        ExtraDodge = int.Parse(data[17]);
        Gold = int.Parse(data[18]);
        int count = int.Parse(data[19]);
        Skills.Clear();
        for (int i = 0; i < count; i++)
        {
            Skills.Add(new Skill(DataManager.Instance.CreateSkill(data[20+i])));
        }
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
