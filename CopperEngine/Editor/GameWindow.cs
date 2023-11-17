using System.Numerics;
using CopperEngine.Editor.DearImGui;
using ImGuiNET;

namespace CopperEngine.Editor;

[EditorWindow("Game")]
public class GameWindow : BaseEditorWindow
{
    internal override void PreRender()
    {
        ImGui.PushStyleVar(ImGuiStyleVar.WindowPadding, new Vector2(0, 0));
    }

    internal override void Render()
    {
        EngineEditor.GameWindowFocused = ImGui.IsWindowFocused();
        rlImGui.ImageRenderTextureFit(EngineRenderer.gameTexture);
    }

    internal override void PostRender()
    {
        ImGui.PopStyleVar();
    }
}