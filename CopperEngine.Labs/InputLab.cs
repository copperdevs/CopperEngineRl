using System.Numerics;
using CopperEngine.Info;
using CopperEngine.Logs;
using CopperEngine.Utility;
using Raylib_CsLo;
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
            Log.Info(Raylib.IsKeyDown((int)KeyboardButton.Space));
            Log.Info(Raylib.IsMouseButtonDown((int)MouseButton.Left));
            
            
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