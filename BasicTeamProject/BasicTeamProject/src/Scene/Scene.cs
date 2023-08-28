using BasicTeamProject.Data;

namespace BasicTeamProject.Scene;

public abstract class Scene
{
    protected DataManager _dataManager;
    protected List<string> FunctionList;
    public Scene()
    {
        FunctionList = new List<string>();
        _dataManager = DataManager.Instance;
        SetFunctionList();
    }
    protected abstract void WriteView();

    protected virtual void PreOperate()
    {
        
    }

    protected virtual void afterOperate()
    {
        DataManager.Instance.FunctionList = FunctionList;
    }

    protected abstract void SetFunctionList();
    private void EndView()
    {
        Console.WriteLine("원하시는 행동을 입력해 주세요");
    }

    protected void enter()
    {
        Console.WriteLine();
    }

    public void Execute()
    {
        PreOperate();
        WriteView();
        afterOperate();
        EndView();
    }
}