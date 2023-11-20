﻿using System.Numerics;
using Raylib_CsLo;

namespace CopperEngine.Info;

public static class Input
{
    public static bool IsKeyPressed(KeyboardKey key) => Raylib.IsKeyPressed(key);
    public static bool IsKeyDown(KeyboardKey key) => Raylib.IsKeyDown(key);
    public static bool IsKeyReleased(KeyboardKey key) => Raylib.IsKeyReleased(key);
    public static bool IsKeyUp(KeyboardKey key) => Raylib.IsKeyUp(key);
    public static int GetKeyPressed() => Raylib.GetKeyPressed();
    public static int GetCharPressed() => Raylib.GetCharPressed();
    public static void SetExitKey(KeyboardKey key) => Raylib.SetExitKey(key);
    public static bool IsMouseButtonPressed(MouseButton button) => Raylib.IsMouseButtonPressed(button);
    public static bool IsMouseButtonDown(MouseButton button) => Raylib.IsMouseButtonDown(button);
    public static bool IsMouseButtonReleased(MouseButton button) => Raylib.IsMouseButtonReleased(button);
    public static bool IsMouseButtonUp(MouseButton button) => Raylib.IsMouseButtonUp(button);
    public static int GetMouseX() => Raylib.GetMouseX();
    public static int GetMouseY() => Raylib.GetMouseY();
    public static Vector2 GetMousePosition() => Raylib.GetMousePosition();
    public static Vector2 GetMouseDelta() => Raylib.GetMouseDelta();
    public static float GetMouseWheelMove() => Raylib.GetMouseWheelMove();
    public static Vector2 GetMouseWheelMoveV() => Raylib.GetMouseWheelMoveV();
    public static void SetMouseCursor(MouseCursor cursor) => Raylib.SetMouseCursor(cursor);
    public static void SetMousePosition(int x, int y) => Raylib.SetMousePosition(x, y);
    public static void SetMouseOffset(int x, int y) => Raylib.SetMouseOffset(x, y);
    public static void SetMouseScale(float scaleX, float scaleY) => Raylib.SetMouseScale(scaleX, scaleY);
    public static bool IsGamepadAvailable(int gamepad) => Raylib.IsGamepadAvailable(gamepad);
    public static string GetGamepadName(int gamepad) => Raylib.GetGamepadName_(gamepad);
    public static bool IsGamepadButtonPressed(int gamepad, GamepadButton button) => Raylib.IsGamepadButtonPressed(gamepad, button);
    public static bool IsGamepadButtonDown(int gamepad, GamepadButton button) => Raylib.IsGamepadButtonDown(gamepad, button);
    public static bool IsGamepadButtonReleased(int gamepad, GamepadButton button) => Raylib.IsGamepadButtonReleased(gamepad, button);
    public static bool IsGamepadButtonUp(int gamepad, GamepadButton button) => Raylib.IsGamepadButtonUp(gamepad, button);
    public static int GetGamepadButtonPressed() => Raylib.GetGamepadButtonPressed();
    public static int GetGamepadAxisCount(int gamepad) => Raylib.GetGamepadAxisCount(gamepad);
    public static float GetGamepadAxisMovement(int gamepad, GamepadAxis axis) => Raylib.GetGamepadAxisMovement(gamepad, axis);
    public static void SetGamepadMappings(string mappings) => Raylib.SetGamepadMappings(mappings);
    public static int GetTouchX() => Raylib.GetTouchX();
    public static int GetTouchY() => Raylib.GetTouchY();
    public static Vector2 GetTouchPosition(int index) => Raylib.GetTouchPosition(index);
    public static int GetTouchPointId(int index) => Raylib.GetTouchPointId(index);
    public static int GetTouchPointCount() => Raylib.GetTouchPointCount();
    public static void SetGesturesEnabled(Gesture flags) => Raylib.SetGesturesEnabled((uint)flags);
    public static bool IsGestureDetected(Gesture gesture) => Raylib.IsGestureDetected(gesture);
    public static Gesture GetGestureDetected() => (Gesture)Raylib.GetGestureDetected();
    public static float GetGestureHoldDuration() => Raylib.GetGestureHoldDuration();
    public static Vector2 GetGestureDragVector() => Raylib.GetGestureDragVector();
    public static float GetGestureDragAngle() => Raylib.GetGestureDragAngle();
    public static Vector2 GetGesturePinchVector() => Raylib.GetGesturePinchVector();
    public static float GetGesturePinchAngle() => Raylib.GetGesturePinchAngle();
    public static void ShowCursor() => Raylib.ShowCursor();
    public static void HideCursor() => Raylib.HideCursor();
    public static bool IsCursorHidden() => Raylib.IsCursorHidden();
    public static void EnableCursor() => Raylib.EnableCursor();
    public static void DisableCursor() => Raylib.DisableCursor();
    public static bool IsCursorOnScreen() => Raylib.IsCursorOnScreen();
}