﻿using CopperEngine.Components;
using CopperEngine.Scenes;
using ImGuiNET;

namespace CopperEngine.Editor.Windows;

[EditorWindow("Hierarchy", StartingState = true)]
internal sealed class HierarchyWindow : BaseEditorWindow
{
    internal static GameObject? CurrentTarget;

    internal override void Start()
    {
        SceneManager.SceneChanged += () => CurrentTarget = null;
    }

    internal override void Render()
    {
        for (var index = 0; index < SceneManager.ActiveScene.GameObjects.Count; index++)
        {
            var gameObject = SceneManager.ActiveScene.GameObjects[index];
            if (ImGui.Selectable($"GameObject #{index}", CurrentTarget == gameObject))
                CurrentTarget = gameObject;
        }
    }
}