using System.Numerics;

namespace CopperEngine;

public class EngineApplication
{
    protected internal virtual void Load() { }
    
    protected internal virtual void PreUpdate() { }
    protected internal virtual void Update() { }
    protected internal virtual void PostUpdate() { }
    
    protected internal virtual void WindowResize(Vector2 newSize) { }
    
    protected internal virtual void Stop() { }
}