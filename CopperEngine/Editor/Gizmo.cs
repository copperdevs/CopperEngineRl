using System.Numerics;
using CopperEngine.Data;
using CopperEngine.Editor.DearImGui;
using CopperEngine.Editor.Windows;
using ImGuiNET;
using ImGuizmoNET;
using static Raylib_CsLo.Raylib;

namespace CopperEngine.Editor;

public static class Gizmo
{
    internal static OPERATION operation = OPERATION.TRANSLATE;
    internal static MODE mode = MODE.LOCAL;
    
    public static bool gimbalGrabbed;
    public static bool overGimbal;
    
    internal static void Manipulate(ref Transform transform)
    {
        ImGuizmo.SetDrawlist();
        
        var position = ImGui.GetWindowPos();
        var size = ImGui.GetWindowSize();
        
        var view = EngineRenderer.editorCamera.ViewMatrix;
        var proj = EngineRenderer.editorCamera.ProjectionMatrix;
        ImGuizmo.Enable(true);
        ImGuizmo.SetOrthographic(false);
        ImGuizmo.SetRect(position.X, position.Y, size.X, size.Y);
        
        
        var localTransform = transform.Matrix;
        
        // var matrix = Matrix4x4.Identity * Matrix4x4.CreateTranslation(transform.Position);
        // Guizmo.DrawGrid(ref view, ref proj, ref matrix, 10);
        // Guizmo.DrawCubes(ref view, ref proj, ref localTransform, 1);
        ImGuizmo.SetID(0);

        
        if (Guizmo.Manipulate(ref view, ref proj, operation, mode, ref localTransform))
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
}