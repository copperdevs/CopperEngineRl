using System.Numerics;
using CopperEngine.Components;
using Jitter2;

namespace CopperEngine.Scenes;

public class Scene
{
    public readonly string DisplayName;
    public readonly Guid SceneId;

    internal List<GameObject> GameObjects = new();

    internal World PhysicsWorld = new();
    
    public Scene(string displayName) : this (displayName, Guid.NewGuid()) {}
    
    internal Scene(string displayName, Guid sceneId, bool register)
    {
        DisplayName = displayName;
        SceneId = sceneId;
        
        if(register)
            SceneManager.RegisterScene(this);
    }

    internal Scene(string displayName, Guid sceneId)
    {
        DisplayName = displayName;
        SceneId = sceneId;
        
        SceneManager.RegisterScene(this);
    }

    public GameObject CreateGameObject() => CreateGameObject(Vector3.Zero);    
    
    public GameObject CreateGameObject(Vector3 startPosition)
    {
        var gameObject = new GameObject();
        GameObjects.Add(gameObject);

        gameObject.ParentScene = this;
        gameObject.Transform.Position = startPosition;
        
        return gameObject;
    }

    public static implicit operator Guid(Scene scene) => scene.SceneId;
}