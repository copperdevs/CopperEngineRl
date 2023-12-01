using System.Numerics;
using CopperEngine.Components;
using CopperEngine.Data;
using CopperEngine.Editor.Attributes;

namespace CopperEngine.Testing;

public class ReflectionComponent : GameComponent
{
    public float NormalFloatField;
    [Range(0, 1)] public float RangeFloatField;
    public int NormalIntField;
    [Range(0, 1)] public int RangeIntField;

    public Vector2 Vector2Field;
    public Vector3 Vector3Field;
    public Vector4 Vector4Field;
    public Quaternion QuaternionField = Quaternion.Identity;

    public Guid GuidField = Guid.NewGuid();
    public Color ColorField = Color.Red;

    [ReadOnly] public float ReadOnlyTest;
    [Tooltip("test tooltip")] public float TooltipTest;
}
