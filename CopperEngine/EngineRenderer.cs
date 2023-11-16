using System.Numerics;
using CopperEngine.Data;
using CopperEngine.Utility;
using Raylib_CsLo;

namespace CopperEngine;

public static class EngineRenderer
{
    internal static RenderTexture gameTexture;
    private static Camera gameCamera = new()
    {
        Position = new Vector3(10, 10, 10),
        Target = Vector3.Zero
    };

    internal static RenderTexture editorTexture;
    private static Camera editorCamera = new()
    {
        Position = new Vector3(-10, 10, -10),
        Target = Vector3.Zero
    };

    private static bool initialized;

    internal static void Initialize()
    {
        if (initialized)
            return;
        initialized = true;
        
        gameTexture = Raylib.LoadRenderTexture(Raylib.GetScreenWidth(), Raylib.GetScreenHeight());
        editorTexture = Raylib.LoadRenderTexture(Raylib.GetScreenWidth(), Raylib.GetScreenHeight());
    }

    internal static void Render()
    {
        if (Raylib.IsWindowResized())
        {
            Raylib.UnloadRenderTexture(gameTexture);
            gameTexture = Raylib.LoadRenderTexture(Raylib.GetScreenWidth(), Raylib.GetScreenHeight());
            Raylib.UnloadRenderTexture(editorTexture);
            editorTexture = Raylib.LoadRenderTexture(Raylib.GetScreenWidth(), Raylib.GetScreenHeight());
        }
            
        Raylib.BeginDrawing();
            
        // game
        {
            Raylib.BeginTextureMode(gameTexture);
            Raylib.ClearBackground(ColorUtil.DarkGray);
            Raylib.BeginMode3D(gameCamera);
            
            RenderScene();
            
            Raylib.EndMode3D();
            Raylib.EndTextureMode();
        }
            
        // editor
        {
            Raylib.BeginTextureMode(editorTexture);
            Raylib.ClearBackground(ColorUtil.DarkGray);
            Raylib.BeginMode3D(editorCamera);
            
            RenderScene();
            
            
            Raylib.SetConfigFlags(ConfigFlags.FLAG_VSYNC_HINT | ConfigFlags.FLAG_WINDOW_MAXIMIZED);
            Raylib.DrawGrid(100, 1);
            Raylib.SetConfigFlags(ConfigFlags.FLAG_VSYNC_HINT | ConfigFlags.FLAG_WINDOW_MAXIMIZED | ConfigFlags.FLAG_MSAA_4X_HINT);
            
            Raylib.EndMode3D();
            Raylib.EndTextureMode();
        }
    }
    
    private static void RenderScene()
    {
        Raylib.DrawCube(Vector3.Zero, 1, 1, 1, ColorUtil.Red);
    }

    internal static void Stop()
    {
        Raylib.UnloadRenderTexture(gameTexture);
        Raylib.UnloadRenderTexture(editorTexture);
    }
}