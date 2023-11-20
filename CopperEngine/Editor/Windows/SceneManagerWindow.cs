using CopperEngine.Scenes;
using ImGuiNET;

namespace CopperEngine.Editor.Windows;

[EditorWindow("Scene Manager", StartingState = true)]
public class SceneManagerWindow : BaseEditorWindow
{
    internal override void Render()
    {
        foreach (var scene in SceneManager.Scenes!)
        {
            if(ImGui.Button(scene.Value.DisplayName))
                SceneManager.LoadScene(scene.Key);
        }
    }
}