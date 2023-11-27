namespace CopperEngine.Editor.Attributes;

[AttributeUsage(AttributeTargets.Field, Inherited = false, AllowMultiple = false)]
public class RangeAttribute : Attribute
{
    public float Min;
    public float Max;

    public RangeAttribute(float min, float max)
    {
        Min = min;
        Max = max;
    }

    public RangeAttribute(float max) : this(0, max) { }
}