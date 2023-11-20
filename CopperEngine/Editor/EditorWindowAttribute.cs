using ImGuiNET;

namespace CopperEngine.Editor;

[AttributeUsage(AttributeTargets.Class, Inherited = false)]
public class EditorWindowAttribute : Attribute
{
    internal string WindowName;
    public ImGuiWindowFlags WindowFlags = ImGuiWindowFlags.None;
    public bool StartingState = true;

    public EditorWindowAttribute(string windowName)
    {
        WindowName = windowName;
    }
}