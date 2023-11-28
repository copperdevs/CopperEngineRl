using System.Numerics;
using CopperEngine.Info;
using CopperEngine.Utils;
using Raylib_cs;
using MouseButton = CopperEngine.Info.MouseButton;
using rlMouseButton = Raylib_cs.MouseButton;

namespace CopperEngine.Editor.Components;

internal static class EditorCameraController 
{
    private static Camera3D Camera
    {
        get => EngineRenderer.EditorCamera.Camera3D;
        set => EngineRenderer.EditorCamera.Camera3D = value;
    }
    
    private const float FastMoveModifier = 3;
    private static float moveSpeed = 0.15f;

    private static Vector3 direction;
    private static Vector3 cameraFront;
    private static Vector3 cameraRight;
    private static Vector3 cameraUp;
    private static float pitch;
    private static float yaw;

    internal static bool IsMoving;

    internal static void Start()
    {
        var camera = Camera;
        Raylib.UpdateCamera(ref camera, CameraMode.CAMERA_CUSTOM);

        pitch = -0.6f;
        yaw = -2.45f;
        direction.X = MathF.Cos(yaw) * MathF.Cos(pitch);
        direction.Y = MathF.Sin(pitch);
        direction.Z = MathF.Sin(yaw) * MathF.Cos(pitch);
        cameraFront = Vector3.Normalize(direction);
        cameraRight = Vector3.Normalize(Vector3.Cross(camera.Up, cameraFront));
        cameraUp = Vector3.Cross(direction, cameraRight);

        camera.Target = Vector3.Add(camera.Position, cameraFront);
        camera.Position = new Vector3(10, 10, 10);
        
        Camera = camera;
        
        Input.RegisterInput(KeyboardButton.W, ButtonPressType.Down, () => {EngineRenderer.EditorCamera.Position += cameraFront * moveSpeed;});
        Input.RegisterInput(KeyboardButton.S, ButtonPressType.Down, () => {EngineRenderer.EditorCamera.Position -= cameraFront * moveSpeed;});
        Input.RegisterInput(KeyboardButton.A, ButtonPressType.Down, () => {EngineRenderer.EditorCamera.Position -= Vector3.Cross(cameraFront, cameraUp) * moveSpeed;});
        Input.RegisterInput(KeyboardButton.D, ButtonPressType.Down, () => {EngineRenderer.EditorCamera.Position += Vector3.Cross(cameraFront, cameraUp) * moveSpeed;});
        Input.RegisterInput(KeyboardButton.Space, ButtonPressType.Down, () => {EngineRenderer.EditorCamera.Position += cameraUp * moveSpeed;});
        Input.RegisterInput(KeyboardButton.LeftControl, ButtonPressType.Down, () => {EngineRenderer.EditorCamera.Position -= cameraUp * moveSpeed;});
    }
    
    internal static void Update()
    {
        var camera = Camera;
        
        moveSpeed = Input.IsKeyDown(KeyboardButton.RightShift) switch
        {
            true => 0.15f * FastMoveModifier,
            false => 0.15f
        };
        
        LookInput(ref camera);
        
        Camera = camera;
    }

    // BUG: cursor is being weird
    private static void LookInput(ref Camera3D camera)
    {
        var deltaTime = Time.DeltaTime;
        
        if (Input.IsMouseButtonDown(MouseButton.Right))
        {
            // Input.DisableCursor();
            var mouseDelta = Input.GetMouseDelta();
            yaw += (mouseDelta.X * 0.75f) * deltaTime;
            pitch += -(mouseDelta.Y * 0.75f) * deltaTime;

            if (pitch > 1.5)
                pitch = 1.5f;
            else if (pitch < -1.5)
                pitch = -1.5f;
        }
        else
        {
            // Input.EnableCursor();
        }

        direction.X = MathF.Cos(yaw) * MathF.Cos(pitch);
        direction.Y = MathF.Sin(pitch);
        direction.Z = MathF.Sin(yaw) * MathF.Cos(pitch);
        cameraFront = Vector3.Normalize(direction);
        cameraRight = Vector3.Normalize(Vector3.Cross(camera.Up, cameraFront));
        cameraUp = Vector3.Cross(direction, cameraRight);

        camera.Target = Vector3.Add(camera.Position, cameraFront);
    }
}