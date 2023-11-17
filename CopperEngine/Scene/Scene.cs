namespace CopperEngine.Scene;

public class Scene
{
    public readonly string DisplayName;
    public readonly Guid SceneId;
    
    public Scene(string displayName) : this (displayName, Guid.NewGuid()) {}
    
    public Scene(string displayName, Guid sceneId)
    {
        DisplayName = displayName;
        SceneId = sceneId;
    }
}