using System.Numerics;
using JoltPhysicsSharp;

namespace CopperEngine.Physics;

public class Simulation
{
    // System
    public PhysicsSystem PhysicsSystem { get; private set; }
    public TempAllocator Allocator { get; private set; }
    public JobSystemThreadPool JobSystem { get; private set; }

    // Layers
    public CopperBroadPhaseLayerInterface BroadPhaseLayerInterface { get; private set; }
    public CopperObjectLayerPairFilter ObjectLayerPairFilter { get; private set; }
    public CopperObjectVsBroadPhaseLayerFilter ObjectVsBroadPhaseLayerFilter { get; private set; }
    
    // Info
    public Vector3 Gravity
    {
        get
        {
            PhysicsSystem.GetGravity(out var gravity);
            return gravity;
        }
        set => PhysicsSystem.Gravity = value;
    }

    internal Simulation(PhysicsSettings settings)
    {
        Foundation.Init();
        
        Allocator = new TempAllocator(10 * (int) settings.MaxContactConstraints * (int) settings.MaxContactConstraints);
        JobSystem = new JobSystemThreadPool(Foundation.MaxPhysicsJobs, Foundation.MaxPhysicsBarriers);
        
        BroadPhaseLayerInterface = new CopperBroadPhaseLayerInterface();
        ObjectLayerPairFilter = new CopperObjectLayerPairFilter();
        ObjectVsBroadPhaseLayerFilter = new CopperObjectVsBroadPhaseLayerFilter();

        PhysicsSystem = new PhysicsSystem();
        PhysicsSystem.Init(settings.MaxBodies, settings.NumBodyMutexes, settings.MaxBodyPairs, settings.MaxContactConstraints, 
            BroadPhaseLayerInterface, ObjectVsBroadPhaseLayerFilter, ObjectLayerPairFilter);
        PhysicsSystem.Gravity = settings.Gravity;
        PhysicsSystem.OptimizeBroadPhase();
    }

    internal void Update(float timeStep, int collisionSteps)
    {
        PhysicsSystem.Update(timeStep, collisionSteps, Allocator, JobSystem);
    }
}