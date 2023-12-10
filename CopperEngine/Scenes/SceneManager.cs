using CopperEngine.Components;
using CopperEngine.Info;
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
        UpdateGameComponents(scene, gm => { TransformGameComponentsValues(gm, gm.PreUpdate); });
        UpdateGameComponents(scene, gm => { TransformGameComponentsValues(gm, gm.Update); });
        UpdateGameComponents(scene, gm => { TransformGameComponentsValues(gm, gm.PostUpdate); });


        UpdateGameComponents(scene, gm => gm.GizmosDraw());

        return;

        void TransformGameComponentsValues(Component gm, Action updateAction)
        {
            Rlgl.PushMatrix();

            Rlgl.Translatef(gm.Transform.Position.X, gm.Transform.Position.Y, gm.Transform.Position.Z);
            Rlgl.Scalef(gm.Transform.Scale.X, gm.Transform.Scale.Y, gm.Transform.Scale.Z);
            Rlgl.Rotatef(gm.Transform.Rotation.W, gm.Transform.Rotation.X, gm.Transform.Rotation.Y,
                gm.Transform.Rotation.Z);

            updateAction.Invoke();

            Rlgl.PopMatrix();
        }
    }

    internal static void UpdateGameComponents(Scene scene, Action<Component> element)
    {
        scene.GameObjects.ForEach(gm => gm.GameComponents.ForEach(element));
    }
}