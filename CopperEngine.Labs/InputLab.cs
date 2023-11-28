using CopperEngine.Info;
using CopperEngine.Logs;
using Raylib_cs;
using GamepadButton = CopperEngine.Info.GamepadButton;
using MouseButton = CopperEngine.Info.MouseButton;

namespace CopperEngine.Labs;

public static class InputLab
{
    public static void Run()
    {
        var lab = new Lab("Input Lab", () =>
        {
            Log.Info((int)KeyboardButton.Null);
            Log.Info((int)MouseButton.Left);
            Log.Info((int)GamepadButton.Unknown);
        }, () =>
        {
            Log.Info(Raylib.IsKeyDown(KeyboardKey.KEY_SPACE));
            Log.Info(Raylib.IsMouseButtonDown(Raylib_cs.MouseButton.MOUSE_BUTTON_LEFT));
            
            
            // Raylib.IsKeyDown()
            // Raylib.IsKeyPressed()
            // Raylib.IsKeyReleased()
            // Raylib.IsKeyUp()
        });
        
        lab.Run();
        
        // Raylib_CsLo.GamepadAxis
        // Raylib_CsLo.GamepadButton
        // Raylib_CsLo.KeyboardKey
        // Raylib_CsLo.MouseButton
        // Raylib_CsLo.MouseCursor
    }
}