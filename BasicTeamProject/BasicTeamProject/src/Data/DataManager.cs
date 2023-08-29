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
            s_Instance.Shop = new Shop();

            s_Instance._skillDataContainer = new SkillDataContainer();
            s_Instance._skillDataContainer.Init("Data\\SkillData.txt");
            s_Instance._itemDataContainer = new ItemDataContainer();
            s_Instance._itemDataContainer.Init("Data\\ItemData.txt");
            s_Instance._monsterDataContainer = new MonsterDataContainer();
            s_Instance._monsterDataContainer.Init("Data\\MonsterData.txt");
            s_Instance._levelDataContainer = new LevelDataContainer();
            s_Instance._levelDataContainer.Init("Data\\LevelData.txt");
            s_Instance._rewardDataContainer = new RewardDataContainer();
            s_Instance._rewardDataContainer.Init("Data\\RewardData.txt");
            s_Instance._dungeonDataContainer = new DungeonDataContainer();
            s_Instance._dungeonDataContainer.Init("Data\\DungeonData.txt");
            s_Instance._playerDataContainer = new PlayerDataContainer();
            s_Instance._playerDataContainer.Init("Data\\PlayerData.txt");
            s_Instance._shopDataContainer = new ShopDataContainer();
            s_Instance._shopDataContainer.Init("Data\\ShopData.txt");
            s_Instance._sceneManager = new SceneManager();
            s_Instance._sceneManager.Init();
            s_Instance.InputMemory = new InputMemory();


            s_Instance.RenewItems(6);
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
        public Item CreateItem(string name)
        {
            return _itemDataContainer.CreateItem(name);
        }
        public Item CreateItem(string name, int count)
        {
            return _itemDataContainer.CreateItem(name, count);
        }
        public List<Item> GetReward(string name, out int gold)
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

        public void PlayerSetting(string key)
        {
            _playerDataContainer.PlayerSetting(key);
        }
        public void PlayerSetting(int key)
        {
            _playerDataContainer.PlayerSetting(key);
        }
        public void RenewItems(int ListCount)
        {
            _shopDataContainer.RenewItems(ListCount);
        }


        MonsterDataContainer        _monsterDataContainer;
        LevelDataContainer          _levelDataContainer;
        RewardDataContainer         _rewardDataContainer;
        ItemDataContainer           _itemDataContainer;
        DungeonDataContainer        _dungeonDataContainer;
        SkillDataContainer          _skillDataContainer;
        SceneManager                _sceneManager;
        PlayerDataContainer         _playerDataContainer; 
        ShopDataContainer           _shopDataContainer;


        public List<Monster>        Monsters;
        public Player               Player;
        public Inventory            Inventory;
        public List<string>         FunctionList;
        public InputMemory          InputMemory;
        public Shop                 Shop;
    }
}
