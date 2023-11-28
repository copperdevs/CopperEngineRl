using CopperEngine.Components;
using CopperEngine.Logs;
using Force.DeepCloner;
using Raylib_cs;

namespace CopperEngine.Scenes;

public static class SceneManager
{
    private static readonly Scene EmptyScene = new("Empty Scene");
    internal static Dictionary<Guid, Scene>? Scenes = new();
    internal static Scene ActiveScene = EmptyScene;

    public static Action? SceneChanged;

    public static void LoadScene(Guid scene)
    {
        Scenes ??= new Dictionary<Guid, Scene>();
        
        if (!Scenes!.ContainsKey(scene))
        {
            Log.Warning("Target scene to load does not exist. Not loading a new scene. That sucks lmfao");
            return;
        }
        
        var targetScene = (Scenes[scene]).DeepClone();

        if (targetScene is null)
        {
            Log.Warning("Target scene to load is null. Not loading a new scene. lol");
            return;
        }
        
        UpdateGameComponents(ActiveScene, gm => gm.Sleep());
        
        ActiveScene = targetScene;
        
        SceneChanged?.Invoke();
        
        UpdateGameComponents(ActiveScene, gm => gm.Awake());
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
            Rlgl.PushMatrix();
            
            Rlgl.Translatef(gm.Transform.Position.X, gm.Transform.Position.Y, gm.Transform.Position.Z);
            Rlgl.Scalef(gm.Transform.Scale.X, gm.Transform.Scale.Y, gm.Transform.Scale.Z);
            Rlgl.Rotatef(gm.Transform.Rotation.W, gm.Transform.Rotation.X, gm.Transform.Rotation.Y, gm.Transform.Rotation.Z);
            
            gm.PreUpdate();
            gm.Update();
            gm.PostUpdate();
            
            Rlgl.PopMatrix();
        });
    }

    internal static void UpdateGameComponents(Scene scene, Action<GameComponent> element)
    {
        scene.GameObjects.ForEach(gm => gm.GameComponents.ForEach(element));
    }
}