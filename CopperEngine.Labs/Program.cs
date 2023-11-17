using System.Text.Json;
using CopperEngine.Data;
using CopperEngine.Logs;
using CopperEngine.Mathematics;

namespace CopperEngine.Labs;

public static class Program
{
    public static void Main()
    {
        var transform = new Transform
        {
            Position = new Vector3(3, 4, 5), 
            // Matrix = new Matrix4x4(11, 12, 13, 14, 21, 22, 23, 24, 31, 32, 33, 34, 41, 42, 43, 44), 
            Rotation = new Quaternion(2, 3, 4, 5), 
            Scale = Vector3.One
        };

        var json = JsonSerializer.Serialize(transform);
        Log.Info(json);
        
        File.WriteAllText("transform.json", json);
    }
}