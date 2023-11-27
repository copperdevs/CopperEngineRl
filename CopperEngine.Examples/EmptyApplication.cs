using System.Numerics;
using CopperEngine.Logs;

namespace CopperEngine.Examples;

public class EmptyApplication : EngineApplication
{
    protected override void WindowResize(Vector2 newSize)
    {
        Log.Info(newSize);
    }
}