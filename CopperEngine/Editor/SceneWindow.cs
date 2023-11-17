using System.Numerics;
using CopperEngine.Editor.DearImGui;
using ImGuiNET;

namespace CopperEngine.Editor;

[EditorWindow("Scene")]
public class SceneWindow : BaseEditorWindow
{
    internal override void PreRender()
    {
        ImGui.PushStyleVar(ImGuiStyleVar.WindowPadding, new Vector2(0, 0));
    }

    internal override void Render()
    {
        EngineEditor.EditorWindowFocused = ImGui.IsWindowFocused();
        rlImGui.ImageRenderTextureFit(EngineRenderer.editorTexture);
    }

    internal override void PostRender()
    {
        ImGui.PopStyleVar();
    }
}