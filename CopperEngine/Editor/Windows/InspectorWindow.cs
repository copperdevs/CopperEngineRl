using CopperEngine.Editor.DearImGui;
using CopperEngine.Utility;
using ImGuiNET;
using Raylib_CsLo;

namespace CopperEngine.Editor.Windows;

[EditorWindow("Inspector", StartingState = true)]
public class InspectorWindow : BaseEditorWindow
{
    internal static bool transformOpen = true;
    
    internal override void Render()
    {
        if (HierarchyWindow.CurrentTarget is null)
            return;

        transformOpen = ImGui.CollapsingHeader("Transform");
        if (transformOpen)
        {
            var transformPosition = HierarchyWindow.CurrentTarget.Transform.Position;
            ImGui.DragFloat3("Position", ref transformPosition);
            HierarchyWindow.CurrentTarget.Transform.Position = transformPosition;

            var transformScale = HierarchyWindow.CurrentTarget.Transform.Scale;
            ImGui.DragFloat3("Scale", ref transformScale, 0.1f);
            HierarchyWindow.CurrentTarget.Transform.Scale = transformScale;

            var transformRotation = RayMath.QuaternionToEuler(HierarchyWindow.CurrentTarget.Transform.Rotation);
            // var transformRotation = HierarchyWindow.CurrentTarget.Transform.Rotation.ToEulerAngles();
            ImGui.DragFloat3("Rotation", ref transformRotation);
            // HierarchyWindow.CurrentTarget.Transform.Rotation = transformRotation.FromEulerAngles();
            HierarchyWindow.CurrentTarget.Transform.Rotation =
                RayMath.QuaternionFromEuler(transformRotation.X, transformRotation.Y, transformRotation.Z);
        }

        for (var index = 0; index < HierarchyWindow.CurrentTarget.GameComponents.Count; index++)
        {
            var component = HierarchyWindow.CurrentTarget.GameComponents[index];
            
            if (ImGui.CollapsingHeader($"{component.GetType().Name}##{index}"))
            {
                ImGui.Indent();
                
                ImGuiReflection.RenderValues(component);
                component.EditorUpdate();
                HierarchyWindow.CurrentTarget.GameComponents[index] = component;
                
                ImGui.Unindent();
            }
        }
    }
}