using Raylib_cs;
using rlAudio = Raylib_cs.Sound;

namespace CopperEngine.Data;

public sealed class Audio : IDisposable
{
    private readonly rlAudio audio;

    public Audio(string path)
    {
        audio = Raylib.LoadSound(path);
    }

    public void Dispose()
    {
        Raylib.UnloadSound(audio);
    }

    public static implicit operator rlAudio(Audio audio) => audio.audio;
}