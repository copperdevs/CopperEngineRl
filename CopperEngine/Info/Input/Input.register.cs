using Raylib_CsLo;

namespace CopperEngine.Info;

public static partial class Input
{
    private static readonly List<(KeyboardButton, ButtonPressType, Action)> KeyboardButtonActions = new();
    private static readonly List<(MouseButton, ButtonPressType, Action)> MouseButtonActions = new();
    private static readonly List<(GamepadButton, ButtonPressType, Action)> GamepadButtonActions = new();
    
    public static void RegisterInput(KeyboardButton button, ButtonPressType pressType, Action action)
    {
        KeyboardButtonActions.Add((button, pressType, action));
    }

    public static void RegisterInput(MouseButton button, ButtonPressType pressType, Action action)
    {
        MouseButtonActions.Add((button, pressType, action));
    }

    public static void RegisterInput(GamepadButton button, ButtonPressType pressType, Action action)
    {
        GamepadButtonActions.Add((button, pressType, action));
    }

    internal static void CheckInput()
    {
        foreach (var button in KeyboardButtonActions.Where(button => CheckButtonInput((int)button.Item1, button.Item2)))
        {
            button.Item3.Invoke();
        }

        foreach (var button in MouseButtonActions.Where(button => CheckButtonInput((int)button.Item1, button.Item2)))
        {
            button.Item3.Invoke();
        }

        foreach (var button in GamepadButtonActions.Where(button => CheckButtonInput((int)button.Item1, button.Item2)))
        {
            button.Item3.Invoke();
        }
    }

    private static bool CheckButtonInput(int key, ButtonPressType type)
    {
        return type switch
        {
            ButtonPressType.Down => Raylib.IsKeyDown(key),
            ButtonPressType.Pressed => Raylib.IsKeyPressed(key),
            ButtonPressType.Released => Raylib.IsKeyReleased(key),
            ButtonPressType.Up => Raylib.IsKeyUp(key),
            _ => false
        };
    }
}