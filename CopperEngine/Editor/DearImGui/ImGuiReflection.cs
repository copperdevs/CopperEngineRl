using System.Numerics;
using System.Reflection;
using CopperEngine.Data;
using CopperEngine.Editor.Attributes;
using CopperEngine.Physics;
using CopperEngine.Scenes;
using CopperEngine.Utility;
using CopperEngine.Utils;
using ImGuiNET;
using JoltPhysicsSharp;

namespace CopperEngine.Editor.DearImGui;

public static class ImGuiReflection
{
    private static RangeAttribute? currentRangeAttribute;
    private static ReadOnlyAttribute? currentReadOnlyAttribute;
    private static TooltipAttribute? currentTooltipAttribute;
    private static HideInInspectorAttribute? currentHideInInspectorAttribute;
    private static SpaceAttribute? currentSpaceAttribute;
    private static SeperatorAttribute? currentSeperatorAttribute;
    
    public static void RenderValues(object component)
    {
        var fields = component.GetType().GetFields(BindingFlags.Instance | BindingFlags.Public).ToList();
        foreach (var info in fields)
        {
            SpaceAttributeRenderer(info);
            SeperatorAttributeRenderer(info);
            
            currentHideInInspectorAttribute = (HideInInspectorAttribute?)Attribute.GetCustomAttribute(info, typeof(HideInInspectorAttribute))!;
            
            if(currentHideInInspectorAttribute is not null)
                continue;
            
            currentReadOnlyAttribute = (ReadOnlyAttribute?)Attribute.GetCustomAttribute(info, typeof(ReadOnlyAttribute))!;

            if (currentReadOnlyAttribute is not null)
            {
                ImGui.BeginDisabled();
                if (ImGuiRenderers.TryGetValue(info.FieldType, out var action))
                    action.Invoke(info, component);
                else
                    ImGui.LabelText("Unsupported editor value", info.FieldType.FullName);
                ImGui.EndDisabled();
            }
            else
            {
                if (ImGuiRenderers.TryGetValue(info.FieldType, out var action))
                    action.Invoke(info, component);
                else
                    ImGui.LabelText("Unsupported editor value", info.FieldType.FullName);
            }

            currentTooltipAttribute = (TooltipAttribute)Attribute.GetCustomAttribute(info, typeof(TooltipAttribute))!;

            if (currentTooltipAttribute is not null)
            {
                if (ImGui.BeginItemTooltip())
                {
                    ImGui.PushTextWrapPos(ImGui.GetFontSize() * 35.0f);
                    ImGui.TextUnformatted(currentTooltipAttribute.Message);
                    ImGui.PopTextWrapPos();
                    ImGui.EndTooltip();
                }
            }

        }
    }

    private static void SpaceAttributeRenderer(FieldInfo info)
    {
        currentSpaceAttribute = (SpaceAttribute?)Attribute.GetCustomAttribute(info, typeof(SpaceAttribute))!;
        if(currentSpaceAttribute is not null) currentSpaceAttribute.Render();
    }

    private static void SeperatorAttributeRenderer(FieldInfo info)
    {
        currentSeperatorAttribute = (SeperatorAttribute?)Attribute.GetCustomAttribute(info, typeof(SeperatorAttribute))!;
        if(currentSeperatorAttribute is not null) currentSeperatorAttribute.Render();
    }
    
    private static readonly Dictionary<Type, Action<FieldInfo, object>> ImGuiRenderers = new()
    {
        { typeof(float), FloatFieldRenderer },
        { typeof(int), IntFieldRenderer },
        { typeof(bool), BoolFieldRenderer },
        { typeof(Vector2), Vector2FieldRenderer },
        { typeof(Vector3), Vector3FieldRenderer },
        { typeof(Vector4), Vector4FieldRenderer },
        { typeof(Quaternion), QuaternionFieldRenderer },
        { typeof(Guid), GuidFieldRenderer },
        { typeof(Scene), SceneFieldRenderer },
        { typeof(Transform), TransformFieldRenderer },
        { typeof(Color), ColorFieldRenderer },
        { typeof(BodyID), JoltPhysicsBodyIdRenderer },
        { typeof(ObjectLayer), JoltPhysicsObjectLayerRenderer },
        { typeof(MotionType), JoltPhysicsMotionTypeLayerRenderer },
        { typeof(Body), JoltPhysicsBodyRenderer }
    };
    
    private static void FloatFieldRenderer(FieldInfo fieldInfo, object component)
    {
        currentRangeAttribute = (RangeAttribute?)Attribute.GetCustomAttribute(fieldInfo, typeof(RangeAttribute))!;

        if (currentRangeAttribute is not null)
        {
            var value = (float)(fieldInfo.GetValue(component) ?? 0);
            if(ImGui.SliderFloat($"{fieldInfo.Name}##{fieldInfo.Name}", ref value, currentRangeAttribute.Min, currentRangeAttribute.Max))
                fieldInfo.SetValue(component, value); 
        }
        else
        {
            var value = (float)(fieldInfo.GetValue(component) ?? 0);
            if(ImGui.DragFloat($"{fieldInfo.Name}##{fieldInfo.Name}", ref value))
                fieldInfo.SetValue(component, value);   
        }
    }
    
    private static void IntFieldRenderer(FieldInfo fieldInfo, object component)
    {
        currentRangeAttribute = (RangeAttribute?)Attribute.GetCustomAttribute(fieldInfo, typeof(RangeAttribute))!;

        if (currentRangeAttribute is not null)
        {
            var value = (int)(fieldInfo.GetValue(component) ?? 0);
            if(ImGui.SliderInt($"{fieldInfo.Name}##{fieldInfo.Name}", ref value, (int)currentRangeAttribute.Min, (int)currentRangeAttribute.Max))
                fieldInfo.SetValue(component, value); 
        }
        else
        {
            var value = (int)(fieldInfo.GetValue(component) ?? 0);
            if(ImGui.DragInt($"{fieldInfo.Name}##{fieldInfo.Name}", ref value))
                fieldInfo.SetValue(component, value);
        }
    }
    
    private static void BoolFieldRenderer(FieldInfo fieldInfo, object component)
    {
        var value = (bool)(fieldInfo.GetValue(component) ?? false);
        if(ImGui.Checkbox($"{fieldInfo.Name}##{fieldInfo.Name}", ref value))
            fieldInfo.SetValue(component, value);
    }
    
    private static void Vector2FieldRenderer(FieldInfo fieldInfo, object component)
    {
        var value = (Vector2)(fieldInfo.GetValue(component) ?? Vector2.Zero);
        if(ImGui.DragFloat2($"{fieldInfo.Name}##{fieldInfo.Name}", ref value))
            fieldInfo.SetValue(component, value);
    }
    
    private static void Vector3FieldRenderer(FieldInfo fieldInfo, object component)
    {
        var value = (Vector3)(fieldInfo.GetValue(component) ?? Vector3.Zero);
        if(ImGui.DragFloat3($"{fieldInfo.Name}##{fieldInfo.Name}", ref value))
            fieldInfo.SetValue(component, value);
    }
    
    private static void Vector4FieldRenderer(FieldInfo fieldInfo, object component)
    {
        var value = (Vector4)(fieldInfo.GetValue(component) ?? Vector4.Zero);
        if(ImGui.DragFloat4($"{fieldInfo.Name}##{fieldInfo.Name}", ref value))
            fieldInfo.SetValue(component, value);
    }
    
    private static void QuaternionFieldRenderer(FieldInfo fieldInfo, object component)
    {
        var value = ((Quaternion)(fieldInfo.GetValue(component) ?? Quaternion.Identity)).ToVector();
        if(ImGui.DragFloat4($"{fieldInfo.Name}##{fieldInfo.Name}", ref value))
        {
            var result = value.ToQuaternion();
            fieldInfo.SetValue(component, result);
        }
    }
    
    private static void GuidFieldRenderer(FieldInfo fieldInfo, object component)
    {
        var value = (Guid)(fieldInfo.GetValue(component) ?? new Guid());
        ImGui.LabelText($"{fieldInfo.Name}##{fieldInfo.Name}", value.ToString());
    }
    
    private static void SceneFieldRenderer(FieldInfo fieldInfo, object component)
    {
        var value = (Scene)(fieldInfo.GetValue(component) ?? null)!;

        if (ImGui.CollapsingHeader($"{fieldInfo.Name}##{fieldInfo.Name}"))
        {
            ImGui.LabelText("Scene Name", value.DisplayName);
            ImGui.LabelText("Scene Id", value.SceneId.ToString());
        }
    }
    
    private static void TransformFieldRenderer(FieldInfo fieldInfo, object component)
    {
        var value = (Transform)(fieldInfo.GetValue(component) ?? 0);
        
        if (ImGui.CollapsingHeader($"{fieldInfo.Name}##{fieldInfo.Name}"))
        {
            ImGui.Indent();

            var position = value.Position;
            if(ImGui.DragFloat3("Position", ref position, 0.1f))
            {
                value.Position = position;
                fieldInfo.SetValue(component, value);
            }

            var scale = value.Scale;
            if(ImGui.DragFloat3("Scale", ref scale, 0.1f))
            {
                value.Scale = scale;
                fieldInfo.SetValue(component, value);
            }

            var rotation = value.Rotation.ToEulerAngles();
            if(ImGui.DragFloat3("Rotation", ref rotation, 0.1f))
            {
                value.Rotation = rotation.FromEulerAngles();
                fieldInfo.SetValue(component, value);
            }

            ImGui.Unindent();
        }
    }

    private static void ColorFieldRenderer(FieldInfo fieldInfo, object component)
    {
        var value = (Color)(fieldInfo.GetValue(component) ?? new Color(0));
        var color = value / 255;
        Vector4 vecColor = color;
        if (ImGui.ColorEdit4($"{fieldInfo.Name}##{fieldInfo.Name}", ref vecColor))
        {
            fieldInfo.SetValue(component, new Color(vecColor * 255));
        }
    }

    private static void JoltPhysicsBodyIdRenderer(FieldInfo fieldInfo, object component)
    {
        var value = (int)((BodyID)(fieldInfo.GetValue(component) ?? 0)).ID;
        ImGui.DragInt($"{fieldInfo.Name}##{fieldInfo.Name}", ref value);
    }

    private static void JoltPhysicsObjectLayerRenderer(FieldInfo fieldInfo, object component)
    {
        var value = (int)((ObjectLayer)(fieldInfo.GetValue(component) ?? 0)).Value;

        switch (value)
        {
            case Layers.NonMoving:
                ImGui.LabelText("Layer", "Non Moving");
                break;
            case Layers.Moving:
                ImGui.LabelText("Layer", "Moving");
                break;
        }
    }

    private static void JoltPhysicsMotionTypeLayerRenderer(FieldInfo fieldInfo, object component)
    {
        var value = ((MotionType)(fieldInfo.GetValue(component) ?? 0));
        
        switch (value)
        {
            case MotionType.Static:
                ImGui.LabelText("Motion Type", "Static");
                break;
            case MotionType.Kinematic:
                ImGui.LabelText("Motion Type", "Kinematic");
                break;
            case MotionType.Dynamic:
                ImGui.LabelText("Motion Type", "Dynamic");
                break;
            default:
                ImGui.LabelText("Motion Type", "???");
                break;
        }
    }

    private static void JoltPhysicsBodyRenderer(FieldInfo fieldInfo, object component)
    {
        var value = (Body)(fieldInfo.GetValue(component) ?? 0);
        
        if (ImGui.CollapsingHeader($"{fieldInfo.Name}##name"))
        {
            ImGui.Indent();
            
            switch (value.MotionType)
            {
                case MotionType.Static:
                    ImGui.LabelText("Motion Type", "Static");
                    break;
                case MotionType.Kinematic:
                    ImGui.LabelText("Motion Type", "Kinematic");
                    break;
                case MotionType.Dynamic:
                    ImGui.LabelText("Motion Type", "Dynamic");
                    break;
                default:
                    ImGui.LabelText("Motion Type", "???");
                    break;
            }
            var allowSleeping = value.AllowSleeping;
            ImGui.Checkbox("Allow Sleeping", ref allowSleeping);
            var friction = value.Friction;
            ImGui.DragFloat("Friction", ref friction);
            var restitution = value.Restitution;
            ImGui.DragFloat("Restitution", ref restitution);
            var position = (Vector3)value.Position;
            ImGui.DragFloat3("Position", ref position);
            var rotation = new Vector4(value.Rotation.X, value.Rotation.Y, value.Rotation.Z, value.Rotation.W);
            ImGui.DragFloat4("Rotation", ref rotation);
            var centerOfMassPosition = (Vector3)value.CenterOfMassPosition;
            ImGui.DragFloat3("Center of Mass Position", ref centerOfMassPosition);
            // var worldTransform = value.WorldTransform;
            // EditorUtil.DragMatrix4X4("World Transform", ref worldTransform);
            // var centerOfMassTransform = value.CenterOfMassTransform;
            // EditorUtil.DragMatrix4X4("Center of Mass Transform", ref centerOfMassTransform);
            
            ImGui.Unindent();
        }
    }
}