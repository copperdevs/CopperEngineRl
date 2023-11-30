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

    internal static bool fastMove = false;
    internal static float FastMoveModifier = 3;
    internal static float moveSpeed = 0.15f;

    internal static Vector3 direction;
    internal static Vector3 cameraFront;
    internal static Vector3 cameraRight;
    internal static Vector3 cameraUp;
    internal static float pitch;
    internal static float yaw;

    internal static bool IsMoving;
    internal static bool LastFrameIsMoving;

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
    }
    
    internal static void Update()
    {
        var camera = Camera;
        // Raylib.UpdateCamera(ref camera, CameraMode.CAMERA_CUSTOM);
        
        
        SpeedControls();

        LastFrameIsMoving = IsMoving;
        IsMoving = MoveInput(ref camera) || LookInput(ref camera);
        
        if (LastFrameIsMoving && IsMoving)
        {
            // Raylib.DisableCursor();
            Raylib.HideCursor();
        }

        if (LastFrameIsMoving && !IsMoving)
        {
            // Raylib.EnableCursor();
            Raylib.ShowCursor();
        }

        // if (!IsMoving)
        // {
            // Raylib.EnableCursor();
            // return;
        // }
        
        
        Camera = camera;
    }

    private static void SpeedControls()
    {
        var targetMoveSpeed = 0f;
        
        var mouseWheelMovement = Input.GetMouseWheelMove();
        if (mouseWheelMovement != 0)
            targetMoveSpeed += mouseWheelMovement / 25f;

        if (targetMoveSpeed < 0.15f)
            targetMoveSpeed = 0.15f;
        if (targetMoveSpeed > 1)
            targetMoveSpeed = 1;

        fastMove = Input.IsKeyDown(KeyboardButton.LeftShift);
        if (fastMove)
            targetMoveSpeed *= FastMoveModifier;

        moveSpeed = targetMoveSpeed;
    }

    
    // BUG: what the fuck
    private static bool MoveInput(ref Camera3D camera)
    {
        var moved = false;
        
        if (Input.IsKeyDown(KeyboardButton.W))
        {
            camera.Position += cameraFront * moveSpeed;
            moved = true;
        }

        if (Input.IsKeyDown(KeyboardButton.S))
        {
            camera.Position -= cameraFront * moveSpeed;
            moved = true;
        }

        if (Input.IsKeyDown(KeyboardButton.A))
        {
            camera.Position -= Vector3.Cross(cameraFront, cameraUp) * moveSpeed;
            moved = true;
        }

        if (Input.IsKeyDown(KeyboardButton.D))
        {
            camera.Position += Vector3.Cross(cameraFront, cameraUp) * moveSpeed;
            moved = true;
        }

        if (Input.IsKeyDown(KeyboardButton.Space))
        {
            camera.Position += cameraUp * moveSpeed;
            moved = true;
        }

        if (Input.IsKeyDown(KeyboardButton.LeftControl))
        {
            camera.Position += cameraUp * moveSpeed;
            moved = true;
        }

        return moved;
    }

    private static bool LookInput(ref Camera3D camera)
    {
        var deltaTime = Time.DeltaTime;
        var isMoving = Input.IsMouseButtonDown(MouseButton.Right);

        // if (isMoving is false)
            // return false;
        
        if (isMoving)
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

        return isMoving;
    }
}