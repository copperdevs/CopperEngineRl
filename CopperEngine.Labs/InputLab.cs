using CopperEngine.Info;
using CopperEngine.Logs;
using Raylib_CsLo;

namespace CopperEngine.Labs;

public static class InputLab
{
    public static void Run()
    {
        var lab = new Lab("Input Lab", () =>
        {
            Log.Info((int)ButtonInput.MouseButtonLeft);
            Log.Info((int)ButtonInput.MouseButtonRight);
            Log.Info((int)ButtonInput.MouseButtonMiddle);
            Log.Info((int)ButtonInput.MouseButtonSide);
            Log.Info((int)ButtonInput.MouseButtonExtra);
            Log.Info((int)ButtonInput.MouseButtonForward);
            Log.Info((int)ButtonInput.MouseButtonBack);
        }, () =>
        {
            Log.Info(Raylib.IsKeyDown((int)ButtonInput.KeySpace));
            Log.Info(Raylib.IsMouseButtonDown((int)ButtonInput.MouseButtonLeft));
        });
        
        lab.Run();
        
        // Raylib_CsLo.GamepadAxis
        // Raylib_CsLo.GamepadButton
        // Raylib_CsLo.KeyboardKey
        // Raylib_CsLo.MouseButton
        // Raylib_CsLo.MouseCursor
    }
}