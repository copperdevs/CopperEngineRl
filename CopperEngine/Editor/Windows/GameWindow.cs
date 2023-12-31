﻿using System.Numerics;
using CopperEngine.Editor.DearImGui;
using ImGuiNET;

namespace CopperEngine.Editor.Windows;

[EditorWindow("Game", StartingState = true)]
internal sealed class GameWindow : BaseEditorWindow
{
    internal static Vector2 WindowSize;
    internal static Vector2 WindowPosition;
    internal override void PreRender()
    {
        ImGui.PushStyleVar(ImGuiStyleVar.WindowPadding, new Vector2(0, 0));
    }

    internal override void Render()
    {
        EngineEditor.GameWindowFocused = ImGui.IsWindowFocused();
        rlImGui.ImageRenderTextureFit(EngineRenderer.GameTexture);
        WindowSize = ImGui.GetWindowSize();
        WindowPosition = ImGui.GetWindowPos();
    }

    internal override void PostRender()
    {
        ImGui.PopStyleVar();
    }
}