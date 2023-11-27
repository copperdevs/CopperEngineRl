using System.Numerics;
using CopperEngine.Components;
using ImGuizmoNET;
using Raylib_CsLo;
using Color = CopperEngine.Data.Color;

namespace CopperEngine.Testing;

public class TestComponent : GameComponent
{
    public Color CubeColor = Color.Red;
    public Color CubeOutlineColor = Color.Maroon;
    
    protected override void Update()
    {
        Raylib.DrawCube(Vector3.Zero, 1, 1, 1, CubeColor);
        Raylib.DrawCubeWires(Vector3.Zero, 1, 1, 1, CubeOutlineColor);
    }
}