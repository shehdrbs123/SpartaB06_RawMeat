﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            this.Mp = skill.Mp;
            this.ResetCoolTime = skill.CoolTime;
            this.isPer = skill.isPer;
            this.isBuff = skill.isBuff;
            this.Value = skill.Value;
            this.Type = skill.Type;
            this.ResetDuration = skill.ResetDuration;
        }

        public string NameID { get; set; }
        public int Mp { get; set; }
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
        public void ShowInfo()
        {
            Console.WriteLine($"{NameID} | 소모량 -{Mp}");
            if(Using)
                Console.WriteLine($"사용중 : 지속시간 {Duration} 턴 지속");
            Console.WriteLine($"MP소모량 : {Mp}");
            Console.WriteLine($"스킬소모량 : {Mp}");
            Console.WriteLine($"쿨타임 : {CoolTime} / {ResetCoolTime}");
            if (isPer)
                    Console.WriteLine($"스킬 계수 : My{Type.ToString()} * {Value * 100} %");
                else if (!isBuff)
                    Console.WriteLine($"스킬 데미지: My{Type.ToString()} + {Value}");
                else
                    Console.WriteLine($"버프 증가량 : +{Value}{Type.ToString()}");
            
        }

        //증가Value를 리턴할거임
        public int UseSkill(ISkillStatus Target)
        {
            if (CoolTime > 0)
                return -1;//쿨타임이 안끝난경우
            if (Target.CurrentMP < Mp)
                return -2;//마나가 부족한경우
            Target.CurrentMP -= Mp;

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
                            Added = (int)((float)Target.MaxHP * Value);
                        Target.MaxHP += Added;
                        Target.CurrentHP += Added;
                        break;
                    case TypeOfAbility.MaxMP:
                        if (isPer)
                            Added = (int)((float)Target.MaxMP * Value);
                        Target.MaxMP += Added;
                        Target.CurrentMP += Added;
                        break;
                    case TypeOfAbility.CurrentHP:
                        if (isPer)
                            Added = (int)((float)Target.CurrentHP * Value);
                        Target.CurrentHP += Added;
                        break;
                    case TypeOfAbility.CurrentMP:
                        if (isPer)
                            Added = (int)((float)Target.CurrentMP * Value);
                        Target.CurrentMP += Added;
                        break;
                    case TypeOfAbility.Att:
                        if (isPer)
                            Added = (int)((float)Target.Att * Value);
                        Target.Att += Added;
                        break;
                    case TypeOfAbility.Def:
                        if (isPer)
                            Added = (int)((float)Target.Def * Value);
                        Target.Def += Added;
                        break;
                    case TypeOfAbility.Critical:
                        if (isPer)
                            Added = (int)((float)Target.Critical * Value);
                        Target.Critical += Added;
                        break;
                    case TypeOfAbility.Dodge:
                        if (isPer)
                            Added = (int)((float)Target.Dodge * Value);
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
                Using = false;

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
