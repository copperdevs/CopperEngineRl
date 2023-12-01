namespace CopperEngine.Editor.Attributes;

[AttributeUsage(AttributeTargets.Field, Inherited = false, AllowMultiple = false)]
public class TooltipAttribute : Attribute
{
    public readonly string Message;

    public TooltipAttribute(string message)
    {
        this.Message = message;
    }
}