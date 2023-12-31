﻿using System.Numerics;
using System.Reflection;
using CopperEngine.Data;
using CopperEngine.Editor.Attributes;
using CopperEngine.Scenes;
using CopperEngine.Utility;
using ImGuiNET;
using Jitter2.Dynamics;

namespace CopperEngine.Editor.DearImGui;

internal static class ImGuiReflection
{
    private static RangeAttribute? currentRangeAttribute;
    private static ReadOnlyAttribute? currentReadOnlyAttribute;
    private static TooltipAttribute? currentTooltipAttribute;
    private static HideInInspectorAttribute? currentHideInInspectorAttribute;
    private static SpaceAttribute? currentSpaceAttribute;
    private static SeperatorAttribute? currentSeperatorAttribute;

    internal static void RenderValues(object component)
    {
        var fields = component.GetType().GetFields(BindingFlags.Instance | BindingFlags.Public).ToList();
        foreach (var info in fields)
        {
            SpaceAttributeRenderer(info);
            SeperatorAttributeRenderer(info);

            currentHideInInspectorAttribute =
                (HideInInspectorAttribute?)Attribute.GetCustomAttribute(info, typeof(HideInInspectorAttribute))!;

            if (currentHideInInspectorAttribute is not null)
                continue;

            currentReadOnlyAttribute =
                (ReadOnlyAttribute?)Attribute.GetCustomAttribute(info, typeof(ReadOnlyAttribute))!;

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
        if (currentSpaceAttribute is not null) currentSpaceAttribute.Render();
    }

    private static void SeperatorAttributeRenderer(FieldInfo info)
    {
        currentSeperatorAttribute =
            (SeperatorAttribute?)Attribute.GetCustomAttribute(info, typeof(SeperatorAttribute))!;
        if (currentSeperatorAttribute is not null) currentSeperatorAttribute.Render();
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
        { typeof(Jitter2.Dynamics.RigidBody), JitterRigidbodyRenderer },
    };

    private static void FloatFieldRenderer(FieldInfo fieldInfo, object component)
    {
        currentRangeAttribute = (RangeAttribute?)Attribute.GetCustomAttribute(fieldInfo, typeof(RangeAttribute))!;

        if (currentRangeAttribute is not null)
        {
            var value = (float)(fieldInfo.GetValue(component) ?? 0);
            if (ImGui.SliderFloat($"{fieldInfo.Name}##{fieldInfo.Name}", ref value, currentRangeAttribute.Min,
                    currentRangeAttribute.Max))
                fieldInfo.SetValue(component, value);
        }
        else
        {
            var value = (float)(fieldInfo.GetValue(component) ?? 0);
            if (ImGui.DragFloat($"{fieldInfo.Name}##{fieldInfo.Name}", ref value))
                fieldInfo.SetValue(component, value);
        }
    }

    private static void IntFieldRenderer(FieldInfo fieldInfo, object component)
    {
        currentRangeAttribute = (RangeAttribute?)Attribute.GetCustomAttribute(fieldInfo, typeof(RangeAttribute))!;

        if (currentRangeAttribute is not null)
        {
            var value = (int)(fieldInfo.GetValue(component) ?? 0);
            if (ImGui.SliderInt($"{fieldInfo.Name}##{fieldInfo.Name}", ref value, (int)currentRangeAttribute.Min,
                    (int)currentRangeAttribute.Max))
                fieldInfo.SetValue(component, value);
        }
        else
        {
            var value = (int)(fieldInfo.GetValue(component) ?? 0);
            if (ImGui.DragInt($"{fieldInfo.Name}##{fieldInfo.Name}", ref value))
                fieldInfo.SetValue(component, value);
        }
    }

    private static void BoolFieldRenderer(FieldInfo fieldInfo, object component)
    {
        var value = (bool)(fieldInfo.GetValue(component) ?? false);
        if (ImGui.Checkbox($"{fieldInfo.Name}##{fieldInfo.Name}", ref value))
            fieldInfo.SetValue(component, value);
    }

    private static void Vector2FieldRenderer(FieldInfo fieldInfo, object component)
    {
        var value = (Vector2)(fieldInfo.GetValue(component) ?? Vector2.Zero);
        if (ImGui.DragFloat2($"{fieldInfo.Name}##{fieldInfo.Name}", ref value))
            fieldInfo.SetValue(component, value);
    }

    private static void Vector3FieldRenderer(FieldInfo fieldInfo, object component)
    {
        var value = (Vector3)(fieldInfo.GetValue(component) ?? Vector3.Zero);
        if (ImGui.DragFloat3($"{fieldInfo.Name}##{fieldInfo.Name}", ref value))
            fieldInfo.SetValue(component, value);
    }

    private static void Vector4FieldRenderer(FieldInfo fieldInfo, object component)
    {
        var value = (Vector4)(fieldInfo.GetValue(component) ?? Vector4.Zero);
        if (ImGui.DragFloat4($"{fieldInfo.Name}##{fieldInfo.Name}", ref value))
            fieldInfo.SetValue(component, value);
    }

    private static void QuaternionFieldRenderer(FieldInfo fieldInfo, object component)
    {
        var value = ((Quaternion)(fieldInfo.GetValue(component) ?? Quaternion.Identity)).ToVector();
        if (ImGui.DragFloat4($"{fieldInfo.Name}##{fieldInfo.Name}", ref value))
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
            if (ImGui.DragFloat3("Position", ref position, 0.1f))
            {
                value.Position = position;
                fieldInfo.SetValue(component, value);
            }

            var scale = value.Scale;
            if (ImGui.DragFloat3("Scale", ref scale, 0.1f))
            {
                value.Scale = scale;
                fieldInfo.SetValue(component, value);
            }

            var rotation = value.Rotation.ToEulerAngles();
            if (ImGui.DragFloat3("Rotation", ref rotation, 0.1f))
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

    private static void JitterRigidbodyRenderer(FieldInfo fieldInfo, object component)
    {
        var value = ((RigidBody)(fieldInfo.GetValue(component) ?? null!)).Data;

        var index = value._index;
        ImGui.DragInt("Index", ref index);

        var lockFlag = value._lockFlag;
        ImGui.DragInt("Lock Flag", ref lockFlag);

        var position = value.Position.ToVector3();
        ImGui.DragFloat3("Position", ref position);

        var velocity = value.Velocity.ToVector3();
        ImGui.DragFloat3("Velocity", ref velocity);

        var angularVelocity = value.AngularVelocity.ToVector3();
        ImGui.DragFloat3("Angular Velocity", ref angularVelocity);

        var deltaVelocity = value.DeltaVelocity.ToVector3();
        ImGui.DragFloat3("Delta Velocity", ref deltaVelocity);

        var deltaAngularVelocity = value.DeltaAngularVelocity.ToVector3();
        ImGui.DragFloat3("Delta Angular Velocity", ref deltaAngularVelocity);

        var orientation = value.Orientation.ToMatrix4X4();
        EditorUtil.DragMatrix4X4("Orientation", ref orientation);

        var inverseInertiaWorld = value.InverseInertiaWorld.ToMatrix4X4();
        EditorUtil.DragMatrix4X4("Inverse Inertia World", ref inverseInertiaWorld);

        var inverseMass = value.InverseMass;
        ImGui.DragFloat("Inverse Mass", ref inverseMass);

        var isActive = value.IsActive;
        ImGui.Checkbox("Is Active", ref isActive);

        var isStatic = value.IsStatic;
        ImGui.Checkbox("Is Static", ref isStatic);
    }
}