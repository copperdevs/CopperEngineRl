using System.Numerics;
using Raylib_cs;
using rlMouseButton = Raylib_cs.MouseButton;
using rlGamepadButton = Raylib_cs.GamepadButton;

namespace CopperEngine.Info;

public static partial class Input
{
    private static readonly List<(KeyboardButton[], ButtonPressType, Action)> KeyboardButtonActions = new();
    private static readonly List<(MouseButton[], ButtonPressType, Action)> MouseButtonActions = new();
    private static readonly List<(GamepadButton[], ButtonPressType, Action)> GamepadButtonActions = new();

    private static readonly List<(AxisInput, Action<Vector2>)> AxisInputActions = new();

    public static void RegisterInput(KeyboardButton button, ButtonPressType pressType, Action action)
    {
        KeyboardButtonActions.Add((new[] { button }, pressType, action));
    }

    public static void RegisterInput(MouseButton button, ButtonPressType pressType, Action action)
    {
        MouseButtonActions.Add((new[] { button }, pressType, action));
    }

    public static void RegisterInput(GamepadButton button, ButtonPressType pressType, Action action)
    {
        GamepadButtonActions.Add((new[] { button }, pressType, action));
    }

    public static void RegisterInput(KeyboardButton[] button, ButtonPressType pressType, Action action)
    {
        KeyboardButtonActions.Add((button, pressType, action));
    }

    public static void RegisterInput(MouseButton[] button, ButtonPressType pressType, Action action)
    {
        MouseButtonActions.Add((button, pressType, action));
    }

    public static void RegisterInput(GamepadButton[] button, ButtonPressType pressType, Action action)
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
            var mainDown = true;

            foreach (var keyButton in button.Item1)
            {
                var buttonDown = button.Item2 switch
                {
                    ButtonPressType.Down => Raylib.IsKeyDown((KeyboardKey)keyButton),
                    ButtonPressType.Pressed => Raylib.IsKeyPressed((KeyboardKey)keyButton),
                    ButtonPressType.Released => Raylib.IsKeyReleased((KeyboardKey)keyButton),
                    ButtonPressType.Up => Raylib.IsKeyUp((KeyboardKey)keyButton),
                    _ => throw new ArgumentOutOfRangeException()
                };

                if (mainDown && buttonDown)
                    mainDown = true;
                else
                    mainDown = false;
            }

            if (mainDown)
                button.Item3.Invoke();
        }

        foreach (var button in MouseButtonActions)
        {
            var mainDown = true;

            foreach (var keyButton in button.Item1)
            {
                var buttonDown = button.Item2 switch
                {
                    ButtonPressType.Down => Raylib.IsMouseButtonDown((rlMouseButton)keyButton),
                    ButtonPressType.Pressed => Raylib.IsMouseButtonPressed((rlMouseButton)keyButton),
                    ButtonPressType.Released => Raylib.IsMouseButtonReleased((rlMouseButton)keyButton),
                    ButtonPressType.Up => Raylib.IsMouseButtonUp((rlMouseButton)keyButton),
                    _ => throw new ArgumentOutOfRangeException()
                };

                if (mainDown && buttonDown)
                    mainDown = true;
                else
                    mainDown = false;
            }

            if (mainDown)
                button.Item3.Invoke();
        }

        foreach (var button in GamepadButtonActions)
        {
            var mainDown = true;

            foreach (var keyButton in button.Item1)
            {
                var buttonDown = button.Item2 switch
                {
                    ButtonPressType.Down => Raylib.IsGamepadButtonDown(0, (rlGamepadButton)keyButton),
                    ButtonPressType.Pressed => Raylib.IsGamepadButtonPressed(0, (rlGamepadButton)keyButton),
                    ButtonPressType.Released => Raylib.IsGamepadButtonReleased(0, (rlGamepadButton)keyButton),
                    ButtonPressType.Up => Raylib.IsGamepadButtonUp(0, (rlGamepadButton)keyButton),
                    _ => throw new ArgumentOutOfRangeException()
                };

                if (mainDown && buttonDown)
                    mainDown = true;
                else
                    mainDown = false;
            }

            if (mainDown)
                button.Item3.Invoke();
        }

        foreach (var axis in AxisInputActions)
        {
            switch (axis.Item1)
            {
                case AxisInput.GamepadLeftJoystick:
                    axis.Item2.Invoke(new Vector2(Raylib.GetGamepadAxisMovement(0, GamepadAxis.GAMEPAD_AXIS_LEFT_X),
                        Raylib.GetGamepadAxisMovement(0, GamepadAxis.GAMEPAD_AXIS_LEFT_Y)));
                    break;
                case AxisInput.GamepadRightJoystick:
                    axis.Item2.Invoke(new Vector2(Raylib.GetGamepadAxisMovement(0, GamepadAxis.GAMEPAD_AXIS_RIGHT_X),
                        Raylib.GetGamepadAxisMovement(0, GamepadAxis.GAMEPAD_AXIS_RIGHT_Y)));
                    break;
                case AxisInput.GamepadLeftTrigger:
                    axis.Item2.Invoke(
                        new Vector2(Raylib.GetGamepadAxisMovement(0, GamepadAxis.GAMEPAD_AXIS_LEFT_TRIGGER), 0));
                    break;
                case AxisInput.GamepadRightTrigger:
                    axis.Item2.Invoke(
                        new Vector2(Raylib.GetGamepadAxisMovement(0, GamepadAxis.GAMEPAD_AXIS_RIGHT_TRIGGER), 0));
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