using System.Numerics;
using CopperEngine.Data;
using CopperEngine.Info;
using CopperEngine.Logs;
using CopperEngine.Utility;
using CopperEngine.Utils;
using Raylib_cs;
using MouseButton = CopperEngine.Info.MouseButton;
using rlMouseButton = Raylib_cs.MouseButton;

namespace CopperEngine.Editor.Components;

internal static class EditorCameraController 
{
    private static Camera3D Camera
    {
        get => EngineRenderer.EditorCamera;
        set => EngineRenderer.EditorCamera.Camera3D = value;
    }

    internal static bool FastMove = false;
    internal static float FastMoveModifier = 3;
    internal static float MoveSpeed = 0.15f;
    internal static float CurrentMoveSpeed;

    internal static Vector3 Direction;
    internal static Vector3 CameraFront;
    internal static Vector3 CameraRight;
    internal static Vector3 CameraUp;
    internal static float Pitch;
    internal static float Yaw;

    internal static bool IsLooking;


    internal static void Start()
    {
        var camera = Camera;
        Raylib.UpdateCamera(ref camera, CameraMode.CAMERA_CUSTOM);

        Pitch = -0.6f;
        Yaw = -2.45f;
        Direction.X = MathF.Cos(Yaw) * MathF.Cos(Pitch);
        Direction.Y = MathF.Sin(Pitch);
        Direction.Z = MathF.Sin(Yaw) * MathF.Cos(Pitch);
        CameraFront = Vector3.Normalize(Direction);
        CameraRight = Vector3.Normalize(Vector3.Cross(camera.Up, CameraFront));
        CameraUp = Vector3.Cross(Direction, CameraRight);

        camera.Target = Vector3.Add(camera.Position, CameraFront);
        camera.Position = new Vector3(10, 10, 10);
        
        Camera = camera;
    }
    
    internal static void Update()
    {
        var camera = Camera;
        // Raylib.UpdateCamera(ref camera, CameraMode.CAMERA_CUSTOM);
        
        SpeedControls();

        MoveInput(ref camera); 
        IsLooking = LookInput();
        UpdateValues(ref camera);
        
        Camera = camera;
    }

    private static void SpeedControls()
    {
        FastMove = Input.IsKeyDown(KeyboardButton.LeftShift);

        var targetMoveSpeed = FastMove switch
        {
            true => MoveSpeed * FastMoveModifier,
            false => MoveSpeed
        };

        CurrentMoveSpeed = targetMoveSpeed;
    }
    
    private static void MoveInput(ref Camera3D camera)
    {
        if (Input.IsKeyDown(KeyboardButton.W))
        {
            camera.Position += CameraFront * CurrentMoveSpeed;
        }

        if (Input.IsKeyDown(KeyboardButton.S))
        {
            camera.Position -= CameraFront * CurrentMoveSpeed;
        }

        if (Input.IsKeyDown(KeyboardButton.A))
        {
            camera.Position -= Vector3.Cross(CameraFront, CameraUp) * CurrentMoveSpeed;
        }

        if (Input.IsKeyDown(KeyboardButton.D))
        {
            camera.Position += Vector3.Cross(CameraFront, CameraUp) * CurrentMoveSpeed;
        }

        if (Input.IsKeyDown(KeyboardButton.Space))
        {
            camera.Position += CameraUp * CurrentMoveSpeed;
        }

        if (Input.IsKeyDown(KeyboardButton.LeftControl))
        {
            camera.Position += -CameraUp * CurrentMoveSpeed;
        }
    }
    private static bool LookInput()
    {
        var isLooking = Input.IsMouseButtonDown(MouseButton.Right);

        if (!isLooking) 
            return false;
        
        var deltaTime = Time.DeltaTime;
        var mouseDelta = Input.GetMouseDelta();
        
        Yaw += (mouseDelta.X * 0.75f) * deltaTime;
        Pitch += -(mouseDelta.Y * 0.75f) * deltaTime;

        Pitch = MathUtil.Clamp(Pitch, -1.5f, 1.5f);

        return true;
    }

    private static void UpdateValues(ref Camera3D camera)
    {
        
        Direction.X = MathF.Cos(Yaw) * MathF.Cos(Pitch);
        Direction.Y = MathF.Sin(Pitch);
        Direction.Z = MathF.Sin(Yaw) * MathF.Cos(Pitch);
        CameraFront = Vector3.Normalize(Direction);
        CameraRight = Vector3.Normalize(Vector3.Cross(camera.Up, CameraFront));
        CameraUp = Vector3.Cross(Direction, CameraRight);
        camera.Target = Vector3.Add(camera.Position, CameraFront);
    }
}