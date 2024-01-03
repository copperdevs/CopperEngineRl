using CopperEngine.Info;
using Raylib_cs;

namespace CopperEngine;

public static class Engine
{
    private static bool initialized;
    internal static DateTime StartTime = DateTime.Now;

    internal static EngineState State = EngineState.Editor;

    internal enum EngineState
    {
        Game,
        Editor
    }

    public static void Initialize()
    {
        if (initialized)
            return;
        initialized = true;

        CopperLogger.Initialize();

        InitializeElement(EngineWindow.Initialize, "Engine Window");
        InitializeElement(EngineRenderer.Initialize, "Engine Renderer");
        InitializeElement(EngineEditor.Initialize, "Engine Editor");
        InitializeElement(EnginePhysics.Initialize, "Engine Physics");

        return;

        void InitializeElement(Action target, string name)
        {
            Log.Info($"Starting initialization of {name}");
            target.Invoke();
            Log.Info($"Stopped initialization of {name}");
        }
    }

    public static void Run()
    {
        while (!Raylib.WindowShouldClose())
        {
            EnginePhysics.Update();
            Input.CheckInput();

            EngineWindow.Update();

            Raylib.BeginDrawing();

            EngineRenderer.Render();
            EngineEditor.Render();

            Raylib.EndDrawing();
        }

        Stop();
    }

    private static void Stop()
    {
        EngineEditor.Stop();
        EngineRenderer.Stop();
        EngineWindow.Stop();
    }
}