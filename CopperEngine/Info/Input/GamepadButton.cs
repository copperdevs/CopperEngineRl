using rlGamepadButton = Raylib_cs.GamepadButton;

namespace CopperEngine.Info;

public enum GamepadButton
{
    Unknown = rlGamepadButton.GAMEPAD_BUTTON_UNKNOWN,
    LeftFaceUp = rlGamepadButton.GAMEPAD_BUTTON_LEFT_FACE_UP,
    LeftFaceRight = rlGamepadButton.GAMEPAD_BUTTON_LEFT_FACE_RIGHT,
    LeftFaceDown = rlGamepadButton.GAMEPAD_BUTTON_LEFT_FACE_DOWN,
    LeftFaceLeft = rlGamepadButton.GAMEPAD_BUTTON_LEFT_FACE_LEFT,
    RightFaceUp = rlGamepadButton.GAMEPAD_BUTTON_RIGHT_FACE_UP,
    RightFaceRight = rlGamepadButton.GAMEPAD_BUTTON_RIGHT_FACE_RIGHT,
    RightFaceDown = rlGamepadButton.GAMEPAD_BUTTON_RIGHT_FACE_DOWN,
    RightFaceLeft = rlGamepadButton.GAMEPAD_BUTTON_RIGHT_FACE_LEFT,
    LeftTrigger1 = rlGamepadButton.GAMEPAD_BUTTON_LEFT_TRIGGER_1,
    LeftTrigger2 = rlGamepadButton.GAMEPAD_BUTTON_LEFT_TRIGGER_2,
    RightTrigger1 = rlGamepadButton.GAMEPAD_BUTTON_RIGHT_TRIGGER_1,
    RightTrigger2 = rlGamepadButton.GAMEPAD_BUTTON_RIGHT_TRIGGER_2,
    MiddleLeft = rlGamepadButton.GAMEPAD_BUTTON_MIDDLE_LEFT,
    Middle = rlGamepadButton.GAMEPAD_BUTTON_MIDDLE,
    MiddleRight = rlGamepadButton.GAMEPAD_BUTTON_MIDDLE_RIGHT,
    LeftThumb = rlGamepadButton.GAMEPAD_BUTTON_LEFT_THUMB,
    RightThumb = rlGamepadButton.GAMEPAD_BUTTON_RIGHT_THUMB,
}