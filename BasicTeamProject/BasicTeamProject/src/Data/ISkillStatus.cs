using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BasicTeamProject.Data
{
    public interface ISkillStatus
    {
        public int CurrentHP { get; set; }
        public int CurrentMP { get; set; }
        public int MaxHP { get; set; }
        public int MaxMP { get; set; }
        public float Att { get; set; }
        public float Def { get; set; }
        public float Critical { get; set; }
        public float Dodge { get; set; }
    }
}
