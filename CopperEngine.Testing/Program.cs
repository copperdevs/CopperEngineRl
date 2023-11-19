using CopperEngine;
using CopperEngine.Scenes;

namespace CopperEngine.Testing;

public static class Program
{
    public static void Main()
    {
        Engine.Initialize();

        var scene = new Scene("Test Scene");
        
        var gameObject = scene.CreateGameObject();
        gameObject.AddComponent<TestComponent>();
        
        Engine.Run();
    }
}