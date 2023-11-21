using System.Numerics;
using CopperEngine.Components;
using ImGuizmoNET;
using Raylib_CsLo;

namespace CopperEngine.Testing;

public class TestComponent : GameComponent
{
    protected override void Update()
    {
        Raylib.DrawCube(Vector3.Zero, 1, 1, 1, Raylib.RED);
        Raylib.DrawCubeWires(Vector3.Zero, 1, 1, 1, Raylib.MAROON);
    }
}