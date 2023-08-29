// See https://aka.ms/new-console-template for more information

using BasicTeamProject;

public class Program
{
    private static  bool isTestMode = true;
    public static void Main()
    {
        GameCore engine = new GameCore();
        engine.Play(isTestMode);
    }
}