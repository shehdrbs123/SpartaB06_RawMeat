﻿using System.Drawing;
using BasicTeamProject.src;

namespace BasicTeamProject.Data
{
    public class Skill
    {
        public Skill()
        {
        }
        public Skill(Skill skill)
        {
            this.NameID = skill.NameID;
            this.HP = skill.HP;
            this.MP = skill.MP;
            this.ResetCoolTime = skill.ResetCoolTime;
            this.isPer = skill.isPer;
            this.isBuff = skill.isBuff;
            this.Value = skill.Value;
            this.Type = skill.Type;
            this.ResetDuration = skill.ResetDuration;
            this.isWide = skill.isWide;
        }

        public string NameID { get; set; }
        public int HP { get; set; } = 0;
        public int MP { get; set; }
        public int CoolTime { get; set; } = 0;
        public int ResetCoolTime { get; set; }
        public TypeOfAbility Type { get; set; } = TypeOfAbility.End;
        public int Duration { get; set; } = 0;
        public int ResetDuration { get; set; }
        public float Value { get; set; }

        public bool Using { get; set; } = false;
        public bool isPer { get; set; }
        public bool isBuff { get; set; }
        private int Added;
        public bool isWide { get; set; }
        private string GetTypeString()
        {
            if (isBuff)
            {
                switch (Type)
                {
                    case TypeOfAbility.Att:
                        return "공격력";
                    case TypeOfAbility.Def:
                        return "방어력";
                    case TypeOfAbility.MaxHP:
                        return "최대체력";
                    case TypeOfAbility.CurrentHP:
                        return "현재체력";
                    case TypeOfAbility.MaxMP:
                        return "최대마나";
                    case TypeOfAbility.CurrentMP:
                        return "현재마나";
                    case TypeOfAbility.Critical:
                        return "치명타";
                    case TypeOfAbility.Dodge:
                        return "회피";
                }
            }
            else
            {
                switch (Type)
                {
                    case TypeOfAbility.Att:
                        return "(공격력)";
                    case TypeOfAbility.Def:
                        return "(방어력)";
                    case TypeOfAbility.MaxHP:
                        return "(최대체력)";
                    case TypeOfAbility.CurrentHP:
                        return "(현재체력)";
                    case TypeOfAbility.MaxMP:
                        return "(최대마나)";
                    case TypeOfAbility.CurrentMP:
                        return "(현재마나)";
                    case TypeOfAbility.Critical:
                        return "(치명타)";
                    case TypeOfAbility.Dodge:
                        return "(회피)";
                }
            }
            return "";
        }

        private int GetPlayerTypeValue(Player player)
        {
            switch (Type)
            {
                case TypeOfAbility.Att:
                    return (int)player.Att;
                case TypeOfAbility.Def:
                    return (int)player.Def;
                case TypeOfAbility.MaxHP:
                    return (int)player.MaxHP;
                case TypeOfAbility.CurrentHP:
                    return (int)player.CurrentHP;
                case TypeOfAbility.MaxMP:
                    return (int)player.MaxMP;
                case TypeOfAbility.CurrentMP:
                    return (int)player.CurrentMP;
                case TypeOfAbility.Critical:
                    return (int)player.Critical;
                case TypeOfAbility.Dodge:
                    return (int)player.Dodge;
            }
            return 0;
        }

        public void ShowInfo(int index)
        {
            List<String> list = new List<String>();
            string coolTimeGauge = "";
            Player player = DataManager.Instance.Player;
            int playerTypeValue = GetPlayerTypeValue(player);
            string strPlayerTypeValue = playerTypeValue.ToString();
            string strPlayerTypeValueAfter = ((int)((float)playerTypeValue * Value)).ToString();

            
            list.Add("┌─────┬────────────────────────────┐");
            
            //이름 >> 
            int nameNHealthPos;
            
       
            string nameNHealth = $"│     │ {NameID}";
            nameNHealth = nameNHealth.PadRight(20 - StringCounter.GetStringLength(NameID));
            nameNHealthPos = nameNHealth.Length;
            Tuple<int, int, int> PaintRange = new Tuple<int, int, int>(1, nameNHealthPos, 12);
            int rate = (int)((double)CoolTime / ResetCoolTime * 12);
            
            
            nameNHealth += "               │";
            list.Add(nameNHealth);
            //
            list.Add($"│  {index}  │ 소모 : HP {HP, 3}   /  MP {MP, 3}  │");
            string effectLine = $"│     │ 효과 : {GetTypeString()} ";
            effectLine = effectLine.PadRight(effectLine.Length + 4 - (2 + (int)(strPlayerTypeValue.Length * 0.5f)));
            effectLine += $"{strPlayerTypeValue}";
            effectLine = effectLine.PadRight(effectLine.Length + 2 - (int)Math.Ceiling(strPlayerTypeValue.Length * 0.5));
            effectLine += " → ";
            effectLine = effectLine.PadRight(effectLine.Length + 4 - (2 + (int)(strPlayerTypeValueAfter.Length * 0.5f)));
            effectLine += $"{strPlayerTypeValueAfter}";
            //Console.WriteLine($"{}");
            effectLine = effectLine.PadRight(effectLine.Length + 2 - (int)Math.Ceiling(strPlayerTypeValueAfter.Length * 0.5));
            effectLine += " │";
            list.Add(string.Format(effectLine));
            list.Add("└─────┴────────────────────────────┘");
            //│   │ 효과 : 공격력  1000 → 1200 │
            //│   │ 효과 : 공격력   12  →  13  │


            int countOfKorean = 0;
            for(int i = 0; i < list.Count; i++)
            {
                for (int j = 0; j < list[i].Length; j++)
                {
                    byte oF = (byte)((list[i][j] & 0xFF00) >> 7);
                    if (oF != 0)
                        ++countOfKorean;
                }
                list[i] = list[i].PadRight(100 - countOfKorean);
                for (int j = 0; j < list[i].Length; ++j)
                {
                    if (i == PaintRange.Item1)
                    {
                        Console.BackgroundColor = ConsoleColor.Black;
                        if (PaintRange.Item2 <= j && j <= PaintRange.Item2 + PaintRange.Item3)
                        {
                            if (j - PaintRange.Item2  > rate)
                            {
                                Console.BackgroundColor = ConsoleColor.White;                                
                            }
                        }
                            
                    }
                    Console.Write(list[i][j]);
                }
                Console.WriteLine();
            }
            Console.WriteLine();
        }

        public bool GetSkillAble(ISkillStatus Target)
        {
            if (CoolTime > 0)
                return false;//쿨타임이 안끝난경우
            if (Target.CurrentMP < MP)
                return false;//마나가 부족한경우
            if (Target.CurrentHP <= HP)
                return false;//체력이 부족한경우
            return true;
        }
                          
        public int UseSkill(ISkillStatus Target, out bool Wide)
        {
            Wide = isWide;
            if (CoolTime > 0)
                return -1;//쿨타임이 안끝난경우
            if (Target.CurrentMP < MP)
                return -2;//마나가 부족한경우
            if (Target.CurrentHP <= HP)
                return -4;//체력이 부족한경우
            Target.CurrentMP -= MP;
            Target.CurrentHP -= HP;

            if (Using)
            {
                Duration = ResetDuration;
                return Duration;//현재 이 스킬을 쓰는중인경우
            }

            Using = true;
            CoolTime = ResetCoolTime;


            //즉발형인경우
            if (ResetDuration == -1)
                Using = false;
            else
                Duration = ResetDuration;

            if (isBuff)//버프인경우 -3리턴
            {
                Added = (int)Value;
                switch (Type)
                {
                    case TypeOfAbility.MaxHP:
                        if (isPer)
                            Added = (int)(((float)Target.MaxHP * Value) - Target.MaxHP);
                        Console.WriteLine($"{Duration}턴 동안 MaxHP 증가 {Target.MaxHP}  -> {Target.MaxHP + Added}");
                        Target.MaxHP += Added;
                        Target.CurrentHP += Added;
                        break;
                    case TypeOfAbility.MaxMP:
                        if (isPer)
                            Added = (int)(((float)Target.MaxMP * Value) - Target.MaxMP);
                        Console.WriteLine($"{Duration}턴 동안 MaxMP 증가 {Target.MaxMP}  -> {Target.MaxMP + Added}");
                        Target.MaxMP += Added;
                        Target.CurrentMP += Added;
                        break;
                    case TypeOfAbility.CurrentHP:
                        if (isPer)
                            Added = Math.Min(Target.MaxHP - Target.CurrentHP, (int)(((float)Target.CurrentHP * Value) - Target.CurrentHP));
                        Added = Math.Min(Target.MaxHP - Target.CurrentHP, Added);

                        Console.WriteLine($"HP{Target.CurrentHP} -> HP{Target.CurrentHP + Added}");
                        Target.CurrentHP += Added;
                        break;
                    case TypeOfAbility.CurrentMP:
                        if (isPer)
                            Added = Math.Min(Target.MaxMP - Target.CurrentMP, (int)(((float)Target.CurrentMP * Value) - Target.CurrentMP));
                        Added = Math.Min(Target.MaxMP - Target.CurrentMP, Added);

                        Console.WriteLine($"MP{Target.CurrentMP} -> MP{Target.CurrentMP + Added}");
                        Target.CurrentMP += Added;
                        break;
                    case TypeOfAbility.Att:
                        if (isPer)
                            Added = (int)(((float)Target.Att * Value) - Target.Att);
                        Console.WriteLine($"{Duration}턴 동안 Att 증가 {Target.Att}  -> {Target.Att + Added}");
                        Target.Att += Added;
                        break;
                    case TypeOfAbility.Def:
                        if (isPer)
                            Added = (int)(((float)Target.Def * Value) - Target.Def);
                        Console.WriteLine($"{Duration}턴 동안 Def 증가 {Target.Def}  -> {Target.Def + Added}");
                        Target.Def += Added;
                        break;
                    case TypeOfAbility.Critical:
                        if (isPer)
                            Added = (int)(((float)Target.Critical * Value) - Target.Critical);
                        Console.WriteLine($"{Duration}턴 동안 Critical 증가 {Target.Critical}  -> {Target.Critical + Added}");
                        Target.Critical += Added;
                        break;
                    case TypeOfAbility.Dodge:
                        if (isPer)
                            Added = (int)(((float)Target.Dodge * Value) - Target.Dodge);
                        Console.WriteLine($"{Duration}턴 동안 Dodge 증가 {Target.Dodge}  -> {Target.Dodge + Added}");
                        Target.Dodge += Added;
                        break;
                }

                return -3;
            }
            else//공격인경우 데미지 리턴
            {
                switch (Type)
                {
                    case TypeOfAbility.MaxHP:
                        if (isPer)
                            return (int)((float)Target.MaxHP * Value);
                        else
                            return Target.MaxHP + (int)Value;
                    case TypeOfAbility.MaxMP:
                        if (isPer)
                            return (int)((float)Target.MaxMP * Value);
                        else
                            return Target.MaxMP + (int)Value;
                    case TypeOfAbility.CurrentHP:
                        if (isPer)
                            return (int)((float)Target.CurrentHP * Value);
                        else
                            return Target.CurrentHP + (int)Value;
                    case TypeOfAbility.CurrentMP:
                        if (isPer)
                            return (int)((float)Target.CurrentMP * Value);
                        else
                            return Target.CurrentMP + (int)Value;
                    case TypeOfAbility.Att:
                        if (isPer)
                            return (int)((float)Target.Att * Value);
                        else
                            return (int)Target.Att + (int)Value;
                    case TypeOfAbility.Def:
                        if (isPer)
                            return (int)((float)Target.Def * Value);
                        else
                            return (int)Target.Def + (int)Value;
                    case TypeOfAbility.Critical:
                        if (isPer)
                            return (int)((float)Target.Critical * Value);
                        else
                            return (int)Target.Critical + (int)Value;
                    case TypeOfAbility.Dodge:
                        if (isPer)
                            return (int)((float)Target.Dodge * Value);
                        else
                            return (int)Target.Dodge + (int)Value;
                }
            }

            return -999;//오류임 Exp쪽이 들어왔단소린데 버그임
        }

        //턴마다 쿨타임,지속시간 감소 지속시간 다됐으면 스킬사용해제
        public bool TurnCheck(ISkillStatus obj)
        {
            if (CoolTime > 0)
                --CoolTime;

            if (!isBuff || !Using)//공격형이거나, 사용중이지않거나
                return false;

            if (Duration > 0)//지속시간 줄이기
                --Duration;
            else//지속시간이 다닳면
            {
                Console.WriteLine($"{NameID}의 지속시간이 끝났다!");
                Using = false;

                switch (Type)
                {
                    case TypeOfAbility.MaxHP:
                        Console.WriteLine($"MaxHP {obj.MaxHP}  -> {obj.MaxHP - Added}");
                        obj.MaxHP -= Added;
                        obj.CurrentHP = Math.Min(obj.MaxHP, obj.CurrentHP);
                        break;
                    case TypeOfAbility.MaxMP:
                        Console.WriteLine($"MaxHP {obj.MaxMP}  -> {obj.MaxMP - Added}");
                        obj.MaxMP -= Added;
                        obj.CurrentMP = Math.Min(obj.MaxMP, obj.CurrentMP);
                        break;
                    case TypeOfAbility.Att:
                        Console.WriteLine($"Att {obj.Att}  -> {obj.Att - Added}");
                        obj.Att -= Added;
                        break;
                    case TypeOfAbility.Def:
                        Console.WriteLine($"Att {obj.Def}  -> {obj.Def - Added}");
                        obj.Def -= Added;
                        break;
                    case TypeOfAbility.Critical:
                        Console.WriteLine($"Att {obj.Critical}  -> {obj.Critical - Added}");
                        obj.Critical -= Added;
                        break;
                    case TypeOfAbility.Dodge:
                        Console.WriteLine($"Att {obj.Dodge}  -> {obj.Dodge - Added}");
                        obj.Dodge -= Added;
                        break;
                }
                return true;
            }
            return false;
        }

        public void ResetSkill(ISkillStatus obj)
        {
            if(Using)
            {
                switch (Type)
                {

                    case TypeOfAbility.MaxHP:
                        obj.MaxHP -= Added;
                        obj.CurrentHP = (int)((float)obj.CurrentHP / Value);
                        break;
                    case TypeOfAbility.MaxMP:
                        obj.MaxMP -= Added;
                        obj.CurrentHP = (int)((float)obj.CurrentMP / Value);
                        break;
                    case TypeOfAbility.Att:
                        obj.Att -= Added;
                        break;
                    case TypeOfAbility.Def:
                        obj.Def -= Added;
                        break;
                    case TypeOfAbility.Critical:
                        obj.Critical -= Added;
                        break;
                    case TypeOfAbility.Dodge:
                        obj.Dodge -= Added;
                        break;
                }
                Using = false;
                Duration = 0;
            }
            CoolTime = 0;

        }
    }
}
