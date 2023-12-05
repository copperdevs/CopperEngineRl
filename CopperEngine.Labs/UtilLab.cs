using CopperEngine.Logs;
using CopperEngine.Utility;

namespace CopperEngine.Labs;

public class UtilLab
{
    public static void Run()
    {
        var randomFloatList = new RandomList<float>(new List<float> {1, 2, 3, 4, 5, 6, 7, 8, 9, 10});
        var randomIntList = new RandomList<int>(1, 2, 3, 4, 5, 6, 7, 8, 9, 10) { 5, 5, 6 };
        var randomByteList = new RandomList<byte> { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };

        for (var i = 0; i < 5; i++)
        {
            float randomFloatValue = randomFloatList;
            Log.Info(randomFloatValue);
            
            float randomIntValue = randomIntList;
            Log.Info(randomIntValue);
            
            float randomByteValue = randomByteList;
            Log.Info(randomByteValue);
        }
    }
}