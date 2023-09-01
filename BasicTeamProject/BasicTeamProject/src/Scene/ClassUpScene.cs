using BasicTeamProject.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BasicTeamProject.Scene
{
    internal class ClassUpScene : Scene
    {
        string teacher;
        int classUpLevel;
        protected override void SetFunctionList()
        {
            _FunctionList.Add("MainScene");
        }

        protected override void WriteView()
        {
            teacher = _dataManager.Player.job.ToString();
            Console.WriteLine($"{teacher}의 성소");
            enter();
            classUpLevel = _dataManager.GetClassUpLevel(_dataManager.Player);
            if (classUpLevel == 0)
                return;

            teacher += " 전직교관";
            Console.WriteLine($"{teacher} : 안녕");
            Console.ReadLine();
            
            Console.WriteLine($"{teacher} : 전직을 하려면 레벨이 {classUpLevel}을 넘어야해");
            Console.WriteLine($"현재레벨 : {_dataManager.Player.Level}");
            Console.ReadLine();

            Console.WriteLine("1.전직한다");

            Console.WriteLine("0.나가기");
        }

        protected override void afterOperate()
        {
            int input;
            while (!_dataManager.InputMemory.TryGetKey(2, out input))
            {
                Console.WriteLine("잘못 입력하셨습니다.");
                Console.Write(">>");
            }
            _dataManager.InputMemory.PreInput = 0;
            _dataManager.InputMemory.InputComplete = true;
            if (input == 1)
            {
                int itemcount = _dataManager.Inventory.GetItemCount("똥", ItemType.Consumable);
                if (_dataManager.Player.Level < classUpLevel)
                {
                    Console.WriteLine($"{teacher} : 레벨이 부족하잖아!");
                    Console.ReadLine();
                    return;
                }
                else
                {
                    Console.WriteLine($"{teacher} : 좋아! 전직시켜주지!");
                    Console.ReadLine();
                    _dataManager.ClassUp(_dataManager.Player);
                    Console.WriteLine($"{teacher} : 축하해!");
                }
            }
            base.afterOperate();

        }
    }
}
