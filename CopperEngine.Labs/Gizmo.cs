using System.Numerics;
using Raylib_CsLo;
using static Raylib_CsLo.Raylib;

namespace CopperEngine.Labs;

public static class Gizmo
{
    private enum MoveDirection
    {
        None,
        X,
        Y,
        Z
    }

    private static MoveDirection currentDirection = MoveDirection.None;
    private static bool movingGizmo = false;

    private static Ray ray = new();

    private static Vector3 xPosition = new(1.0f, 0.0f, 0.0f);
    private static Vector3 xDirection = new(1.0f, 0.0f, 0.0f);
    private static Vector3 xSize = new(0.15f, 0.15f, 0.15f);
    private static RayCollision xCollision = new();

    private static Vector3 yPosition = new(0.0f, 1.0f, 0.0f);
    private static Vector3 yDirection = new(0.0f, 1.0f, 0.0f);
    private static Vector3 ySize = new(0.15f, 0.15f, 0.15f);
    private static RayCollision yCollision = new();

    private static Vector3 zPosition = new(0.0f, 0.0f, 1.0f);
    private static Vector3 zDirection = new(0.0f, 0.0f, 1.0f);
    private static Vector3 zSize = new(0.15f, 0.15f, 0.15f);
    private static RayCollision zCollision = new();

    public static void DrawTranslationGizmo(ref Vector3 position, ref Camera3D camera)
    {
        RlGl.rlPushMatrix();
        RlGl.rlTranslatef(position.X, position.Y, position.Z);

        ray = GetMouseRay(GetMousePosition(), camera);

        if (movingGizmo && IsMouseButtonReleased(MouseButton.MOUSE_BUTTON_LEFT))
            movingGizmo = false;


        if (!movingGizmo && (CheckCollision(out xCollision, xPosition + position, xSize, MoveDirection.X) ||
                             CheckCollision(out yCollision, yPosition + position, ySize, MoveDirection.Y) ||
                             CheckCollision(out zCollision, zPosition + position, zSize, MoveDirection.Z)))
        {
            movingGizmo = true;

            switch (currentDirection)
            {
                case MoveDirection.None:
                    break;
                case MoveDirection.X:
                    UpdatePosition(ref position, xDirection, GetMouseDelta().X);
                    break;
                case MoveDirection.Y:
                    UpdatePosition(ref position, yDirection, -GetMouseDelta().Y);
                    break;
                case MoveDirection.Z:
                    UpdatePosition(ref position, zDirection, -GetMouseDelta().X);
                    break;
                default:
                    currentDirection = MoveDirection.None;
                    break;
            }
        }
        else if (movingGizmo)
        {
            switch (currentDirection)
            {
                case MoveDirection.None:
                    break;
                case MoveDirection.X:
                    UpdatePosition(ref position, xDirection, GetMouseDelta().X);
                    break;
                case MoveDirection.Y:
                    UpdatePosition(ref position, yDirection, -GetMouseDelta().Y);
                    break;
                case MoveDirection.Z:
                    UpdatePosition(ref position, zDirection, -GetMouseDelta().X);
                    break;
                default:
                    currentDirection = MoveDirection.None;
                    break;
            }
        }

        DrawCubes(xCollision, xPosition, xSize, (RED, MAROON), (MAROON, RED));
        DrawCubes(yCollision, yPosition, ySize, (GREEN, DARKGREEN), (DARKGREEN, GREEN));
        DrawCubes(zCollision, zPosition, zSize, (BLUE, DARKBLUE), (DARKBLUE, BLUE));


        RlGl.rlPopMatrix();
    }

    private static bool CheckCollision(out RayCollision collision, Vector3 cubePosition, Vector3 cubeSize, MoveDirection direction)
    {
        collision = GetRayCollisionBox(ray,
            new BoundingBox
            (
                new Vector3(cubePosition.X - cubeSize.X / 2, cubePosition.Y - cubeSize.Y / 2,
                    cubePosition.Z - cubeSize.Z / 2),
                new Vector3(cubePosition.X + cubeSize.X / 2, cubePosition.Y + cubeSize.Y / 2,
                    cubePosition.Z + cubeSize.Z / 2)
            ));
        currentDirection = direction;
        return collision.hit;
    }

    private static void DrawCubes(RayCollision collision, Vector3 cubePosition, Vector3 cubeSize, (Color,Color) activeColors, (Color,Color) inactiveColors)
    {
        if (collision.hit && IsMouseButtonDown(MouseButton.MOUSE_BUTTON_LEFT))
        {
            DrawCube(cubePosition, cubeSize.X, cubeSize.Y, cubeSize.Z, activeColors.Item1);
            DrawCubeWires(cubePosition, cubeSize.X, cubeSize.Y, cubeSize.Z, activeColors.Item2);

            DrawCubeWires(cubePosition, cubeSize.X + 0.2f, cubeSize.Y + 0.2f, cubeSize.Z + 0.2f, GREEN);
        }
        else
        {
            DrawCube(cubePosition, cubeSize.X, cubeSize.Y, cubeSize.Z, inactiveColors.Item1);
            DrawCubeWires(cubePosition, cubeSize.X, cubeSize.Y, cubeSize.Z, inactiveColors.Item2);
        }

        DrawLine3D(cubePosition, Vector3.Zero, activeColors.Item1);
    }

    private static void UpdatePosition(ref Vector3 position, Vector3 direction, float mouseDelta)
    {
        if (IsMouseButtonDown(MouseButton.MOUSE_BUTTON_LEFT))
        {
            // position.Y -= GetMouseDelta().Y * GetFrameTime();
            // position += direction * GetMouseDelta().Y * GetFrameTime();
            position += direction * GetFrameTime() * mouseDelta;
        }
    }
}