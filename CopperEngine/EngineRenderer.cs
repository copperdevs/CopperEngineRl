﻿using System.Numerics;
using CopperEngine.Data;
using CopperEngine.Editor;
using CopperEngine.Scenes;
using CopperEngine.Utility;
using Raylib_CsLo;

namespace CopperEngine;

public static class EngineRenderer
{
    internal static RenderTexture gameTexture;
    internal static Camera gameCamera = new()
    {
        Position = new Vector3(10, 10, 10),
        Target = Vector3.Zero
    };

    internal static RenderTexture editorTexture;
    internal static Camera editorCamera = new()
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
            
        // game - always do this
        {
            Raylib.BeginTextureMode(gameTexture);
            Raylib.ClearBackground(ColorUtil.DarkGray);
            Raylib.BeginMode3D(gameCamera);
            
            RenderScene();
            
            Raylib.EndMode3D();
            Raylib.EndTextureMode();
        }
            
        // editor - only do if editor
        if(Engine.State is Engine.EngineState.Editor)
        {
            Raylib.BeginTextureMode(editorTexture);
            Raylib.ClearBackground(ColorUtil.DarkGray);
            Raylib.BeginMode3D(editorCamera);
            
            RenderScene();
            
            Raylib.SetConfigFlags(ConfigFlags.FLAG_VSYNC_HINT | ConfigFlags.FLAG_WINDOW_MAXIMIZED);
            Raylib.DrawGrid(100, 1);
            Raylib.SetConfigFlags(ConfigFlags.FLAG_VSYNC_HINT | ConfigFlags.FLAG_WINDOW_MAXIMIZED | ConfigFlags.FLAG_MSAA_4X_HINT);

            InspectorWindow.RenderGizmos();
            
            Raylib.EndMode3D();
            Raylib.EndTextureMode();
        }

        if (Engine.State is not Engine.EngineState.Editor)
        {
            Raylib.DrawTextureRec(gameTexture.texture, new Rectangle(0, 0, gameTexture.texture.width, -gameTexture.texture.height), Vector2.Zero, ColorUtil.White);
        }
    }
    
    private static void RenderScene()
    {
        Raylib.DrawCube(Vector3.Zero, 1, 1, 1, ColorUtil.Red);
        SceneManager.UpdateCurrentScene();
    }

    internal static void Stop()
    {
        Raylib.UnloadRenderTexture(gameTexture);
        Raylib.UnloadRenderTexture(editorTexture);
    }
}