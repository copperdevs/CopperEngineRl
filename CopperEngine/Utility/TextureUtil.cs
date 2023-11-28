using System.Numerics;
using Raylib_cs;

namespace CopperEngine.Utility;

public static class TextureUtil
{
    /// <inheritdoc cref="Raylib.LoadTexture(string)"/>
    public static Texture2D Load(string path) => Raylib.LoadTexture(path);
    
    /// <inheritdoc cref="Raylib.LoadTextureFromImage"/>
    public static Texture2D LoadFromImage(Image image) => Raylib.LoadTextureFromImage(image);
    
    /// <inheritdoc cref="Raylib.LoadTextureCubemap"/>
    public static Texture2D LoadCubemap(Image image, CubemapLayout layout) => Raylib.LoadTextureCubemap(image, layout);
    
    /// <inheritdoc cref="Raylib.LoadRenderTexture"/>
    public static RenderTexture2D LoadRenderTexture(int width, int height) => Raylib.LoadRenderTexture(width, height);
    
    /// <inheritdoc cref="Raylib.UnloadTexture"/>
    public static void Unload(Texture2D texture) => Raylib.UnloadTexture(texture);
    
    /// <inheritdoc cref="Raylib.UnloadRenderTexture"/>
    public static void UnloadRenderTexture(RenderTexture2D target) => Raylib.UnloadRenderTexture(target);
    
    
    /// <inheritdoc cref="Raylib.IsTextureReady"/>
    public static bool IsReady(Texture2D texture) => Raylib.IsTextureReady(texture);
    
    /// <inheritdoc cref="Raylib.IsRenderTextureReady"/>
    public static bool IsRenderTextureReady(RenderTexture2D target) => Raylib.IsRenderTextureReady(target);
    
    /// <inheritdoc cref="Raylib.UpdateTexture"/>
    public static void Update<T>(Texture2D texture, ReadOnlySpan<T> pixels) where T : unmanaged => Raylib.UpdateTexture(texture, pixels);
    
    /// <inheritdoc cref="Raylib.UpdateTexture"/>
    public static void Update<T>(Texture2D texture, T[] pixels) where T : unmanaged => Raylib.UpdateTexture(texture, pixels);
    
    /// <inheritdoc cref="Raylib.UpdateTextureRec"/>
    public static void UpdateRec<T>(Texture2D texture, Rectangle rec, ReadOnlySpan<T> pixels) where T : unmanaged => Raylib.UpdateTextureRec(texture, rec, pixels);
    
    /// <inheritdoc cref="Raylib.UpdateTextureRec"/>
    public static void UpdateRec<T>(Texture2D texture, Rectangle rec, T[] pixels) where T : unmanaged => Raylib.UpdateTextureRec(texture, rec, pixels);

    
    /// <inheritdoc cref="Raylib.GenTextureMipmaps(ref Texture2D)"/>
    public static void GenMipmaps(ref Texture2D texture) => Raylib.GenTextureMipmaps(ref texture);
    
    /// <inheritdoc cref="Raylib.SetTextureFilter"/>
    public static void SetFilter(Texture2D texture, TextureFilter filter) => Raylib.SetTextureFilter(texture, filter);
    
    /// <inheritdoc cref="Raylib.SetTextureWrap"/>
    public static void SetWrap(Texture2D texture, TextureWrap wrap) => Raylib.SetTextureWrap(texture, wrap);

    
    /// <inheritdoc cref="Raylib.DrawTexture"/>
    public static void Draw(Texture2D texture, int posX, int posY, Color color) => Raylib.DrawTexture(texture, posX, posY, color);
    
    /// <inheritdoc cref="Raylib.DrawTextureV"/>
    public static void Draw(Texture2D texture, Vector2 pos, Color color) => Raylib.DrawTextureV(texture, pos, color);
    
    /// <inheritdoc cref="Raylib.DrawTextureEx"/>
    public static void Draw(Texture2D texture, Vector2 pos, float rotation, float scale, Color color) => Raylib.DrawTextureEx(texture, pos, rotation, scale, color);
    
    /// <inheritdoc cref="Raylib.DrawTextureRec"/>
    public static void DrawRec(Texture2D texture, Rectangle source, Vector2 pos, Color color) => Raylib.DrawTextureRec(texture, source, pos, color);
    
    /// <inheritdoc cref="Raylib.DrawTexturePro"/>
    public static void DrawPro(Texture2D texture, Rectangle source, Rectangle dest, Vector2 origin, float rotation, Color color) => Raylib.DrawTexturePro(texture, source, dest, origin, rotation, color);
    
    /// <inheritdoc cref="Raylib.DrawTextureNPatch"/>
    public static void DrawNPatch(Texture2D texture, NPatchInfo nPatchInfo, Rectangle dest, Vector2 origin, float rotation, Color color) => Raylib.DrawTextureNPatch(texture, nPatchInfo, dest, origin, rotation, color);
    
    private struct GridTextureData
    {
        public readonly int Width;
        public readonly int Height;
        public readonly Color Color1;
        public readonly Color Color2;

        public GridTextureData(int width, int height, Color color1, Color color2)
        {
            Width = width;
            Height = height;
            Color1 = color1;
            Color2 = color2;
        }
    }

    private static Texture2D GenerateGridTexture(GridTextureData data)
    {
        var img = ImageUtil.GenChecked(data.Width, data.Height, 1, 1, data.Color1, data.Color2);
        var gridTexture = TextureUtil.LoadFromImage(img);
        ImageUtil.Unload(img);
        TextureUtil.GenMipmaps(ref gridTexture);
        TextureUtil.SetFilter(gridTexture, TextureFilter.TEXTURE_FILTER_ANISOTROPIC_16X);
        TextureUtil.SetWrap(gridTexture, TextureWrap.TEXTURE_WRAP_CLAMP);
        return gridTexture;
    }

    public static Texture2D GridTexture(int width, int height, Color color1, Color color2)
    {
        var data = new GridTextureData(width, height, color1, color2);

        return GenerateGridTexture(data);
    }

    public static Texture2D GridTexture(int width, int height)
    {
        return GridTexture(width, height, ColorUtil.Gray, ColorUtil.LightGray);
    }
}