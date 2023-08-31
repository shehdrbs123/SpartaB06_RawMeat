using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BasicTeamProject.src
{
    public class StringCounter
    {
        public static int GetStringLength(string str)
        {
            int countOfKorean = 0;
            for (int j = 0; j < str.Length; j++)
            {
                byte oF = (byte)((str[j] & 0xFF00) >> 7);
                if (oF != 0)
                    ++countOfKorean;
            }
            return countOfKorean;
        }
    }
}
