﻿using System.Numerics;
using CopperEngine.Editor.DearImGui;
using ImGuiNET;

namespace CopperEngine.Editor.Windows;

[EditorWindow("Scene")]
public class SceneWindow : BaseEditorWindow
{
    internal static Vector2 WindowSize;
    internal static Vector2 WindowPosition;
    
    internal override void PreRender()
    {
        ImGui.PushStyleVar(ImGuiStyleVar.WindowPadding, new Vector2(0, 0));
    }

    internal override void Render()
    {
        EngineEditor.EditorWindowFocused = ImGui.IsWindowFocused();
        rlImGui.ImageRenderTextureFit(EngineRenderer.editorTexture);
        
        WindowSize = ImGui.GetWindowSize();
        WindowPosition = ImGui.GetWindowPos();
        
        if(InspectorWindow.transformOpen && HierarchyWindow.CurrentTarget is not null)
            Gizmo.Manipulate(ref HierarchyWindow.CurrentTarget.Transform);
    }

    internal override void PostRender()
    {
        ImGui.PopStyleVar();
    }
}   