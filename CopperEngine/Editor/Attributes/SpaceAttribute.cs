using System.Numerics;
using ImGuiNET;

namespace CopperEngine.Editor.Attributes;

[AttributeUsage(AttributeTargets.Field, Inherited = false, AllowMultiple = false)]
public class SpaceAttribute : Attribute
{
    private Vector2 spacing;

    public SpaceAttribute()
    {
        spacing = new Vector2(0, 20);
    }

    public SpaceAttribute(float space)
    {
        spacing = new Vector2(0, space);
    }

    internal void Render()
    {
        ImGui.Dummy(spacing);
    }
}