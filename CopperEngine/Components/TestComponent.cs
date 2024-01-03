using System.Numerics;
using Raylib_cs;
using Color = CopperEngine.Data.Color;

namespace CopperEngine.Components;

public sealed class TestComponent : Component
{
    public Color CubeColor = Color.Red;
    public Color CubeOutlineColor = Color.Maroon;
    
    protected internal override void Update()
    {
        Raylib.DrawCube(Vector3.Zero, 1, 1, 1, CubeColor);
        Raylib.DrawCubeWires(Vector3.Zero, 1, 1, 1, CubeOutlineColor);
    }
}