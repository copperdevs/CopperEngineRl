using System.Numerics;
using ImGuizmoNET;

namespace CopperEngine.Editor.DearImGui;

internal static class Guizmo
{
    public static unsafe void DrawGrid(
        ref Matrix4x4 view,
        ref Matrix4x4 projection,
        ref Matrix4x4 matrix,
        float gridSize)
    {
        fixed (Matrix4x4* view1 = &view)
        fixed (Matrix4x4* projection1 = &projection)
        fixed (Matrix4x4* matrix1 = &matrix)
            ImGuizmoNative.ImGuizmo_DrawGrid((float*)view1, (float*)projection1, (float*)matrix1, gridSize);
    }

    public static unsafe void DrawCubes(
        ref Matrix4x4 view,
        ref Matrix4x4 projection,
        ref Matrix4x4 matrices,
        int matrixCount)
    {
        fixed (Matrix4x4* view1 = &view)
        fixed (Matrix4x4* projection1 = &projection)
        fixed (Matrix4x4* matrices1 = &matrices)
            ImGuizmoNative.ImGuizmo_DrawCubes((float*)view1, (float*)projection1, (float*)matrices1, matrixCount);
    }

    public static unsafe bool Manipulate(
        ref Matrix4x4 view,
        ref Matrix4x4 projection,
        OPERATION operation,
        MODE mode,
        ref Matrix4x4 matrix,
        ref Matrix4x4 deltaMatrix,
        ref float snap,
        ref float localBounds,
        ref float boundsSnap)
    {
        fixed (Matrix4x4* view1 = &view)
        fixed (Matrix4x4* projection1 = &projection)
        fixed (Matrix4x4* matrix1 = &matrix)
        fixed (Matrix4x4* deltaMatrix1 = &deltaMatrix)
        fixed (float* snap1 = &snap)
        fixed (float* localBounds1 = &localBounds)
        fixed (float* boundsSnap1 = &boundsSnap)
            return ImGuizmoNative.ImGuizmo_Manipulate((float*)view1, (float*)projection1, operation, mode,
                (float*)matrix1, (float*)deltaMatrix1, snap1, localBounds1, boundsSnap1) > (byte)0;
    }

    public static unsafe bool Manipulate(
        ref Matrix4x4 view,
        ref Matrix4x4 projection,
        OPERATION operation,
        MODE mode,
        ref Matrix4x4 matrix)
    {
        fixed (Matrix4x4* view1 = &view)
        fixed (Matrix4x4* projection1 = &projection)
        fixed (Matrix4x4* matrix1 = &matrix)
            return ImGuizmoNative.ImGuizmo_Manipulate((float*)view1, (float*)projection1, operation, mode,
                (float*)matrix1, (float*)null, (float*)null, (float*)null, (float*)null) > (byte)0;
    }

    public static unsafe void ViewManipulate(
        ref Matrix4x4 view,
        ref Matrix4x4 projection,
        OPERATION operation,
        MODE mode,
        ref Matrix4x4 matrix,
        float length,
        Vector2 position,
        Vector2 size,
        uint backgroundColor)
    {
        fixed (Matrix4x4* view1 = &view)
        fixed (Matrix4x4* projection1 = &projection)
        fixed (Matrix4x4* matrix1 = &matrix)
            ImGuizmoNative.ImGuizmo_ViewManipulate_FloatPtr((float*)view1, (float*)projection1, operation, mode,
                (float*)matrix1, length, position, size, backgroundColor);
    }

    public static unsafe void ViewManipulate(
        ref Matrix4x4 view,
        float length,
        Vector2 position,
        Vector2 size,
        uint backgroundColor)
    {
        fixed (Matrix4x4* view1 = &view)
            ImGuizmoNative.ImGuizmo_ViewManipulate_Float((float*)view1, length, position, size, backgroundColor);
    }
}