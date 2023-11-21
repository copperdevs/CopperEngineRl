using CopperEngine;
using CopperEngine.Scenes;

namespace CopperEngine.Testing;

public static class Program
{
    public static void Main()
    {
        Engine.Initialize();

        var sceneTwo = new Scene("Test Scene Two");
        
        var gameObjectTwo = sceneTwo.CreateGameObject();
        gameObjectTwo.AddComponent<TestComponent>();

        var sceneOne = new Scene("Test Scene One");
        
        var gameObjectOne = sceneOne.CreateGameObject();
        gameObjectOne.AddComponent<TestComponent>();
        
        Engine.Run();
    }
}