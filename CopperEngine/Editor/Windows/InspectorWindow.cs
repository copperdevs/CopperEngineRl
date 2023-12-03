using CopperEngine.Editor.DearImGui;
using CopperEngine.Utils;
using ImGuiNET;

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
            ImGui.Indent();
            var transformPosition = HierarchyWindow.CurrentTarget.Transform.Position;
            ImGui.DragFloat3("Position", ref transformPosition);
            HierarchyWindow.CurrentTarget.Transform.Position = transformPosition;

            var transformScale = HierarchyWindow.CurrentTarget.Transform.Scale;
            ImGui.DragFloat3("Scale", ref transformScale, 0.1f);
            HierarchyWindow.CurrentTarget.Transform.Scale = transformScale;

            var transformRotation = MathUtil.ToEulerAngles(HierarchyWindow.CurrentTarget.Transform.Rotation);
            ImGui.DragFloat3("Rotation", ref transformRotation);
            // HierarchyWindow.CurrentTarget.Transform.Rotation = MathUtil.FromEulerAngles(transformRotation);
            ImGui.Unindent();
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