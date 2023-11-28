﻿using System.Numerics;
using CopperEngine.Utils;
using Raylib_cs;
using static Raylib_cs.Raylib;

namespace CopperEngine.Utility;

public static class Extensions
{
    public static void WithTexture(this Material material, Texture2D texture)
    {
        unsafe
        {
            
            material.Maps[(int)MaterialMapIndex.MATERIAL_MAP_DIFFUSE].Texture = texture;
        }
    }

    public static void WithTexture(this Model model, Texture2D texture)
    {
        unsafe
        {
            model.Materials[0].WithTexture(texture);
        }
    }

    public static void WithGridTexture(this ref Model model, int width, int height) => model.WithTexture(TextureUtil.GridTexture(width, height));
    public static void WithGridTexture(this ref Model model, int width, int height, Color color1, Color color2) => model.WithTexture(TextureUtil.GridTexture(width, height, color1, color2));
    public static void WithGridTexture(this ref Material material, int width, int height) => material.WithTexture(TextureUtil.GridTexture(width, height));
    public static void WithGridTexture(this ref Material material, int width, int height, Color color1, Color color2) => material.WithTexture(TextureUtil.GridTexture(width, height, color1, color2));

    
    public static Vector4 ToVector(this Quaternion quaternion) => new(quaternion.X, quaternion.Y, quaternion.Z, quaternion.W);
    public static Quaternion ToQuaternion(this Vector4 vector) => new(vector.X, vector.Y, vector.Z, vector.W);
    
    public static Quaternion FromEulerAngles(this Vector3 euler) => MathUtil.FromEulerAngles(euler);
    public static Vector3 ToEulerAngles(this Quaternion quaternion) => MathUtil.ToEulerAngles(quaternion);
    
    public static Vector3 AreaInSphere(this Random random)
    {
        var xVal = (random.NextDouble() * 2) - 1;
        var yVal = (random.NextDouble() * 2) - 1;
        var zVal = (random.NextDouble() * 2) - 1;
        return Vector3.Normalize(new Vector3((float)xVal, (float)yVal, (float)zVal));
    }

    public static Vector3 Scale(this Vector3 vector, float scale)
    {
        return vector.Scale(new Vector3(scale));
    }

    public static Vector3 Scale(this Vector3 vec1, Vector3 vec2)
    {
        return new Vector3(vec1.X * vec2.X, vec1.Y * vec2.Y, vec1.Z * vec2.Z);
    }

    public static Vector3 WithX(this Vector3 vector, float value) => vector with { X = value };
    public static Vector3 WithY(this Vector3 vector, float value) => vector with { Y = value };
    public static Vector3 WithZ(this Vector3 vector, float value) => vector with { Z = value };
    public static Vector3 Clamp(this Vector3 value, Vector3 min, Vector3 max) => MathUtil.Clamp(value, min, max);
    

    public static Vector4 WithX(this Vector4 vector, float value) => vector with { X = value };
    public static Vector4 WithY(this Vector4 vector, float value) => vector with { Y = value };
    public static Vector4 WithZ(this Vector4 vector, float value) => vector with { Z = value };
    public static Vector4 WithW(this Vector4 vector, float value) => vector with { W = value };
    
    
    public static string ToFancyString(this IEnumerable<byte> array) => array.Aggregate("", (current, item) => current + $"<{item}>,");
    public static string CapitalizeFirstLetter(this string message)
    {
        return message.Length switch
        {
            0 => "",
            1 => char.ToUpper(message[0]).ToString(),
            _ => char.ToUpper(message[0]) + message[1..]
        };;
    }

    public static Vector2 Remap(this Vector2 value, Vector2 iMin, Vector2 iMax, Vector2 oMin, Vector2 oMax) => MathUtil.Remap(iMin, iMax, oMin, oMax, value);
    
    public static Matrix4x4 ToRowMajor(this Matrix4x4 columnMatrix)
    {
        return new Matrix4x4(
            columnMatrix.M11, columnMatrix.M21, columnMatrix.M31, columnMatrix.M41,
            columnMatrix.M12, columnMatrix.M22, columnMatrix.M32, columnMatrix.M42,
            columnMatrix.M13, columnMatrix.M23, columnMatrix.M33, columnMatrix.M43,
            columnMatrix.M14, columnMatrix.M24, columnMatrix.M34, columnMatrix.M44
        );
    }

    // Extension method to convert a row-major Matrix4x4 to a column-major Matrix4x4
    public static Matrix4x4 ToColumnMajor(this Matrix4x4 rowMatrix)
    {
        return new Matrix4x4(
            rowMatrix.M11, rowMatrix.M12, rowMatrix.M13, rowMatrix.M14,
            rowMatrix.M21, rowMatrix.M22, rowMatrix.M23, rowMatrix.M24,
            rowMatrix.M31, rowMatrix.M32, rowMatrix.M33, rowMatrix.M34,
            rowMatrix.M41, rowMatrix.M42, rowMatrix.M43, rowMatrix.M44
        );
    }
}