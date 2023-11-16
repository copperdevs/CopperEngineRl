﻿using System.Numerics;
using Raylib_CsLo;

namespace CopperEngine.Utility;

public static class TextureUtil
{
    public static Texture Load(string path) => Raylib.LoadTexture(path);
    public static Texture LoadFromImage(Image image) => Raylib.LoadTextureFromImage(image);
    public static Texture LoadCubemap(Image image, CubemapLayout layout) => Raylib.LoadTextureCubemap(image, layout);
    public static RenderTexture LoadRenderTexture(int width, int height) => Raylib.LoadRenderTexture(width, height);
    public static void Unload(Texture texture) => Raylib.UnloadTexture(texture);
    public static void Unload(RenderTexture target) => Raylib.UnloadRenderTexture(target);
    
    public static unsafe void Update<T>(Texture texture, void* pixels) where T : unmanaged => Raylib.UpdateTexture(texture, pixels);
    public static void Update<T>(Texture texture, ref object pixels) where T : unmanaged
    {
        unsafe
        {
            fixed (void* pixelsPtr = &pixels)
                Raylib.UpdateTexture(texture, pixelsPtr);
        }
    }

    public static unsafe void Update<T>(Texture texture, T* pixels) where T : unmanaged => Raylib.UpdateTexture(texture, pixels);
    public static void Update<T>(Texture texture, ref T pixels) where T : unmanaged
    {
        unsafe
        {
            fixed (T* pixelsPtr = &pixels)
                Raylib.UpdateTexture(texture, pixelsPtr);
        }
    }

    public static unsafe void UpdateRec<T>(Texture texture, Rectangle rec, void* pixels) where T : unmanaged => Raylib.UpdateTextureRec(texture, rec, pixels);
    public static void UpdateRec<T>(Texture texture, Rectangle rec, ref object pixels) where T : unmanaged
    {
        unsafe
        {
            fixed (void* pixelsPtr = &pixels)
                Raylib.UpdateTextureRec(texture, rec, pixelsPtr);
        }
    }

    public static unsafe void GenMipmaps(Texture* texture) => Raylib.GenTextureMipmaps(texture);
    public static void GenMipmaps(ref Texture texture)
    {
        unsafe
        {
            fixed (Texture* texturePtr = &texture)
                Raylib.GenTextureMipmaps(texturePtr);
        }
    }

    public static void SetFilter(Texture texture, TextureFilter filter) => Raylib.SetTextureFilter(texture, filter);
    public static void SetWrap(Texture texture, TextureWrap wrap) => Raylib.SetTextureWrap(texture, wrap);
    public static void Draw(Texture texture, int posX, int posY, Color color) => Raylib.DrawTexture(texture, posX, posY, color);
    public static void Draw(Texture texture, Vector2 pos, Color color) => Raylib.DrawTextureV(texture, pos, color);
    public static void Draw(Texture texture, Vector2 pos, float rotation, float scale, Color color) => Raylib.DrawTextureEx(texture, pos, rotation, scale, color);
    public static void DrawRec(Texture texture, Rectangle source, Vector2 pos, Color color) => Raylib.DrawTextureRec(texture, source, pos, color);
    public static void DrawPro(Texture texture, Rectangle source, Rectangle dest, Vector2 origin, float rotation, Color color) => Raylib.DrawTexturePro(texture, source, dest, origin, rotation, color);
    public static void DrawNPatch(Texture texture, NPatchInfo nPatchInfo, Rectangle dest, Vector2 origin, float rotation, Color color) => Raylib.DrawTextureNPatch(texture, nPatchInfo, dest, origin, rotation, color);

    
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

    private static Texture GenerateGridTexture(GridTextureData data)
    {
        var img = ImageUtil.GenChecked(data.Width, data.Height, 1, 1, data.Color1, data.Color2);
        var gridTexture = TextureUtil.LoadFromImage(img);
        ImageUtil.Unload(img);
        TextureUtil.GenMipmaps(ref gridTexture);
        TextureUtil.SetFilter(gridTexture, TextureFilter.TEXTURE_FILTER_ANISOTROPIC_16X);
        TextureUtil.SetWrap(gridTexture, TextureWrap.TEXTURE_WRAP_CLAMP);
        return gridTexture;
    }

    public static Texture GridTexture(int width, int height, Color color1, Color color2)
    {
        var data = new GridTextureData(width, height, color1, color2);

        return GenerateGridTexture(data);
    }

    public static Texture GridTexture(int width, int height)
    {
        return GridTexture(width, height, ColorUtil.Gray, ColorUtil.LightGray);
    }
}