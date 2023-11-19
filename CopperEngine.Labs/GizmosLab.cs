using System.Numerics;
using CopperEngine.Logs;
using Raylib_CsLo;
using static Raylib_CsLo.Raylib;

namespace CopperEngine.Labs;

public static class GizmosLab
{
    private static Camera3D camera = new()
    {
        fovy = 45,
        position = Vector3.One*10,
        target = Vector3.Zero,
        up = Vector3.UnitY,
        projection_ = CameraProjection.CAMERA_PERSPECTIVE
    };

    private static Vector3 cubePosition = new();
    
    public static void Run()
    {
        SetConfigFlags(ConfigFlags.FLAG_MSAA_4X_HINT | ConfigFlags.FLAG_VSYNC_HINT | ConfigFlags.FLAG_WINDOW_RESIZABLE);
        InitWindow(650, 400, "gizmos");
        SetTargetFPS(144);

        SetCameraMode(camera, CameraMode.CAMERA_FREE);
        
		while (!WindowShouldClose())
		{
			UpdateCamera(ref camera);
			
			BeginDrawing();

			ClearBackground(RAYWHITE);

			BeginMode3D(camera);
			
			Gizmo.DrawTranslationGizmo(ref cubePosition, camera);
			Log.Info(GetMouseDelta());
			
			DrawCube(cubePosition, 1, 1, 1, GREEN);
			DrawCubeWires(cubePosition, 1, 1, 1, DARKGREEN);

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