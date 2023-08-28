using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BasicTeamProject.Scene;

namespace BasicTeamProject.Data
{
    public class DataManager
    {
        static DataManager s_Instance;
        public static DataManager Instance { get { Init(); return s_Instance; } }
        public static void Init()
        {
            if (s_Instance != null)
                return;

            s_Instance = new DataManager();

            s_Instance.Player = new Player();
            s_Instance.Inventory = new Inventory();
            s_Instance.Monsters = new List<Monster>();


            //s_Instance._skillDataContainer = new SkillDataContainer();
            //s_Instance._skillDataContainer.Init("나 중에 쓰자.txt");
            //s_Instance._itemDataContainer = new ItemDataContainer();
            //s_Instance._itemDataContainer.Init("나 중에 쓰자.txt");
            //s_Instance._monsterDataContainer = new MonsterDataContainer();
            //s_Instance._monsterDataContainer.Init("Data\\DungoenData.txt");
            //s_Instance._levelDataContainer = new LevelDataContainer();
            //s_Instance._levelDataContainer.Init("나 중에 쓰자.txt");
            //s_Instance._rewardDataContainer = new RewardDataContainer();
            //s_Instance._rewardDataContainer.Init("나 중에 쓰자.txt");
            //s_Instance._dungeonDataContainer = new DungeonDataContainer();
            //s_Instance._dungeonDataContainer.Init("나 중에 쓰자.txt");
            s_Instance._sceneManager = new SceneManager();
            s_Instance._sceneManager.Init();
            s_Instance.InputMemory = new InputMemory();
        }
        public void CreateDungeon(int level)
        {
            //던전생성시 몬스터들 생성할때 던전레벨 전달
            string[] dungeonInfo = _dungeonDataContainer.GetDungeons(level);

            for(int i = 0; i < dungeonInfo.Length; ++i)
            {
                Monsters.Add(CreateMonster(dungeonInfo[i]));
            }
        }
        public Monster CreateMonster(string name) 
        {
            return _monsterDataContainer.CreateMonster(name);
        }
        public Skill CreateSkill(string name)
        {
            return _skillDataContainer.CreateSkill(name);
        }
        public Item_ CreateItem(string name)
        {
            return _itemDataContainer.CreateItem(name);
        }
        public List<Item_> GetReward(string name, out int gold)
        {
            return _rewardDataContainer.GetReward(name, out gold);
        }
        public int GetMaxExp(int level)
        {
            return _levelDataContainer.GetMaxExp(level);
        }

        public Scene.Scene GetScene(string name)
        {
            return _sceneManager.GetScene(name);
        }

        MonsterDataContainer        _monsterDataContainer;
        LevelDataContainer          _levelDataContainer;
        RewardDataContainer         _rewardDataContainer;
        ItemDataContainer           _itemDataContainer;
        DungeonDataContainer        _dungeonDataContainer;
        SkillDataContainer          _skillDataContainer;
        SceneManager                _sceneManager;
        
        
        public List<Monster>        Monsters;
        public Player               Player;
        public Inventory            Inventory;
        public List<string>         FunctionList;
        public InputMemory          InputMemory;
    }
}
