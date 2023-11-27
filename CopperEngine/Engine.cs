using CopperEngine.Logs;
using Raylib_CsLo;

namespace CopperEngine;

public static class Engine
{
    private static bool initialized;
    internal static DateTime StartTime = DateTime.Now;

    internal static EngineState State = EngineState.Editor;

    private static EngineApplication? engineApplication;

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
            engineApplication = application;
            
            engineApplication.Load();
            EngineWindow.WindowResized += engineApplication.WindowResize;
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
            EngineWindow.Update();
            
            Raylib.BeginDrawing();
            
            engineApplication?.PreUpdate();
            engineApplication?.Update();
            engineApplication?.PostUpdate();
            
            EngineRenderer.Render();
            EngineEditor.Render();
            
            Raylib.EndDrawing();
        }
        
        Stop();
    }

    private static void Stop()
    {
        engineApplication?.Stop();
        EngineEditor.Stop();
        EngineRenderer.Stop();
        EngineWindow.Stop();
    }
}