using Raylib_CsLo;

namespace CopperEngine;

public static class EngineWindow
{
    private static bool initialized;

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

    internal static void Stop()
    {
        Raylib.CloseAudioDevice();
        Raylib.CloseWindow();   
    }
}