using System.Numerics;
using CopperEngine.Data;
using CopperEngine.Scenes;
using CopperEngine.Utility;
using Raylib_cs;

namespace CopperEngine;

public static class EngineRenderer
{
    internal static RenderTexture2D GameTexture;
    internal static readonly Camera GameCamera = new()
    {
        Position = new Vector3(10, 10, 10),
        Target = Vector3.Zero
    };

    internal static RenderTexture2D EditorTexture;
    internal static readonly Camera EditorCamera = new()
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
        
        GameTexture = Raylib.LoadRenderTexture(Raylib.GetScreenWidth(), Raylib.GetScreenHeight());
        EditorTexture = Raylib.LoadRenderTexture(Raylib.GetScreenWidth(), Raylib.GetScreenHeight());
    }

    internal static void Render()
    {
        if (Raylib.IsWindowResized())
        {
            Raylib.UnloadRenderTexture(GameTexture);
            GameTexture = Raylib.LoadRenderTexture(Raylib.GetScreenWidth(), Raylib.GetScreenHeight());
            Raylib.UnloadRenderTexture(EditorTexture);
            EditorTexture = Raylib.LoadRenderTexture(Raylib.GetScreenWidth(), Raylib.GetScreenHeight());
        }
            
        // game - always do this
        {
            Raylib.BeginTextureMode(GameTexture);
            Raylib.ClearBackground(ColorUtil.DarkGray);
            Raylib.BeginMode3D(GameCamera);
            
            RenderScene();
            
            Raylib.EndMode3D();
            Raylib.EndTextureMode();
        }
            
        // editor - only do if editor
        if(Engine.State is Engine.EngineState.Editor)
        {
            Raylib.BeginTextureMode(EditorTexture);
            Raylib.ClearBackground(ColorUtil.DarkGray);
            Raylib.BeginMode3D(EditorCamera);
            
            RenderScene();
            
            Raylib.DrawGrid(100, 1);

            
            Raylib.EndMode3D();
            Raylib.EndTextureMode();
        }

        if (Engine.State is not Engine.EngineState.Editor)
        {
            PostProcessingRender(() =>
            {
                Raylib.DrawTextureRec(GameTexture.Texture, new Rectangle(0, 0, GameTexture.Texture.Width, -GameTexture.Texture.Height), Vector2.Zero, ColorUtil.White);
            });
        }
    }

    internal static void PostProcessingRender(Action renderMethod)
    {
        renderMethod.Invoke();
    }
    
    private static void RenderScene()
    {
        // Raylib.DrawCube(Vector3.Zero, 1, 1, 1, ColorUtil.Red);
        SceneManager.UpdateCurrentScene();
    }

    internal static void Stop()
    {
        Raylib.UnloadRenderTexture(GameTexture);
        Raylib.UnloadRenderTexture(EditorTexture);
    }
}