﻿using System.Numerics;
using CopperEngine.Data;
using CopperEngine.Editor.DearImGui;
using CopperEngine.Utility;
using ImGuiNET;
using ImGuizmoNET;

namespace CopperEngine.Editor;

internal static class Gizmo
{
    private static OPERATION operation = OPERATION.TRANSLATE;
    private const MODE Mode = MODE.LOCAL;

    private static bool gimbalGrabbed;
    private static bool overGimbal;

    private static (Matrix4x4, Matrix4x4) BaseGizmo()
    {
        ImGuizmo.SetDrawlist();

        var position = ImGui.GetWindowPos();
        var size = ImGui.GetWindowSize();

        var view = EngineRenderer.EditorCamera.ViewMatrix;
        var proj = EngineRenderer.EditorCamera.ProjectionMatrix;
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

        if (Guizmo.Manipulate(ref camera.Item1, ref camera.Item2, operation, Mode, ref localTransform))
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

    internal static void ViewManipulate()
    {
        var (view, _) = BaseGizmo();

        Guizmo.ViewManipulate(ref view, 25, ImGui.GetWindowPos() + (Vector2.One * 25), Vector2.One * 100, 100);
        EngineRenderer.EditorCamera.ViewMatrix = view.ToColumnMajor();
    }
}