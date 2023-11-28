using System.Numerics;
using CopperEngine.Editor.Components;
using CopperEngine.Editor.DearImGui;
using CopperEngine.Logs;
using ImGuiNET;
using Raylib_cs;

namespace CopperEngine.Editor.Windows;

[EditorWindow("Scene", StartingState = true)]
public class SceneWindow : BaseEditorWindow
{
    internal static Vector2 WindowSize;
    internal static Vector2 WindowPosition;

    internal override void Start()
    {
        EditorCameraController.Start(); 
    }

    internal override void PreRender()
    {
        ImGui.PushStyleVar(ImGuiStyleVar.WindowPadding, new Vector2(0, 0));
    }

    internal override void Render()
    {
        EngineEditor.EditorWindowFocused = ImGui.IsWindowFocused();
        
        
        rlImGui.ImageRenderTextureFit(EngineRenderer.EditorTexture);
        
        WindowSize = ImGui.GetWindowSize();
        WindowPosition = ImGui.GetWindowPos();
        
        if(InspectorWindow.transformOpen && HierarchyWindow.CurrentTarget is not null)
            Gizmo.Manipulate(ref HierarchyWindow.CurrentTarget.Transform);
        
        // Gizmo.ViewManipulate();
        
        if(ImGui.IsWindowFocused() && ImGui.IsWindowHovered())
        // if(ImGui.IsWindowFocused())
            EditorCameraController.Update();
            
        // if(ImGui.IsWindowFocused() && ImGui.IsWindowHovered())
            // Raylib.UpdateCamera(ref EngineRenderer.EditorCamera.Camera3D, CameraMode.CAMERA_FREE);
    }

    internal override void PostRender()
    {
        ImGui.PopStyleVar();
    }
}   