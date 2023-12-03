using CopperEngine.Data;
using CopperEngine.Editor;
using CopperEngine.Logs;
using CopperEngine.Physics;
using CopperEngine.Utility;
using JoltPhysicsSharp;

namespace CopperEngine.Components;

public class Rigidbody : Component
{
    public Simulation Simulation => EnginePhysics.Simulation;
    public BodyInterface BodyInterface => Simulation.PhysicsSystem.BodyInterface;
    
    public Shape Shape;
    public MotionType MotionType;

    public Body Body;
    public BodyID BodyId;
    
    public float Friction;
    public float Restitution;

    public ObjectLayer Layer;

    public Rigidbody(Shape shape, MotionType type = MotionType.Dynamic, float friction = 0, float restitution = 0) 
    {
        Shape = shape;
        MotionType = type;
        Friction = friction;
        Restitution = restitution;
    }

    protected internal override void Start()
    {
        Layer = MotionType switch
        {
            MotionType.Static => Layers.NonMoving,
            MotionType.Kinematic => Layers.Moving,
            MotionType.Dynamic => Layers.Moving,
            _ => Layer
        };

        BodyCreationSettings settings = new BodyCreationSettings(Shape, Transform.Position, Transform.Rotation, MotionType, Layer);

        Body = BodyInterface.CreateBody(settings);
        Body.Friction = Friction;
        Body.Restitution = Restitution;
        
        BodyInterface.AddBody(Body, Activation.Activate);
        BodyId = Body.ID;
    }

    protected internal override void PostUpdate()
    {
        BodyInterface.SetPositionAndRotationWhenChanged(BodyId, new Double3(Transform.Position.X, Transform.Position.Y, Transform.Position.Z), Transform.Rotation, Activation.Activate);
    }

    protected internal override void Update()
    {
        Transform.Position = BodyInterface.GetPosition(BodyId);
        Transform.Rotation = BodyInterface.GetRotation(BodyId);   
    }

    protected internal override void Stop()
    {
        BodyInterface.RemoveBody(BodyId);
        BodyInterface.DestroyBody(BodyId);
        Shape.Dispose();
    }


    protected internal override void GizmosDraw()
    {
        if(shapeGizmos.TryGetValue(Shape.GetType(), out var action))
            action.Invoke(Shape, Transform);
    }

    private static readonly Dictionary<Type, Action<Shape, Transform>> shapeGizmos = new()
    {
        { typeof(BoxShape), BoxShapeGizmo }
    };

    private static void BoxShapeGizmo(Shape shape, Transform transform)
    {
        var boxShape = (BoxShape)shape;
        ModelUtil.DrawCubeWires(transform.Position, boxShape.HalfExtent * 2, Color.Green);
    }
    
}