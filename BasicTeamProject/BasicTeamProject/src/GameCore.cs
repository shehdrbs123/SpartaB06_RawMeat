using BasicTeamProject.Data;
using BasicTeamProject.Scene;
namespace BasicTeamProject
{
    public class GameCore
    {
        private DataManager _manager;
        private InputMemory _input;
        private bool _isTest = false;
        private bool _isGamePlay = true;

        public GameCore()
        {
            _manager = DataManager.Instance;
            _input = DataManager.Instance.InputMemory;
            //Console.SetWindowSize(100,80);
        }
        public void Play(bool isTest)
        {
            _isTest = isTest;
            SetDataDefault();
            GamePlay();
        }


        private void SetDataDefault()
        {
            _manager.FunctionList = new List<string>();
            if (!_isTest)
            {
                _manager.FunctionList.Add("CreateUserScene");
            }
            else
            {
                DataManager.Instance.Inventory.AddItem("낡은검", 1);
                DataManager.Instance.Inventory.AddItem("낡은검", 1);
                DataManager.Instance.Inventory.AddItem("장갑", 1);
                DataManager.Instance.Inventory.AddItem("갑옷", 1);
                DataManager.Instance.Inventory.AddItem("투구", 1);
                DataManager.Instance.Inventory.AddItem("겁내쌘무기", 2);
                DataManager.Instance.Inventory.AddItem("똥", 100);
                DataManager.Instance.Inventory.AddItem("체력포션", 10);
                DataManager.Instance.Inventory.AddItem("마나포션", 10);
                _manager.FunctionList.Add("CreateUserScene");
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
            _manager.GetScene(_manager.FunctionList[0]).Execute();
            while (IsPlay())
            {
                //입력 받기
                while (IsCanInput()&&!_input.TryGetKey(out _manager.InputMemory.PreInput))
                {
                    Console.WriteLine("잘못 입력하셨습니다");
                    Console.Write(">>");
                }
                
                Console.Clear();
                //출력
                _manager.GetScene(_manager.FunctionList[_manager.InputMemory.PreInput]).Execute();
            }
        }

        private bool IsPlay()
        {
            return _isGamePlay;
        }

        private bool IsCanInput()
        {
            if (_manager.InputMemory.InputComplete)
            {
                _manager.InputMemory.InputComplete = false;
                return false;
            }
            else
            {
                return true;
            }
        }

    }
}