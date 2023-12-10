using ImGuiNET;

namespace CopperEngine.Editor;

[AttributeUsage(AttributeTargets.Class, Inherited = false)]
public class EditorWindowAttribute : Attribute
{
    internal readonly string WindowName;
    internal readonly ImGuiWindowFlags WindowFlags = ImGuiWindowFlags.None;
    public bool StartingState = true;

    public EditorWindowAttribute(string windowName)
    {
        WindowName = windowName;
    }

    public EditorWindowAttribute(string windowName, ImGuiWindowFlags windowFlags)
    {
        WindowName = windowName;
        WindowFlags = windowFlags;
    }
}