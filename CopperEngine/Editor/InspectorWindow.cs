using System.Numerics;
using CopperEngine.Labs;
using CopperEngine.Utility;
using ImGuiNET;
using Raylib_CsLo;

namespace CopperEngine.Editor;

[EditorWindow("Inspector")]
public class InspectorWindow : BaseEditorWindow
{
    internal override void Render()
    {
        if (HierarchyWindow.CurrentTarget is null)
            return;

        if (ImGui.CollapsingHeader("Transform"))
        {
            var transformPosition = HierarchyWindow.CurrentTarget.Transform.Position;
            ImGui.DragFloat3("Position", ref transformPosition);
            HierarchyWindow.CurrentTarget.Transform.Position = transformPosition;

            var transformScale = HierarchyWindow.CurrentTarget.Transform.Scale;
            ImGui.DragFloat3("Scale", ref transformScale, 0.1f);
            HierarchyWindow.CurrentTarget.Transform.Scale = transformScale;


            var transformRotation = HierarchyWindow.CurrentTarget.Transform.Rotation.ToEulerAngles();
            ImGui.DragFloat3("Rotation", ref transformRotation);
            HierarchyWindow.CurrentTarget.Transform.Rotation = transformRotation.FromEulerAngles();
        }

        if (ImGui.Begin("Inspector Debug"))
        {
            var originalMousePos = Raylib.GetMousePosition();
            ImGui.DragFloat2("Original Mouse Position", ref originalMousePos);

            ImGui.Separator(); // --- //

            var screenSize = new Vector2(Raylib.GetScreenWidth(), Raylib.GetScreenHeight());
            ImGui.DragFloat2("Screen Size", ref screenSize);

            ImGui.Separator(); // --- //

            var newScreenSizeMin = SceneWindow.WindowPosition;
            var newScreenSizeMax = SceneWindow.WindowPosition + SceneWindow.WindowSize;
            
            ImGui.DragFloat2("New Screen Size Min", ref newScreenSizeMin);
            ImGui.DragFloat2("New Screen Size Max", ref newScreenSizeMax);

            ImGui.Separator(); // --- //

            // var mousePos = originalMousePos.Remap(Vector2.Zero, screenSize, newScreenSizeMax, newScreenSizeMin);
            var mousePos = (originalMousePos - newScreenSizeMin)*2;
            ImGui.DragFloat2("Mouse Position", ref mousePos);
            
            ImGui.End();
        }
    }

    internal static void RenderGizmos()
    {
        if (HierarchyWindow.CurrentTarget is null)
            return;

        var transformPosition = HierarchyWindow.CurrentTarget.Transform.Position;

        Raylib.BeginMode3D(EngineRenderer.editorCamera);
        Gizmo.DrawTranslationGizmo(ref transformPosition, EngineRenderer.editorCamera);
        Raylib.EndMode3D();

        HierarchyWindow.CurrentTarget.Transform.Position = transformPosition;
    }
}