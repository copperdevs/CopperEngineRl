using CopperEngine.Utility;
using ImGuiNET;

namespace CopperEngine.Editor.Windows;

[EditorWindow("Renderer Info", StartingState = false)]
internal sealed class RendererInfoWindow : BaseEditorWindow
{
    internal override void Render()
    {
        if (ImGui.CollapsingHeader("Cameras"))
        {
            ImGui.Indent();

            if (ImGui.CollapsingHeader("Editor##camera"))
            {
                ImGui.Indent();
                EditorUtil.DragMatrix4X4("View Matrix##editor_camera", EngineRenderer.EditorCamera.ViewMatrix);
                EditorUtil.DragMatrix4X4("Projection Matrix##editor_camera",
                    EngineRenderer.EditorCamera.ProjectionMatrix);
                ImGui.Unindent();
            }

            if (ImGui.CollapsingHeader("Game##camera"))
            {
                ImGui.Indent();
                EditorUtil.DragMatrix4X4("View Matrix##game_camera", EngineRenderer.GameCamera.ViewMatrix);
                EditorUtil.DragMatrix4X4("Projection Matrix##game_camera", EngineRenderer.GameCamera.ProjectionMatrix);
                ImGui.Unindent();
            }

            ImGui.Unindent();
        }
    }
}