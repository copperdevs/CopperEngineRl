using CopperEngine.Components;
using CopperEngine.Scenes;
using ImGuiNET;

namespace CopperEngine.Editor.Windows;

[EditorWindow("Hierarchy", StartingState = true)]
public class HierarchyWindow : BaseEditorWindow
{
    internal static GameObject? CurrentTarget;

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