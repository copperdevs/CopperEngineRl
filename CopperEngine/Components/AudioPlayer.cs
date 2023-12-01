
using Raylib_cs;

namespace CopperEngine.Components;

public class AudioPlayer : GameComponent
{
    public void PlaySound(Audio audio)
    {
        Raylib.PlaySound(audio);
    }
}