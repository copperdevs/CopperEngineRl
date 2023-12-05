using System.Numerics;
using CopperEngine.Utility;
using Jitter2;
using Jitter2.Collision.Shapes;
using Jitter2.Dynamics;
using Color = CopperEngine.Data.Color;
using JitterRigidbody = Jitter2.Dynamics.RigidBody;
using Transform = CopperEngine.Data.Transform;

namespace CopperEngine.Components;

public class Rigidbody : Component
{
    public World Simulation => ParentScene.PhysicsWorld;

    public JitterRigidbody JitterRigidbody;
    public Shape RigidbodyShape;

    private bool isStatic = false;

    public Rigidbody(Shape rigidbodyShape, bool isStatic = false)
    {
        RigidbodyShape = rigidbodyShape;
        this.isStatic = isStatic;
    }

    public Rigidbody(Shape rigidbodyShape, Vector3 startPos, bool isStatic = false)
    {
        RigidbodyShape = rigidbodyShape;
        this.isStatic = isStatic;
        Transform.Position = startPos;
    }


    protected internal override void Start()
    {
        JitterRigidbody = ParentScene.PhysicsWorld.CreateRigidBody();
        JitterRigidbody.AddShape(RigidbodyShape);
        JitterRigidbody.Position = Transform.Position.ToJVector();
        JitterRigidbody.IsStatic = isStatic;
    }

    protected internal override void PreUpdate()
    {
        Transform.Matrix = JitterRigidbody.GetTransformMatrix().ToRowMajor();
    }

    protected internal override void PostUpdate()
    {
        // if(Transform.Matrix != JitterRigidbody.GetTransformMatrix())
            // idk update rigidbody
    }

    protected internal override void Update()
    {
        
    }

    protected internal override void Stop()
    {
        
    }


    protected internal override void GizmosDraw()
    {
        
    }

    private static readonly Dictionary<Type, Action<Shape, Transform>> shapeGizmos = new()
    {
        { typeof(BoxShape), BoxShapeGizmo }
    };
    
    private static void BoxShapeGizmo(Shape shape, Transform transform)
    {
        var boxShape = (BoxShape)shape;
        ModelUtil.DrawCubeWires(transform.Position, boxShape.Size.ToVector3(), Color.Green);
    }
    
}