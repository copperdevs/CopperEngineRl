using JoltPhysicsSharp;

namespace CopperEngine.Physics;

public class CopperObjectLayerPairFilter : ObjectLayerPairFilter
{
    protected override bool ShouldCollide(ObjectLayer object1, ObjectLayer object2)
    {
        switch (object1)
        {
            case Layers.NonMoving:
                return object2 == Layers.Moving;

            case Layers.Moving:
                return true;
            default:
                return false;
        }
    }
}