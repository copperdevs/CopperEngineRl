using CopperEngine.Logs;
using Raylib_CsLo;

namespace CopperEngine.Labs;

public class Lab
{
    private readonly string title;
    private readonly Action loadAction;
    private readonly Action updateAction;

    public Lab(string title) : this(title, () => {}, () => {})
    {
        
    }

    public Lab(string title, Action loadAction, Action updateAction)
    {
        this.title = title;
        this.loadAction = loadAction;
        this.updateAction = updateAction;
    }

    public void Run()
    {
        CopperLogger.Initialize();
        
        Raylib.SetConfigFlags(ConfigFlags.FLAG_MSAA_4X_HINT | ConfigFlags.FLAG_VSYNC_HINT | ConfigFlags.FLAG_WINDOW_RESIZABLE);
        Raylib.InitWindow(650, 400, title);
        Raylib.SetTargetFPS(144);
        Raylib.InitAudioDevice();
        
        loadAction.Invoke();

        while (!Raylib.WindowShouldClose())
        {
            Raylib.BeginDrawing();
            
            updateAction.Invoke();
            
            Raylib.EndDrawing();
        }

        Raylib.CloseAudioDevice();
        Raylib.CloseWindow();  
    }
        
}