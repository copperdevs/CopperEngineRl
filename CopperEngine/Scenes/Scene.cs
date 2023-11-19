using CopperEngine.Components;

namespace CopperEngine.Scenes;

public class Scene
{
    public readonly string DisplayName;
    public readonly Guid SceneId;

    internal List<GameObject> GameObjects = new();
    
    public Scene(string displayName) : this (displayName, Guid.NewGuid()) {}
    
    public Scene(string displayName, Guid sceneId)
    {
        DisplayName = displayName;
        SceneId = sceneId;
        
        SceneManager.RegisterScene(this);
    }

    public GameObject CreateGameObject()
    {
        var gameObject = new GameObject();
        GameObjects.Add(gameObject);

        gameObject.ParentScene = this;
        
        return gameObject;
    }

    public static implicit operator Guid(Scene scene) => scene.SceneId;
}