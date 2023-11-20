using CopperEngine.Utils;
using ImGuiNET;

namespace CopperEngine.Editor.Windows;

[EditorWindow("Renderer Info", StartingState = false)]
public class RendererInfoWindow : BaseEditorWindow
{
    internal override void Render()
    {
        if(ImGui.CollapsingHeader("Cameras"))
        {
            ImGui.Indent();

            if (ImGui.CollapsingHeader("Editor##camera"))
            {
                ImGui.Indent();
                EditorUtil.DragMatrix4X4("View Matrix##editor_camera", EngineRenderer.editorCamera.ViewMatrix);
                EditorUtil.DragMatrix4X4("Projection Matrix##editor_camera", EngineRenderer.editorCamera.ProjectionMatrix);
                ImGui.Unindent();
            }

            if (ImGui.CollapsingHeader("Game##camera"))
            {
                ImGui.Indent();
                EditorUtil.DragMatrix4X4("View Matrix##game_camera", EngineRenderer.gameCamera.ViewMatrix);
                EditorUtil.DragMatrix4X4("Projection Matrix##game_camera", EngineRenderer.gameCamera.ProjectionMatrix);
                ImGui.Unindent();
            }
            
            ImGui.Unindent();
        }
    }
}