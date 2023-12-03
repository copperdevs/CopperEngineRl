using CopperEngine.Info;
using CopperEngine.Logs;
using Raylib_cs;

namespace CopperEngine;

public static class Engine
{
    private static bool initialized;
    internal static DateTime StartTime = DateTime.Now;

    internal static EngineState State = EngineState.Editor;

    internal static EngineApplication? EngineApplication;

    internal enum EngineState
    {
        Game,
        Editor
    }

    public static void Initialize<T>() where T : EngineApplication, new()
    {
        Initialize(new T());
    }
    
    public static void Initialize(EngineApplication application)
    {
        Initialize(() =>
        {
            EngineApplication = application;
            
            EngineApplication.Load();
            EngineWindow.WindowResized += EngineApplication.WindowResize;
        });
    }

    public static void Initialize()
    {
        Initialize(() => {});
    }
    
    public static void Initialize(Action loadAction)
    {
        if (initialized)
            return;
        initialized = true;

        CopperLogger.Initialize();

        InitializeElement(EngineWindow.Initialize, "Engine Window");
        InitializeElement(EngineRenderer.Initialize, "Engine Renderer");
        InitializeElement(EngineEditor.Initialize, "Engine Editor");
        InitializeElement(EnginePhysics.Initialize, "Engine Physics");
        
        InitializeElement(loadAction, "Engine Load Action");
        
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
            
            EngineApplication?.PreUpdate();
            EngineApplication?.Update();
            EngineApplication?.PostUpdate();
            
            Raylib.EndDrawing();
        }
        
        Stop();
    }

    private static void Stop()
    {
        EngineApplication?.Stop();
        EngineEditor.Stop();
        EngineRenderer.Stop();
        EngineWindow.Stop();
    }
}