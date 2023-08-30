using BasicTeamProject.Data;

namespace BasicTeamProject.Scene;

public abstract class Scene
{
    protected DataManager _dataManager;
    protected List<string> _FunctionList;
    public Scene()
    {
        _FunctionList = new List<string>();
        _dataManager = DataManager.Instance;
        _FunctionList.Add("MainScene");
        SetFunctionList();
    }
    protected virtual void PreOperate()
    {
        _dataManager.InputMemory.SetRange(1,_FunctionList.Count);
    }
    
    protected abstract void WriteView();

    protected virtual void afterOperate()
    {
        _dataManager.FunctionList = _FunctionList;
    }

    protected abstract void SetFunctionList();
    protected void EndView()
    {
        enter();
        Console.WriteLine("원하시는 행동을 입력해 주세요");
        Console.Write(">>");
    }

    protected void enter()
    {
        Console.WriteLine();
    }

    private void SetRangeDefault()
    {
        _dataManager.InputMemory.SetRange(1,_FunctionList.Count);
    }
    public void Execute()
    {
        SetRangeDefault();
        PreOperate();
        WriteView();
        afterOperate();
        EndView();
    }
}