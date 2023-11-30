using CopperEngine.Editor.Components;
using ImGuiNET;

namespace CopperEngine.Editor.Windows;

[EditorWindow("Scene Window Camera Info", StartingState = false)]
public class SceneWindowCameraInfo : BaseEditorWindow
{
    internal override void Render()
    {
        ImGui.Checkbox("Fast Move", ref EditorCameraController.fastMove);
        ImGui.DragFloat("Fast Move Modifier", ref EditorCameraController.FastMoveModifier);
        ImGui.DragFloat("Move Speed", ref EditorCameraController.moveSpeed);
        
        ImGui.DragFloat3("Direction", ref EditorCameraController.direction);
        ImGui.DragFloat3("Camera Front", ref EditorCameraController.cameraFront);
        ImGui.DragFloat3("Camera Right", ref EditorCameraController.cameraRight);
        ImGui.DragFloat3("Camera Up", ref EditorCameraController.cameraUp);
        ImGui.DragFloat("Pitch", ref EditorCameraController.pitch);
        ImGui.DragFloat("Yaw", ref EditorCameraController.yaw);

        ImGui.Checkbox("Is Moving", ref EditorCameraController.IsMoving);
        ImGui.Checkbox("Last Frame Is Moving", ref EditorCameraController.LastFrameIsMoving);
    }
}