using ImGuiNET;

namespace CopperEngine.Editor;

public abstract class BaseEditorWindow
{
    internal virtual void Start() { }
    internal virtual void Update() { }
    internal virtual void PreRender() { }
    internal virtual void Render() { }
    internal virtual void PostRender() { }
    internal virtual void Stop() { }
}