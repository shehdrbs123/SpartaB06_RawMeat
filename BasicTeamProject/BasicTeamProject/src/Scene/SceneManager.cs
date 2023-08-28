using System.Collections.Generic;
using System.Reflection;

namespace BasicTeamProject.Scene;

public class SceneManager
{
    private Dictionary<string, Scene> _scenes;

    public SceneManager()
    {
        _scenes = new Dictionary<string, Scene>();

    }

    public void Init()
    {
        Type parentType = typeof(Scene);
        Assembly assembly = Assembly.GetExecutingAssembly();
        Type[] childTypes = assembly.GetTypes().Where(type => type.IsSubclassOf(parentType)).ToArray();
        
        foreach (Type childType in childTypes)
        {
            Scene? childScene = (Scene?)Activator.CreateInstance(childType);
            if(childScene != null)
                _scenes.Add(childType.Name,childScene);
        }
    }

    public Scene GetScene(string sceneName)
    {
        return _scenes[sceneName];
    }
}
