using System.Numerics;
using CopperEngine.Utils;
using Raylib_CsLo;

using RaylibCamera = Raylib_CsLo.Camera3D;

namespace CopperEngine.Data;

public class Camera
{
    private RaylibCamera Camera3D = new(Vector3.Zero, Vector3.One, Vector3.UnitY, 45, CameraProjection.CAMERA_PERSPECTIVE);

    public Vector3 Position
    {
        get => Camera3D.position;
        set => Camera3D.position = value;
    }

    public Vector3 Target
    {
        get => Camera3D.target;
        set => Camera3D.target = value;
    }

    // public Vector3 Front = new(0.0f, 0.0f, -1.0f);
    public Vector3 Up => Camera3D.up;
    public Vector3 Direction => Vector3.Normalize(Camera3D.target - Position);

    // public float Yaw = -90f;
    // public float Pitch = 0f;
    public float Zoom
    {
        get => Camera3D.fovy;
        set => Camera3D.fovy = value;
    }

    public Vector2 ClippingPlane = new(0.1f, 100f);

    public static implicit operator RaylibCamera(Camera camera) => camera.Camera3D;
    
    public Matrix4x4 ViewMatrix => Matrix4x4.CreateLookAt(Position, Camera3D.target, Up);
    
    
    public Matrix4x4 ProjectionMatrix => Matrix4x4.CreatePerspectiveFieldOfView(
        MathUtil.DegreesToRadians(Zoom), 
        (float)Raylib.GetScreenWidth() / (float)Raylib.GetScreenHeight(), 
        ClippingPlane.X, ClippingPlane.Y);
}
