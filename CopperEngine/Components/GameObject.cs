using System.Numerics;
using CopperEngine.Data;
using CopperEngine.Scenes;

namespace CopperEngine.Components;

public class GameObject
{
    internal List<GameComponent> GameComponents = new();

    internal Scene ParentScene;
    
    public Transform Transform = new()
    {
        Position = Vector3.Zero, 
        Rotation = Quaternion.Identity, 
        Scale = Vector3.One
    };

    public void AddComponent<T>() where T : GameComponent, new() => AddComponent(new T());
    public void AddComponent(GameComponent component)
    {
        component.Parent = this;
        GameComponents.Add(component);
        component.Start();
    }
}