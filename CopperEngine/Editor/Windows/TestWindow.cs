using ImGuiNET;

namespace CopperEngine.Editor.Windows;

[EditorWindow("Test Window", StartingState = false)]
public class TestWindow : BaseEditorWindow
{
    internal override void Render()
    {
        ImGui.Text("tool tip example");
        if (ImGui.BeginItemTooltip())
        {
            ImGui.PushTextWrapPos(ImGui.GetFontSize() * 35.0f);
            ImGui.TextUnformatted("| tooltip | tooltip | tooltip | tooltip ");
            ImGui.PopTextWrapPos();
            ImGui.EndTooltip();
        }


    }
}