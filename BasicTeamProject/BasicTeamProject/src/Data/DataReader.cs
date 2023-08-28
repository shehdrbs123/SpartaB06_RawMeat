using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BasicTeamProject.Data
{
    public abstract class DataReader
    {
        public virtual void Init(string path)
        {
            StreamReader sr = new StreamReader(path);
            string inputData = "";
            while (!sr.EndOfStream)
            {
                inputData = sr.ReadLine();
                if (inputData == "END")
                    break;
                string[] data = inputData.Split('|');
                Process(data);
            }
            sr.Close();
        }

        public abstract void Process(string[] data);
    }
}
