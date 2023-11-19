using CopperEngine.Components;
using Raylib_CsLo;

namespace CopperEngine.Testing;

public class TestComponent : GameComponent
{
    protected override void Update()
    {
        Raylib.DrawCube(Transform.Position, 1, 1, 1, Raylib.RED);
        Raylib.DrawCubeWires(Transform.Position, 1, 1, 1, Raylib.MAROON);
    }
}