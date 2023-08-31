using BasicTeamProject.src;
using System;
using System.Net.Http.Headers;
using System.Reflection;
using static BasicTeamProject.Data.Player;
using static System.Runtime.InteropServices.JavaScript.JSType;

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
    public bool Attack = false;

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

    public bool GetSkillTargetAble()
    {
        if (CurrentSkill > 0)
        {
            return (!Skills[CurrentSkill - 1].isBuff && CurrentMP >= Skills[CurrentSkill - 1].MP && Skills[CurrentSkill - 1].CoolTime == 0);
        }

        return false;
    }
    public bool GetSkillTargetAble(out bool wide)
    {
        wide = false;
        if (CurrentSkill > 0)
        {
            wide = Skills[CurrentSkill - 1].isWide;
            return (!Skills[CurrentSkill - 1].isBuff && CurrentMP >= Skills[CurrentSkill - 1].MP && Skills[CurrentSkill - 1].CoolTime == 0);
        }

        return false;
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

    public void LevelUP()
    {
        Console.WriteLine("레벨 업!");
        CurrentExp -= DataManager.Instance.GetMaxExp();
        Level++;
        MaxHP += 30;
        MaxMP += 10;
        CurrentHP = MaxHP;
        CurrentMP = MaxMP;
        Att += 2;
        Def += 1;
        Critical += 3;
        Dodge += 1;
    }

    public void TurnCheck()
    {
        foreach (Skill skill in Skills)
        {
           skill.TurnCheck(this);
        }
    }
    public int UseSkill(int Index, out bool wide)
    {
        int check = Skills[Index - 1].UseSkill(this, out wide);
        if (check == -999)
        {
            Console.WriteLine("뭔가문제");
            return 0;
        }
        return check;

    }
    public bool PlayerAct(out int damage, out bool wide)
    {
        wide = false;
        if (CurrentSkill > 0)
        {
            if(Skills[CurrentSkill - 1].GetSkillAble(this))
                Console.WriteLine($"{Skills[CurrentSkill - 1].NameID}!!!!!");

            damage = UseSkill(CurrentSkill, out wide);
            if (damage > 0)
            {
                CurrentSkill = -1;
                return true;
            }
            else 
            {
                if (damage == -1)
                    Console.WriteLine($"쿨타임이다! {Skills[CurrentSkill - 1].CoolTime}턴 남음");
                else if (damage == -2)
                    Console.WriteLine($"마나가 부족하다! {Skills[CurrentSkill - 1].MP}필요");
                else if (damage == -4)
                    Console.WriteLine($"체력이 부족하다! {Skills[CurrentSkill - 1].HP}필요");

                CurrentSkill = -1;
                return false;
            }
        }
        else if (Attack)
        {
            Attack = false;
            Console.WriteLine("이얍!");
            Thread.Sleep(600);
            damage = (int)(Att);
            return true;
        }
        damage = 0;
        return false;
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
                Console.WriteLine($"HP {MathF.Min(MaxHP - CurrentHP, abilityValue)} 회복");
                CurrentHP = Math.Clamp(CurrentHP + abilityValue, 1, MaxHP);
                addedValue = CurrentHP;
                break;
            case TypeOfAbility.CurrentMP :
                beforeAddValue = CurrentMP;
                Console.WriteLine($"MP {MathF.Min(MaxMP - CurrentMP, abilityValue)} 회복");
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

    public void ShowBattleInfo()
    {
        List<string> list = new List<string>();
        Tuple<int, int, int> HPPaintRange = new Tuple<int, int, int>(5, 4, 24);
        Tuple<int, int, int> MPPaintRange = new Tuple<int, int, int>(6, 4, 24);
        int HPRate = (int)((double)CurrentHP / MaxHP * 24);
        int MPRate = (int)((double)(CurrentMP / MaxMP * 24));

        list.Add("┌──────────────────────────────┐");
        string name = $"Lv.{Level}  {NameID}  ({job})";
        string nameLine = name.PadLeft((int)(15 + (name.Length - StringCounter.GetStringLength(NameID + job)) / 2));
        nameLine = nameLine.PadRight(30 - StringCounter.GetStringLength(NameID + job));
        list.Add("│" + nameLine + "│");
        list.Add("│───────────(능력치)───────────│");
        list.Add("│                              │");
        string stat = $" 공격력 {Att,4} / 방어력 {Def,4}";
        string statLine = stat.PadLeft((int)(15 + (stat.Length - 6) / 2));
        statLine = statLine.PadRight(24);
        list.Add("│" + statLine + "│");
        string HPstr = $"HP {CurrentHP,4} / {MaxHP,4}";
        string HPLine = HPstr.PadLeft((int)(15 + HPstr.Length / 2));
        HPLine = HPLine.PadRight(30);
        list.Add("│" + HPLine + "│");
        string MPstr = $"MP {CurrentMP,4} / {MaxMP,4}";
        string MPLine = MPstr.PadLeft((int)(15 + MPstr.Length / 2));
        MPLine = MPLine.PadRight(30);
        list.Add("│" + MPLine + "│");
        list.Add("│                              │");
        list.Add("└──────────────────────────────┘");

        for (int i = 0; i < list.Count; i++)
        {
            for (int j = 0; j < list[i].Length; j++)
            {
                if(i == HPPaintRange.Item1)
                {
                    Console.BackgroundColor = ConsoleColor.Black;
                    Console.ForegroundColor = ConsoleColor.White;
                    if (HPPaintRange.Item2 <= j && j <= HPPaintRange.Item2 + HPPaintRange.Item3)
                    {
                        if(j - HPPaintRange.Item2 <= HPRate)
                        {
                            Console.BackgroundColor = ConsoleColor.Red;
                            Console.ForegroundColor = ConsoleColor.Black;
                        }
                    }
                }
                if (i == MPPaintRange.Item1)
                {
                    Console.BackgroundColor = ConsoleColor.Black;
                    Console.ForegroundColor = ConsoleColor.White;
                    if (MPPaintRange.Item2 <= j && j <= MPPaintRange.Item2 + MPPaintRange.Item3)
                    {
                        if (j - MPPaintRange.Item2 <= MPRate)
                        {
                            Console.BackgroundColor = ConsoleColor.Blue;
                            Console.ForegroundColor = ConsoleColor.Black;
                        }
                    }
                }
                Console.Write(list[i][j]);
            }
            Console.WriteLine();
        }
        Console.WriteLine();
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
