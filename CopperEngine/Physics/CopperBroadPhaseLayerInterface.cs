using JoltPhysicsSharp;

namespace CopperEngine.Physics;

public class CopperBroadPhaseLayerInterface : BroadPhaseLayerInterface
{
    private readonly BroadPhaseLayer[] objectToBroadPhase;

    public CopperBroadPhaseLayerInterface()
    {
        objectToBroadPhase = new BroadPhaseLayer[Layers.NumLayers];
        objectToBroadPhase[Layers.NonMoving] = BroadPhaseLayers.NonMoving;
        objectToBroadPhase[Layers.Moving] = BroadPhaseLayers.Moving;
    }

    protected override int GetNumBroadPhaseLayers()
    {
        return BroadPhaseLayers.NumLayers;
    }

    protected override BroadPhaseLayer GetBroadPhaseLayer(ObjectLayer layer)
    {
        return objectToBroadPhase[layer];
    }

    protected override string GetBroadPhaseLayerName(BroadPhaseLayer layer)
    {
        switch (layer) 
        {
            case BroadPhaseLayers.NonMoving:
                return "NON_MOVING";
            
            case BroadPhaseLayers.Moving:
                return "MOVING";
            
            default:
                return "INVALID";
        }
    }
}