namespace CopperEngine.Mathematics;
using SystemVector2 = System.Numerics.Vector2;

public struct Vector2Int
{
    public int X { get; set; }
    public int Y { get; set; }

    public Vector2Int(int x, int y)
    {
        X = x;
        Y = y;
    }

    public static Vector2Int Zero => new(0, 0);
    public static Vector2Int One => new(1, 1);
}