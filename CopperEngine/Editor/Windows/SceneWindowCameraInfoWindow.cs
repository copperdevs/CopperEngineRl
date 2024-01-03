using CopperEngine.Editor.Components;
using ImGuiNET;

namespace CopperEngine.Editor.Windows;

[EditorWindow("Scene Window Camera Info", StartingState = false)]
internal sealed class SceneWindowCameraInfoWindow : BaseEditorWindow
{
    internal override void Render()
    {
        if (ImGui.CollapsingHeader("Camera Controller Values"))
        {
            ImGui.Indent();
            ImGui.Checkbox("Fast Move##cameracontrollervalues", ref EditorCameraController.FastMove);
            ImGui.DragFloat("Fast Move Modifier##cameracontrollervalues", ref EditorCameraController.FastMoveModifier);
            ImGui.DragFloat("Move Speed##cameracontrollervalues", ref EditorCameraController.MoveSpeed);
            ImGui.DragFloat("Current Move Speed##cameracontrollervalues", ref EditorCameraController.CurrentMoveSpeed);
            ImGui.Separator();
            ImGui.DragFloat3("Direction##cameracontrollervalues", ref EditorCameraController.Direction);
            ImGui.DragFloat3("Camera Front##cameracontrollervalues", ref EditorCameraController.CameraFront);
            ImGui.DragFloat3("Camera Right##cameracontrollervalues", ref EditorCameraController.CameraRight);
            ImGui.DragFloat3("Camera Up##cameracontrollervalues", ref EditorCameraController.CameraUp);
            ImGui.DragFloat("Pitch##cameracontrollervalues", ref EditorCameraController.Pitch);
            ImGui.DragFloat("Yaw##cameracontrollervalues", ref EditorCameraController.Yaw);
            ImGui.Separator();
            ImGui.Checkbox("Is Looking##cameracontrollervalues", ref EditorCameraController.IsLooking);
            ImGui.Unindent();
        }

        if (ImGui.CollapsingHeader("Camera Values"))
        {
            ImGui.Indent();
            ImGui.DragFloat3("Position##cameravalues", ref EngineRenderer.EditorCamera.Camera3D.Position);
            ImGui.DragFloat3("Target##cameravalues", ref EngineRenderer.EditorCamera.Camera3D.Target);
            ImGui.DragFloat3("Up##cameravalues", ref EngineRenderer.EditorCamera.Camera3D.Up);
            ImGui.DragFloat("FovY##cameravalues", ref EngineRenderer.EditorCamera.Camera3D.FovY);
            ImGui.Unindent();
        }
    }
}