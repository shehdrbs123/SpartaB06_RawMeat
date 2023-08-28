using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BasicTeamProject.Data
{
    internal class DungeonDataContainer : DataReader
    {

        List<string[]> _dungeons;

        public DungeonDataContainer()
        {
            _dungeons = new List<string[]>();
        }

        public override void Process(string[] data)
        {
            string[] str = new string[int.Parse(data[0])];

            for(int i = 0; i < str.Length; i++)
            {
                str[i] = data[1 + i];
            }

            _dungeons.Add(str);
        }

        public string[] GetDungeons(int level)
        {
            return _dungeons[level - 1];
        }
    }
}
