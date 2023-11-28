using CopperEngine.Components;

namespace CopperEngine.Scenes;

public class Scene
{
    public readonly string DisplayName;
    public readonly Guid SceneId;

    internal List<GameObject> GameObjects = new();
    
    public Scene(string displayName) : this (displayName, Guid.NewGuid()) {}

    internal Scene(string displayName, Guid sceneId, bool register)
    {
        DisplayName = displayName;
        SceneId = sceneId;
        
        if(register)
            SceneManager.RegisterScene(this);
    }
    
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

internal static class SceneExtensions
{
    public static Scene Deserialize(this SerializedScene scene)
    {
        return new Scene(scene.DisplayName!, scene.SceneId, false)
        {
            GameObjects = scene.GameObjects
        };
    }

    public static SerializedScene Serialize(this Scene scene)
    {
        return new SerializedScene()
        {
            DisplayName = scene.DisplayName,
            GameObjects = scene.GameObjects,
            SceneId = scene.SceneId
        };
    }
}

internal class SerializedScene
{
    public string? DisplayName;
    public Guid SceneId;

    public List<GameObject> GameObjects = new();
}