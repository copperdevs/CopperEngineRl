using System.Numerics;
using CopperEngine.Data;
using CopperEngine.Editor.DearImGui;
using CopperEngine.Utility;
using ImGuiNET;
using ImGuizmoNET;

namespace CopperEngine.Editor;

public static class Gizmo
{
    internal static OPERATION operation = OPERATION.TRANSLATE;
    internal static MODE mode = MODE.LOCAL;
    
    public static bool gimbalGrabbed;
    public static bool overGimbal;

    private static (Matrix4x4, Matrix4x4) BaseGizmo()
    {
        ImGuizmo.SetDrawlist();
        
        var position = ImGui.GetWindowPos();
        var size = ImGui.GetWindowSize();
        
        var view = EngineRenderer.editorCamera.ViewMatrix;
        var proj = EngineRenderer.editorCamera.ProjectionMatrix;
        ImGuizmo.Enable(true);
        ImGuizmo.SetOrthographic(false);
        ImGuizmo.SetRect(position.X, position.Y, size.X, size.Y);
        
        ImGuizmo.SetID(0);

        return (view, proj);
    }
    
    internal static void Manipulate(ref Transform transform)
    {
        var camera = BaseGizmo();
        var localTransform = transform.Matrix;

        if (ImGui.IsKeyPressed(ImGuiKey.E)) 
            operation = OPERATION.TRANSLATE;
        if (ImGui.IsKeyPressed(ImGuiKey.R))
            operation = operation is OPERATION.SCALE ? OPERATION.BOUNDS : OPERATION.SCALE;
        if (ImGui.IsKeyPressed(ImGuiKey.T))
            operation = operation is OPERATION.ROTATE ? OPERATION.ROTATE_SCREEN : OPERATION.ROTATE;

        if (Guizmo.Manipulate(ref camera.Item1, ref camera.Item2, operation, mode, ref localTransform))
        {
            gimbalGrabbed = true;
            transform.Matrix = localTransform;
        }
        
        if (!ImGuizmo.IsUsing() && gimbalGrabbed)
        {
            gimbalGrabbed = false;
        }
        overGimbal = ImGuizmo.IsOver();
    }

    private static Matrix4x4 viewMatrix = Matrix4x4.Identity;
    
    internal static void ViewManipulate()
    {
        var (view, _) = BaseGizmo();

        Guizmo.ViewManipulate(ref viewMatrix, 25, ImGui.GetWindowPos() + (Vector2.One * 25), Vector2.One*100, 100);
        EngineRenderer.editorCamera.ViewMatrix = viewMatrix.ToColumnMajor();
    }
}