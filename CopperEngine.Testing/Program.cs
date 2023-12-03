using System.Numerics;
using CopperEngine.Components;
using CopperEngine.Scenes;
using JoltPhysicsSharp;

namespace CopperEngine.Testing;

public static class Program
{
    public static void Main()
    {
        Engine.Initialize(() =>
        {
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

            var physicsScene = new Scene("Physics Scene");

            var ground = physicsScene.CreateGameObject(-(Vector3.UnitY * 2));
        
            var groundSize = new Vector3(100, 1, 100)/2;
            ground.AddComponent(new Rigidbody(new BoxShape(groundSize), MotionType.Static));
            ground.AddComponent<TestComponent>();

            var cube = physicsScene.CreateGameObject();
        
            var cubeSize = Vector3.One/2;
            cube.AddComponent(new Rigidbody(new BoxShape(cubeSize), MotionType.Dynamic, 1, 1));
            cube.AddComponent<TestComponent>();
            
            
        });
        Engine.Run();
    }
}