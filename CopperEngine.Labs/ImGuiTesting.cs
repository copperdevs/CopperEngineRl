using System.Numerics;
using CopperEngine.Data;
using CopperEngine.Editor.DearImGui;
using CopperEngine.Utility;
using ImGuiNET;
using Raylib_cs;
using Color = Raylib_cs.Color;

namespace CopperEngine.Labs;

public class ImGuiTesting
{

    private static RenderTexture2D gameTexture;
    private static Camera gameCamera = new()
    {
        Position = new Vector3(10, 10, 10),
        Target = Vector3.Zero
    };
    
    private static RenderTexture2D editorTexture;
    private static Camera editorCamera = new()
    {
        Position = new Vector3(-10, 10, -10),
        Target = Vector3.Zero
    };
    
    public static void Run()
    {
        Raylib.SetConfigFlags(ConfigFlags.FLAG_MSAA_4X_HINT | ConfigFlags.FLAG_VSYNC_HINT | ConfigFlags.FLAG_WINDOW_RESIZABLE);
        Raylib.InitWindow(650, 400, "imgui render texture testing");
        Raylib.SetTargetFPS(144);
        
        gameTexture = Raylib.LoadRenderTexture(Raylib.GetScreenWidth(), Raylib.GetScreenHeight());
        editorTexture = Raylib.LoadRenderTexture(Raylib.GetScreenWidth(), Raylib.GetScreenHeight());

        // rlImGui.Setup();

        while (!Raylib.WindowShouldClose())
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
            Raylib.BeginTextureMode(gameTexture);
            Raylib.ClearBackground(ColorUtil.DarkGray);
            Raylib.BeginMode3D(gameCamera);
            RenderScene();
            Raylib.EndMode3D();
            Raylib.EndTextureMode();
            
            // editor
            Raylib.BeginTextureMode(editorTexture);
            Raylib.ClearBackground(ColorUtil.DarkGray);
            Raylib.BeginMode3D(editorCamera);
            RenderScene();
            Raylib.EndMode3D();
            Raylib.EndTextureMode();
            
            // ui
            Raylib.ClearBackground(Color.DARKGRAY);
            
            // rlImGui.Begin();
            ImGui.ShowDemoWindow();
            
            
            ImGui.PushStyleVar(ImGuiStyleVar.WindowPadding, new Vector2(0, 0));

            if (ImGui.Begin("Game"))
            {
                // rlImGui.ImageRenderTextureFit(gameTexture);
                ImGui.End();
            }
            
            ImGui.PopStyleVar();
            
            ImGui.PushStyleVar(ImGuiStyleVar.WindowPadding, new Vector2(0, 0));

            if (ImGui.Begin("Editor"))
            {
                // rlImGui.ImageRenderTextureFit(editorTexture);
                ImGui.End();
            }
            
            ImGui.PopStyleVar();
            
            // rlImGui.End();

            Raylib.EndDrawing();
        }

        // rlImGui.Shutdown();
        Raylib.CloseWindow();
    }

    private static void RenderScene()
    {
        Raylib.DrawCube(Vector3.Zero, 1, 1, 1, ColorUtil.Red);
    }
}