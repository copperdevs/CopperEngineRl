
using Raylib_cs;

namespace CopperEngine.Components;

public class AudioPlayer : Component
{
    public void PlaySound(Audio audio)
    {
        Raylib.PlaySound(audio);
    }
}