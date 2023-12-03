using CopperEngine.Components;
using CopperEngine.Info;
using CopperEngine.Physics;
using CopperEngine.Scenes;
using Raylib_cs;

namespace CopperEngine;

public static class EnginePhysics
{
    private static bool initialized;

    public static Simulation Simulation { get; private set; }

    public static float FixedTimeStep = 0.02f;
    internal static float FixedTimer;

    internal static PhysicsSettings PhysicsSettings = new();

    internal static void Initialize()
    {
        if (initialized)
            return;
        initialized = true;

        Simulation = new Simulation(PhysicsSettings);
    }

    internal static void Update()
    {
        FixedTimer += Time.DeltaTime;
        while (FixedTimer >= FixedTimeStep) 
        {
            FixedUpdate();
            FixedTimer -= FixedTimeStep;
        }
    }

    private static void FixedUpdate()
    {
        Simulation.Update(1.0F / FixedTimeStep, 1);
        Engine.EngineApplication?.FixedUpdate();
        UpdateGameComponents();
    }

    private static void UpdateGameComponents()
    {
        foreach (var gm in SceneManager.ActiveScene.GameObjects)
        {
            foreach (var component in gm.GameComponents)
            {
                Rlgl.PushMatrix();
            
                Rlgl.Translatef(gm.Transform.Position.X, gm.Transform.Position.Y, gm.Transform.Position.Z);
                Rlgl.Scalef(gm.Transform.Scale.X, gm.Transform.Scale.Y, gm.Transform.Scale.Z);
                Rlgl.Rotatef(gm.Transform.Rotation.W, gm.Transform.Rotation.X, gm.Transform.Rotation.Y, gm.Transform.Rotation.Z);
            
                component.FixedUpdate();
            
                Rlgl.PopMatrix();
            }
        }
    }
}