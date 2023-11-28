using System.Numerics;
using CopperEngine.Editor;
using CopperEngine.Logs;
using Raylib_cs;
using static Raylib_cs.Raylib;

namespace CopperEngine.Labs;

public static class GizmosLab
{
    private static Camera3D camera = new()
    {
        FovY = 45,
        Position = Vector3.One*10,
        Target = Vector3.Zero,
        Up = Vector3.UnitY,
        Projection = CameraProjection.CAMERA_PERSPECTIVE
    };

    private static Vector3 cubePosition = new();
    
    public static void Run()
    {
        SetConfigFlags(ConfigFlags.FLAG_MSAA_4X_HINT | ConfigFlags.FLAG_VSYNC_HINT | ConfigFlags.FLAG_WINDOW_RESIZABLE);
        InitWindow(650, 400, "gizmos");
        SetTargetFPS(144);

        
		while (!WindowShouldClose())
		{
			UpdateCamera(ref camera, CameraMode.CAMERA_FREE);
			
			BeginDrawing();

			ClearBackground(Color.RAYWHITE);

			BeginMode3D(camera);
			
			Gizmo.DrawTranslationGizmo(ref cubePosition, camera);
			Log.Info(GetMouseDelta());
			
			DrawCube(cubePosition, 1, 1, 1, Color.GREEN);
			DrawCubeWires(cubePosition, 1, 1, 1, Color.DARKGREEN);

			DrawGrid(10, 1.0f);

			EndMode3D();

			DrawFPS(10, 10);

			EndDrawing();
			//----------------------------------------------------------------------------------
		}

		// De-Initialization
		//--------------------------------------------------------------------------------------
		CloseWindow();
    }
}