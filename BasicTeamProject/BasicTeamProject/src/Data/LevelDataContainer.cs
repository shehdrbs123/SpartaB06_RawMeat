using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BasicTeamProject.Data
{
    internal class LevelDataContainer : DataReader
    {
        List<int> _maxExps;
        public LevelDataContainer()
        {
            _maxExps = new List<int>();
        }
        public override void Process(string[] data)
        {
            _maxExps.Add(int.Parse(data[0]));
        }
        public int GetMaxExp(int level)
        {
            return _maxExps[level - 1];
        }
    }
}
