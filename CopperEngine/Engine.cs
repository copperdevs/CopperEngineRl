using CopperEngine.Logs;
using Raylib_CsLo;

namespace CopperEngine;

public static class Engine
{
    private static bool initialized;
    internal static DateTime StartTime = DateTime.Now;
    
    public static void Initialize()
    {
        if (initialized)
            return;
        initialized = true;
        
        CopperLogger.Initialize();

        InitializeElement(EngineWindow.Initialize, "Engine Window");
        InitializeElement(EngineRenderer.Initialize, "Engine Renderer");
        InitializeElement(EngineEditor.Initialize, "Engine Editor");
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
        Initialize();
        
        while (!Raylib.WindowShouldClose())
        {
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