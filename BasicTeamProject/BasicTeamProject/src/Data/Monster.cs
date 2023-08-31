using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BasicTeamProject.src;

namespace BasicTeamProject.Data
{
    public class Monster : ISkillStatus
    {
        public Monster() 
        {
        }
        public Monster(Monster mon)
        {
            this.NameID = mon.NameID;
            this.Level = mon.Level;
            this.MaxHP = mon.MaxHP;
            this.CurrentHP = mon.CurrentHP;
            this.MaxMP = mon.MaxMP;
            this.CurrentMP = mon.CurrentMP;
            this.Att = mon.Att;
            this.Def = mon.Def;
            this.Critical = mon.Critical;
            this.Dodge = mon.Dodge;
            for (int i = 0; i < mon.Skills.Count; i++)
            {
                Skill skill = new Skill(mon.Skills[i]);
                Skills.Add(skill);
            }
        }
        public string NameID { get; set; }
        public int Level { get; set; }
        public int MaxHP { get; set; }
        public int CurrentHP { get; set; }
        public int MaxMP { get; set; }
        public int CurrentMP { get; set; }
        public float Att { get; set; }
        public float Def { get; set; }
        public float Critical { get; set; }
        public float Dodge { get; set; }
        public List<Skill> Skills { get; set; } = new List<Skill>();
        public bool isDead { get; set; } = false;


        public bool MonsterAct(out int damege, out bool wide)
        {
            wide = false;
            for (int i = 0; i < Skills.Count; ++i)
            {
                if (Skills[i].GetSkillAble(this))
                {
                    Console.WriteLine($"{NameID}의 {Skills[i].NameID}발동!");
                    Thread.Sleep(600);
                    damege = Skills[i].UseSkill(this, out wide);
                    if (damege > 0)
                    {
                        return true;
                    }
                    else if (damege == -3)
                    {
                        return false;
                    }
                }
            }
            Console.WriteLine($"퍽!!");
            Thread.Sleep(600);
            damege = (int)Att;
            return true;
        }

        public void TurnCheck()
        {
            foreach (Skill skill in Skills)
            {
                if (skill.TurnCheck(this))
                {
                    //이때 버프가 끝난것 따로 뭔가..넣나..
                    Console.WriteLine($"{NameID}의 {skill.NameID} 지속시간이 끝났다!");
                }
            }
        }
        
        public void ShowInfo(int index)
        {
            List<String> list = new List<String>();
            Tuple<int, int, int> PaintRange;
            int rate = (int)((double)CurrentHP / MaxHP * 24);
            int HPLeftPos;

            list.Add("┌─────┬───────────────────────────┐");
            string NameHPLine = $"│     │ Lv.{Level,2} {NameID}              │";
            list.Add(NameHPLine);
            list.Add($"│  {index}  │ 공격력 :{Att, 3} / 방어력 :{Def, 3} │");
            list.Add($"│     │       HP {CurrentHP,4} / {MaxHP,4}      │");
            PaintRange = new Tuple<int, int, int>(3, 8, 24);
            list.Add("└─────┴───────────────────────────┘");
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
                        Console.ForegroundColor = ConsoleColor.White;
                        if (PaintRange.Item2 <= j && j <= PaintRange.Item2 + PaintRange.Item3)
                        {
                            if (j - PaintRange.Item2 <= rate)
                            {
                                Console.BackgroundColor = ConsoleColor.Red;
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
}
