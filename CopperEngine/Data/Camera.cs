using System.Numerics;
using CopperEngine.Utility;
using Raylib_cs;
using rlCamera = Raylib_cs.Camera3D;

namespace CopperEngine.Data;

public sealed class Camera
{
    internal rlCamera Camera3D = new(Vector3.Zero, Vector3.One, Vector3.UnitY, 45, CameraProjection.CAMERA_PERSPECTIVE);

    public Vector3 Position
    {
        get => Camera3D.Position;
        set => Camera3D.Position = value;
    }

    public Vector3 Target
    {
        get => Camera3D.Target;
        set => Camera3D.Target = value;
    }

    // public Vector3 Front = new(0.0f, 0.0f, -1.0f);
    public Vector3 Up
    {
        get => Camera3D.Up;
        set => Camera3D.Up = value;
    }

    public Vector3 Direction => Vector3.Normalize(Camera3D.Target - Position);

    // public float Yaw = -90f;
    // public float Pitch = 0f;
    public float Zoom
    {
        get => Camera3D.FovY;
        set => Camera3D.FovY = value;
    }

    public Vector2 ClippingPlane = new(0.1f, 100f);

    public static implicit operator rlCamera(Camera camera) => camera.Camera3D;

    public Matrix4x4 ViewMatrix
    {
        get => Matrix4x4.CreateLookAt(Position, Camera3D.Target, Up);
        set => SetViewMatrix(value);
    }

    private void SetViewMatrix(Matrix4x4 matrix)
    {
        Position = new Vector3(matrix.M41, matrix.M42, matrix.M43);
        Up = new Vector3(matrix.M21, matrix.M22, matrix.M23);
    }


    public Matrix4x4 ProjectionMatrix => Matrix4x4.CreatePerspectiveFieldOfView(
        MathUtil.DegreesToRadians(Zoom),
        (float)Raylib.GetScreenWidth() / (float)Raylib.GetScreenHeight(),
        ClippingPlane.X, ClippingPlane.Y);
}