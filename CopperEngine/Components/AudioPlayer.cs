using CopperEngine.Data;
using Raylib_cs;

namespace CopperEngine.Components;

public sealed class AudioPlayer : Component
{
    public void PlaySound(Audio audio)
    {
        Raylib.PlaySound(audio);
    }
}