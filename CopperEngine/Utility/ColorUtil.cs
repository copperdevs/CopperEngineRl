using System.Numerics;
using Raylib_cs;
using Color = CopperEngine.Data.Color;
using rlColor = Raylib_cs.Color;

namespace CopperEngine.Utility;

public static class ColorUtil
{
    public static Color LightGray => new(200, 200, 200, 255);
    public static Color Gray => new(130, 130, 130, 255);
    public static Color DarkGray => new(80, 80, 80, 255);
    public static Color Yellow => new(253, 249, 0, 255);
    public static Color Gold => new(255, 203, 0, 255);
    public static Color Orange => new(255, 161, 0, 255);
    public static Color Pink => new(255, 109, 194, 255);
    public static Color Red => new(230, 41, 55, 255);
    public static Color Maroon => new(190, 33, 55, 255);
    public static Color Green => new(0, 228, 48, 255);
    public static Color Lime => new(0, 158, 47, 255);
    public static Color DarkGreen => new(0, 117, 44, 255);
    public static Color SkyBlue => new(102, 191, 255, 255);
    public static Color Blue => new(0, 121, 241, 255);
    public static Color DarkBlue => new(0, 82, 172, 255);
    public static Color Purple => new(200, 122, 255, 255);
    public static Color Violet => new(135, 60, 190, 255);
    public static Color DarkPurple => new(112, 31, 126, 255);
    public static Color Beige => new(211, 176, 131, 255);
    public static Color Brown => new(127, 106, 79, 255);
    public static Color DarkBrown => new(76, 63, 47, 255);
    public static Color White => new(255, 255, 255, 255);
    public static Color Black => new(0, 0, 0, 255);
    public static Color Blank => new(0, 0, 0, 0);
    public static Color Magenta => new(255, 0, 255, 255);
    public static Color RayWhite => new(245, 245, 245, 255);

    public static Vector4 ToImGuiColor(this Color color)
    {
        return color / 255;
    }

    public static Vector4 FromImGuiColor(this Color color)
    {
        return color * 255;
    }

    /// <inheritdoc cref="Raylib_cs.Raylib.Fade"/>
    public static rlColor Fade(rlColor color, float alpha) => Raylib.Fade(color, alpha);

    /// <inheritdoc cref="Raylib.ColorToInt"/>
    public static int ToInt(rlColor color) => Raylib.ColorToInt(color);

    /// <inheritdoc cref="Raylib.ColorNormalize"/>
    public static Vector4 Normalize(rlColor color) => Raylib.ColorNormalize(color);

    /// <inheritdoc cref="Raylib.ColorFromNormalized"/>
    public static rlColor FromNormalized(Vector4 normalized) => Raylib.ColorFromNormalized(normalized);

    /// <inheritdoc cref="Raylib.ColorToHSV"/>
    public static Vector3 ToHsv(rlColor color) => Raylib.ColorToHSV(color);

    /// <inheritdoc cref="Raylib.ColorFromHSV"/>
    public static rlColor FromHsv(float hue, float saturation, float value) =>
        Raylib.ColorFromHSV(hue, saturation, value);

    /// <inheritdoc cref="Raylib.ColorTint"/>
    public static rlColor Tint(rlColor color, rlColor tint) => Raylib.ColorTint(color, tint);

    /// <inheritdoc cref="Raylib.ColorBrightness"/>
    public static rlColor Brightness(rlColor color, float factor) => Raylib.ColorBrightness(color, factor);

    /// <inheritdoc cref="Raylib.ColorContrast"/>
    public static rlColor Contrast(rlColor color, float contrast) => Raylib.ColorContrast(color, contrast);

    /// <inheritdoc cref="Raylib.ColorAlpha"/>
    public static rlColor Alpha(rlColor color, float alpha) => Raylib.ColorAlpha(color, alpha);

    /// <inheritdoc cref="Raylib.ColorAlphaBlend"/>
    public static rlColor AlphaBlend(rlColor dst, rlColor src, rlColor tint) => Raylib.ColorAlphaBlend(dst, src, tint);

    /// <inheritdoc cref="Raylib.GetColor"/>
    public static rlColor Get(uint hexValue) => Raylib.GetColor(hexValue);

    /// <inheritdoc cref="Raylib.GetPixelDataSize"/>
    public static int GetPixelDataSize(int width, int height, PixelFormat format) =>
        Raylib.GetPixelDataSize(width, height, format);

    /// <inheritdoc cref="Raylib.GetPixelColor"/>
    public static unsafe rlColor GetPixel(void* srcPtr, PixelFormat format) => Raylib.GetPixelColor(srcPtr, format);

    /// <inheritdoc cref="Raylib.SetPixelColor"/>
    public static unsafe void SetPixel(void* dstPtr, rlColor color, PixelFormat format) =>
        Raylib.SetPixelColor(dstPtr, color, format);
}