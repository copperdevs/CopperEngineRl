using CopperEngine;
using CopperEngine.Components;
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

        var modelObject = sceneOne.CreateGameObject();
        modelObject.AddComponent(new Model("Resources/Models/cube.obj"));

        var reflectionObject = sceneOne.CreateGameObject();
        reflectionObject.AddComponent<ReflectionComponent>();
        
        Engine.Run();
    }
}