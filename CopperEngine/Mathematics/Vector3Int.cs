namespace CopperEngine.Mathematics;
using SystemVector3 = System.Numerics.Vector3;

public struct Vector3Int
{
    public int X { get; set; }
    public int Y { get; set; }
    public int Z { get; set; }

    public Vector3Int(int x, int y, int z)
    {
        X = x;
        Y = y;
        Z = z;
    }

    public static Vector3Int Zero => new(0, 0, 0);
    public static Vector3Int One => new(1, 1, 1);
}