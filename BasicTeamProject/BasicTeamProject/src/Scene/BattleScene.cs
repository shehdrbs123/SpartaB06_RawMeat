using BasicTeamProject.Data;

namespace BasicTeamProject.Scene
{
    public class BattleScene : Scene
    {
        private int currentStage = 1;

        protected override void SetFunctionList()
        {
            _FunctionList.Add("BattleSelectScene");
            _FunctionList.Add("BattleSkillSelectScene");
            _FunctionList.Add("BattleConsumeScene");
        }

        protected override void PreOperate()
        {
            base.PreOperate();
            if (_dataManager.Monsters.Count == 0 || BattleSelectScene.remainingMonster == 0)
            {
                _dataManager.CreateDungeon(currentStage++);
                BattleSelectScene.remainingMonster = _dataManager.Monsters.Count;
            }
            _dataManager.InputMemory.SetRange(0,_FunctionList.Count);
        }

        protected override void WriteView()
        {
            Console.WriteLine("Battle Start!!");
            enter();
            Console.WriteLine("[몬스터]");
            int j = 1;
            for (int i = 0; i < _dataManager.Monsters.Count(); i++)
            {
                Monster monster = _dataManager.Monsters[i];
                if (!monster.isDead)
                    monster.ShowInfo(j++);
            }

            enter();

            Console.WriteLine("[플레이어 정보]");
            enter();
            _dataManager.Player.ShowBattleInfo();
            enter();

            enter();
            Console.WriteLine("0. 나가기");
            Console.WriteLine("1. 공격");
            Console.WriteLine("2. 스킬");
            Console.WriteLine("3. 소모품 사용");
            enter();
        }
    }
}
