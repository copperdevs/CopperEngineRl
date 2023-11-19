using CopperEngine.Components;

namespace CopperEngine.Scenes;

public static class SceneManager
{
    private static readonly Scenes.Scene EmptyScene = new("Empty Scene");
    internal static Dictionary<Guid, Scene>? Scenes = new();
    internal static Scene ActiveScene = EmptyScene;

    public static void LoadScene(Guid scene)
    {
        ActiveScene = Scenes?[scene]!;
    }

    internal static void RegisterScene(Scene scene)
    {
        if (Scenes is null)
            Scenes = new Dictionary<Guid, Scene>();
        
        Scenes.Add(scene, scene);
    }

    internal static void UpdateCurrentScene() => UpdateScene(ActiveScene);
    
    internal static void UpdateScene(Scene scene)
    {
        UpdateGameComponents(scene, gm => gm.PreUpdate());
        UpdateGameComponents(scene, gm => gm.Update());
        UpdateGameComponents(scene, gm => gm.PostUpdate());
    }

    internal static void UpdateGameComponents(Scene scene, Action<GameComponent> element)
    {
        scene.GameObjects.ForEach(gm => gm.GameComponents.ForEach(element));
    }
}