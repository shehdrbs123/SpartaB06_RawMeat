using BasicTeamProject.Data;
using BasicTeamProject.Scene;
namespace BasicTeamProject
{
    public class GameCore
    {
        private DataManager manager;
        private bool _isTest = false;
        private bool _isGamePlay = true;

        public GameCore()
        {
            manager = DataManager.Instance;
        }
        public void Play(bool isTest)
        {
            _isTest = isTest;
            SetDataDefault();
            GamePlay();
        }


        private void SetDataDefault()
        {
            manager.FunctionList = new List<string>();
            if (!_isTest)
            {
                manager.FunctionList.Add("CreateUserScene");
            }
            else
            {
                DataManager.Instance.Inventory.AddItem("낡은검", 1);
                DataManager.Instance.Inventory.AddItem("낡은검", 1);
                DataManager.Instance.Inventory.AddItem("낡은검", 1);
                DataManager.Instance.Inventory.AddItem("낡은검", 1);
                DataManager.Instance.Inventory.AddItem("낡은검", 1);
                DataManager.Instance.Inventory.AddItem("겁내쌘무기", 2);
                DataManager.Instance.Inventory.AddItem("똥", 100);
                manager.FunctionList.Add("CreateUserScene");
                //Console.WriteLine("보고 싶은 씬을 골라주세요");
                //int i = 0;
                //var pairs = SceneManager._scenes;

                //foreach (var pair in pairs)
                //{
                //    Console.WriteLine($"{i} : {pair.Key}");
                //    ++i;
                //}

                //int key;
                //while (!TryGetKey(SceneManager._scenes.Count, out key))
                //{
                //    Console.WriteLine("잘못 입력하셨습니다");
                //    Console.Write(">>");
                //}

                //FunctionList[0] = pairs.Values.ToList()[key];
            }
        }

        private void GamePlay()
        {
            manager.GetScene(manager.FunctionList[0]).Execute();
            while (IsPlay())
            {
                //입력 받기
                while (IsCanInput()&&!TryGetKey(manager.FunctionList.Count, out manager.InputMemory.preInput))
                {
                    Console.WriteLine("잘못 입력하셨습니다");
                    Console.Write(">>");
                }
                
                Console.Clear();
                //출력
                manager.GetScene(manager.FunctionList[manager.InputMemory.preInput-1]).Execute();
            }
        }

        private bool IsPlay()
        {
            return _isGamePlay;
        }

        private bool IsCanInput()
        {
            if (manager.InputMemory.InputComplete)
            {
                manager.InputMemory.InputComplete = false;
                return false;
            }
            else
            {
                return true;
            }
        }


        private bool TryGetKey(int range, out int key)
        {
            key = 0;
            bool isOk = false;
            if (int.TryParse(Console.ReadLine(), out key))
            {
                if (1 <= key && key <= range)
                {
                    manager.InputMemory.preInput = key;
                    isOk = true;
                }
            }

            return isOk;
        }
    }
}