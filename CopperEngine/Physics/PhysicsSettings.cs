using System.Numerics;

namespace CopperEngine.Physics; 

public class PhysicsSettings 
{

    public Vector3 Gravity = new(0, -9.81F, 0);
    public uint MaxBodies = 70000;
    public uint NumBodyMutexes = 0;
    public uint MaxBodyPairs = 70000;
    public uint MaxContactConstraints = 70000;
}