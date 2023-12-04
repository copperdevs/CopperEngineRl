using ImGuiNET;

namespace CopperEngine.Editor.Windows;

[EditorWindow("Physics Info", StartingState = false)]
public class PhysicsInfo : BaseEditorWindow
{
    internal override void Render()
    {
        ImGui.DragFloat("Fixed Time Step", ref EnginePhysics.FixedTimeStep);
        ImGui.DragFloat("Fixed Timer", ref EnginePhysics.FixedTimer);
    }
}