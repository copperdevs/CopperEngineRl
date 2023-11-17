using ImGuiNET;

namespace CopperEngine.Editor;

[AttributeUsage(AttributeTargets.Class, Inherited = false)]
public class EditorWindowAttribute : Attribute
{
    internal string WindowName;
    internal ImGuiWindowFlags WindowFlags;

    public EditorWindowAttribute(string windowName, ImGuiWindowFlags windowFlags = ImGuiWindowFlags.None)
    {
        WindowName = windowName;
        WindowFlags = windowFlags;
    }
}