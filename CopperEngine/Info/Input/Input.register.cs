using System.Numerics;
using Raylib_CsLo;

namespace CopperEngine.Info;

public static partial class Input
{
    private static readonly List<(KeyboardButton, ButtonPressType, Action)> KeyboardButtonActions = new();
    private static readonly List<(MouseButton, ButtonPressType, Action)> MouseButtonActions = new();
    private static readonly List<(GamepadButton, ButtonPressType, Action)> GamepadButtonActions = new();

    private static readonly List<(AxisInput, Action<Vector2>)> AxisInputActions = new();
    
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

    public static void RegisterInput(AxisInput axis, Action<Vector2> action)
    {
        AxisInputActions.Add((axis, action));
    }

    internal static void CheckInput()
    {
        foreach (var button in KeyboardButtonActions)
        {
            var buttonDown = button.Item2 switch
            {
                ButtonPressType.Down => Raylib.IsKeyDown((int)button.Item1),
                ButtonPressType.Pressed => Raylib.IsKeyPressed((int)button.Item1),
                ButtonPressType.Released => Raylib.IsKeyReleased((int)button.Item1),
                ButtonPressType.Up => Raylib.IsKeyUp((int)button.Item1),
                _ => false
            };
            
            if(buttonDown)
                button.Item3.Invoke();
        }
        foreach (var button in MouseButtonActions)
        {
            var buttonDown = button.Item2 switch
            {
                ButtonPressType.Down => Raylib.IsMouseButtonDown((int)button.Item1),
                ButtonPressType.Pressed => Raylib.IsMouseButtonPressed((int)button.Item1),
                ButtonPressType.Released => Raylib.IsMouseButtonReleased((int)button.Item1),
                ButtonPressType.Up => Raylib.IsMouseButtonUp((int)button.Item1),
                _ => false
            };
            
            if(buttonDown)
                button.Item3.Invoke();
        }
        foreach (var button in GamepadButtonActions)
        {
            var buttonDown = button.Item2 switch
            {
                ButtonPressType.Down => Raylib.IsGamepadButtonDown(0, (int)button.Item1),
                ButtonPressType.Pressed => Raylib.IsGamepadButtonPressed(0, (int)button.Item1),
                ButtonPressType.Released => Raylib.IsGamepadButtonReleased(0, (int)button.Item1),
                ButtonPressType.Up => Raylib.IsGamepadButtonUp(0, (int)button.Item1),
                _ => false
            };
            
            if(buttonDown)
                button.Item3.Invoke();
        }

        foreach (var axis in AxisInputActions)
        {
            switch (axis.Item1)
            {
                case AxisInput.GamepadLeftJoystick:
                    axis.Item2.Invoke(new Vector2(Raylib.GetGamepadAxisMovement(0, GamepadAxis.GAMEPAD_AXIS_LEFT_X), Raylib.GetGamepadAxisMovement(0, GamepadAxis.GAMEPAD_AXIS_LEFT_Y)));
                    break;
                case AxisInput.GamepadRightJoystick:
                    axis.Item2.Invoke(new Vector2(Raylib.GetGamepadAxisMovement(0, GamepadAxis.GAMEPAD_AXIS_RIGHT_X), Raylib.GetGamepadAxisMovement(0, GamepadAxis.GAMEPAD_AXIS_RIGHT_Y)));
                    break;
                case AxisInput.GamepadLeftTrigger:
                    axis.Item2.Invoke(new Vector2(Raylib.GetGamepadAxisMovement(0, GamepadAxis.GAMEPAD_AXIS_LEFT_TRIGGER), 0));
                    break;
                case AxisInput.GamepadRightTrigger:
                    axis.Item2.Invoke(new Vector2(Raylib.GetGamepadAxisMovement(0, GamepadAxis.GAMEPAD_AXIS_RIGHT_TRIGGER), 0));
                    break;
                case AxisInput.MouseScroll: 
                    axis.Item2.Invoke(Raylib.GetMouseWheelMoveV());
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }   
        }
    }
}