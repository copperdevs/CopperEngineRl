using CopperEngine.Components;
using Force.DeepCloner;
using Raylib_CsLo;

namespace CopperEngine.Scenes;

public static class SceneManager
{
    private static readonly Scene EmptyScene = new("Empty Scene");
    internal static Dictionary<Guid, Scene>? Scenes = new();
    internal static Scene ActiveScene = EmptyScene;

    public static Action? SceneChanged;

    public static void LoadScene(Guid scene)
    {
        var targetScene = (Scenes?[scene]).DeepClone();
        ActiveScene = targetScene;
        
        SceneChanged?.Invoke();
    }

    internal static void RegisterScene(Scene scene)
    {
        Scenes ??= new Dictionary<Guid, Scene>();
        Scenes.Add(scene, scene);
    }

    internal static void UpdateCurrentScene() => UpdateScene(ActiveScene);
    
    internal static void UpdateScene(Scene scene)
    {
        UpdateGameComponents(scene, gm =>
        {
            RlGl.rlPushMatrix();
            
            RlGl.rlTranslatef(gm.Transform.Position.X, gm.Transform.Position.Y, gm.Transform.Position.Z);
            RlGl.rlScalef(gm.Transform.Scale.X, gm.Transform.Scale.Y, gm.Transform.Scale.Z);
            RlGl.rlRotatef(gm.Transform.Rotation.W, gm.Transform.Rotation.X, gm.Transform.Rotation.Y, gm.Transform.Rotation.Z);
            
            gm.PreUpdate();
            gm.Update();
            gm.PostUpdate();
            
            RlGl.rlPopMatrix();
        });
        
        UpdateGameComponents(scene, gm => gm.PreUpdate());
        UpdateGameComponents(scene, gm => gm.Update());
        UpdateGameComponents(scene, gm => gm.PostUpdate());
    }

    internal static void UpdateGameComponents(Scene scene, Action<GameComponent> element)
    {
        scene.GameObjects.ForEach(gm => gm.GameComponents.ForEach(element));
    }
}