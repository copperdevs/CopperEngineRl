using CopperEngine.Scenes;
using ImGuiNET;

namespace CopperEngine.Editor.Windows;

[EditorWindow("Scene Manager", StartingState = true)]
internal sealed class SceneManagerWindow : BaseEditorWindow
{
    internal override void Render()
    {
        ImGui.LabelText("Current Scene", SceneManager.ActiveScene.DisplayName);
        foreach (var scene in SceneManager.Scenes!.ToList())
        {
            if (ImGui.Button(scene.Value.DisplayName))
                SceneManager.LoadScene(scene.Key);
        }
    }
}