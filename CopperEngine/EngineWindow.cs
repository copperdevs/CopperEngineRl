using System.Numerics;
using Raylib_cs;

namespace CopperEngine;

public static class EngineWindow
{
    private static bool initialized;

    public static Action<Vector2>? WindowResized;
    public static Vector2 WindowSize;

    internal static void Initialize()
    {
        if (initialized)
            return;
        initialized = true;
        
        Raylib.SetConfigFlags(ConfigFlags.FLAG_MSAA_4X_HINT | ConfigFlags.FLAG_VSYNC_HINT | ConfigFlags.FLAG_WINDOW_RESIZABLE);
        Raylib.InitWindow(650, 400, "CopperEngineRl");
        Raylib.SetTargetFPS(144);
        Raylib.InitAudioDevice();
    }

    internal static void Update()
    {
        var currentSize = new Vector2(Raylib.GetScreenWidth(), Raylib.GetScreenHeight());
        
        if (WindowSize == currentSize) 
            return;
        
        WindowSize = currentSize;
        WindowResized?.Invoke(WindowSize);
    }

    internal static void Stop()
    {
        Raylib.CloseAudioDevice();
        Raylib.CloseWindow();   
    }
}