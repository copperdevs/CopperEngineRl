using System.Numerics;
using CopperEngine.Components;
using ImGuizmoNET;
using Raylib_CsLo;

namespace CopperEngine.Testing;

public class TestComponent : GameComponent
{
    protected override void Update()
    {
        RlGl.rlPushMatrix();
        RlGl.rlTranslatef(Transform.Position.X, Transform.Position.Y, Transform.Position.Z);
        RlGl.rlScalef(Transform.Scale.X, Transform.Scale.Y, Transform.Scale.Z);
        RlGl.rlRotatef(Transform.Rotation.W, Transform.Rotation.X, Transform.Rotation.Y, Transform.Rotation.Z);
        Raylib.DrawCube(Vector3.Zero, 1, 1, 1, Raylib.RED);
        Raylib.DrawCubeWires(Vector3.Zero, 1, 1, 1, Raylib.MAROON);
        RlGl.rlPopMatrix();
    }
}