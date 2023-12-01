using System.Numerics;
using CopperEngine.Components;
using CopperEngine.Data;
using CopperEngine.Editor.Attributes;

namespace CopperEngine.Testing;

public class ReflectionComponent : GameComponent
{
    [Seperator("Single Values")]
    public float NormalFloatField;
    [Range(0, 1)] public float RangeFloatField;
    public int NormalIntField;
    [Range(0, 1)] public int RangeIntField;

    [Space(25)]
    
    [Seperator("Multi Values")]
    public Vector2 Vector2Field;
    public Vector3 Vector3Field;
    public Vector4 Vector4Field;
    public Quaternion QuaternionField = Quaternion.Identity;

    [Space(25)]
    
    [Seperator("Custom Types")]
    public Guid GuidField = Guid.NewGuid();
    public Color ColorField = Color.Red;

    [Space(25)]
    
    [Seperator("Attribute Testing")] 
    [Range(0, 1)] public float RangeTest;
    [ReadOnly] public float ReadOnlyTest;
    [Tooltip("test tooltip")] public float TooltipTest;
    [HideInInspector] public float HiddenValueTest;
}
