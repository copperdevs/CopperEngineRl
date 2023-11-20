using System.Numerics;
using CopperEngine.Data;
using CopperEngine.Info;
using CopperEngine.Utility;
using CopperEngine.Utils;
using Raylib_CsLo;

namespace CopperEngine.Editor.Components;

internal static class EditorCameraController 
{
    private static Camera3D Camera
    {
        get => EngineRenderer.editorCamera;
        set => EngineRenderer.editorCamera.Camera3D = value;
    }

    private static bool fastMove = false;
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
        Raylib.SetCameraMode(Camera, CameraMode.CAMERA_CUSTOM);

        pitch = -0.6f;
        yaw = -2.45f;
        direction.X = MathF.Cos(yaw) * MathF.Cos(pitch);
        direction.Y = MathF.Sin(pitch);
        direction.Z = MathF.Sin(yaw) * MathF.Cos(pitch);
        cameraFront = Vector3.Normalize(direction);
        cameraRight = Vector3.Normalize(Vector3.Cross(camera.up, cameraFront));
        cameraUp = Vector3.Cross(direction, cameraRight);

        camera.target = Vector3.Add(camera.position, cameraFront);
        camera.position = new Vector3(10, 10, 10);
        
        Camera = camera;
    }
    
    internal static void Update()
    {
        var camera = Camera;
        
        
        SpeedControls();
        
        MoveInput(ref camera);
        
        // IsMoving = Input.IsMouseButtonDown(MouseButton.MOUSE_BUTTON_RIGHT);

        // if (!IsMoving)
        // {
            // Raylib.EnableCursor();
            // return;
        // }
        
        LookInput(ref camera);
        
        Camera = camera;
    }

    private static void SpeedControls()
    {
        var targetMoveSpeed = 0f;
        
        var mouseWheelMovement = Input.GetMouseWheelMove();
        if (mouseWheelMovement != 0)
            targetMoveSpeed += mouseWheelMovement / 25;

        if (targetMoveSpeed < 0.15f)
            targetMoveSpeed = 0.15f;
        if (targetMoveSpeed > 1)
            targetMoveSpeed = 1;

        fastMove = Input.IsKeyDown(KeyboardKey.KEY_LEFT_SHIFT);
        if (fastMove)
            targetMoveSpeed *= FastMoveModifier;

        moveSpeed = targetMoveSpeed;
    }

    private static void MoveInput(ref Camera3D camera)
    {
        if (Input.IsKeyDown(KeyboardKey.KEY_W))
        {
            camera.position = Vector3.Add(camera.position, MathUtil.Scale(cameraFront, moveSpeed));
        }

        if (Input.IsKeyDown(KeyboardKey.KEY_S))
        {
            camera.position = Vector3.Subtract(camera.position, MathUtil.Scale(cameraFront, moveSpeed));
        }

        if (Input.IsKeyDown(KeyboardKey.KEY_A))
        {
            camera.position = Vector3.Subtract(camera.position,
                MathUtil.Scale(Vector3.Cross(cameraFront, cameraUp), moveSpeed));
        }

        if (Input.IsKeyDown(KeyboardKey.KEY_D))
        {
            camera.position = Vector3.Add(camera.position,
                MathUtil.Scale(Vector3.Cross(cameraFront, cameraUp), moveSpeed));
        }

        if (Input.IsKeyDown(KeyboardKey.KEY_SPACE))
        {
            camera.position = Vector3.Add(camera.position, MathUtil.Scale(cameraUp, moveSpeed));
        }

        if (Input.IsKeyDown(KeyboardKey.KEY_LEFT_CONTROL))
        {
            camera.position = Vector3.Subtract(camera.position, MathUtil.Scale(cameraUp, moveSpeed));
        }
    }

    private static void LookInput(ref Camera3D camera)
    {
        var deltaTime = Time.DeltaTime;
        
        if (Input.IsMouseButtonDown(MouseButton.MOUSE_BUTTON_RIGHT))
        {
            Input.DisableCursor();
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
            Input.EnableCursor();
        }

        direction.X = MathF.Cos(yaw) * MathF.Cos(pitch);
        direction.Y = MathF.Sin(pitch);
        direction.Z = MathF.Sin(yaw) * MathF.Cos(pitch);
        cameraFront = Vector3.Normalize(direction);
        cameraRight = Vector3.Normalize(Vector3.Cross(camera.up, cameraFront));
        cameraUp = Vector3.Cross(direction, cameraRight);

        camera.target = Vector3.Add(camera.position, cameraFront);
    }
}