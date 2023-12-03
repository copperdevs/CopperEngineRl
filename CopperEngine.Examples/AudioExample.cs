using CopperEngine.Components;
using CopperEngine.Info;
using CopperEngine.Scenes;

namespace CopperEngine.Examples;

public class AudioExample : EngineApplication
{
    protected override void Load()
    {
        var scene = new Scene("Audio Testing");

        var audioGameObject = scene.CreateGameObject();
        audioGameObject.AddComponent<AudioComponent>();
    }
}

public class AudioComponent : Component
{
    private AudioPlayer player;
    private Audio audio;
    
    protected override void Start()
    {
        player = GetFirstComponentOfType<AudioPlayer>();
        // audio = new Audio("sound file path");
        Input.RegisterInput(KeyboardButton.Space, ButtonPressType.Pressed, () => { player.PlaySound(audio); });
    }
}