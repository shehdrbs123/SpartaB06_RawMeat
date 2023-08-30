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
    protected abstract void WriteView();

    protected virtual void PreOperate()
    {

    }

    protected virtual void afterOperate()
    {
        DataManager.Instance.FunctionList = _FunctionList;
    }

    protected abstract void SetFunctionList();
    protected void EndView()
    {
        enter();
        Console.WriteLine("원하시는 행동을 입력해 주세요");
        Console.WriteLine(">>");
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