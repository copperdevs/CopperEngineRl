﻿using System.Numerics;
using Raylib_CsLo;

namespace CopperEngine.Utility;

public static class ShapeUtil
{
    public static void SetShapesTexture(Texture texture, Rectangle source) => Raylib.SetShapesTexture(texture, source);
    public static void DrawPixel(int posX, int posY, Color color) => Raylib.DrawPixel(posX, posY, color);
    public static void DrawPixel(Vector2 pos, Color color) => Raylib.DrawPixelV(pos, color);
    public static void DrawLine(int startPosX, int startPosY, int endPosX, int endPosY, Color color) => Raylib.DrawLine(startPosX, startPosY, endPosX, endPosY, color);
    public static void DrawLine(Vector2 startPos, Vector2 endPos, Color color) => Raylib.DrawLineV(startPos, endPos, color);
    public static void DrawLine(Vector2 startPos, Vector2 endPos, float thick, Color color) => Raylib.DrawLineEx(startPos, endPos, thick, color);
    public static void DrawLineBezier(Vector2 startPos, Vector2 endPos, float thick, Color color) => Raylib.DrawLineBezier(startPos, endPos, thick, color);
    public static void DrawLineBezierQuad(Vector2 startPos, Vector2 endPos, Vector2 controlPos, float thick, Color color) => Raylib.DrawLineBezierQuad(startPos, endPos, controlPos, thick, color);
    public static void DrawLineBezierCubic(Vector2 startPos, Vector2 endPos, Vector2 startControlPos, Vector2 endControlPos, float thick, Color color) => Raylib.DrawLineBezierCubic(startPos, endPos, startControlPos, endControlPos, thick, color);
    
    [Obsolete("Now has a version that does not use unsafe code")]
    public static unsafe void DrawLineStrip(Vector2* points, int pointCount, Color color) => Raylib.DrawLineStrip(points, pointCount, color);
    public static void DrawLineStrip(ref Vector2 points, int pointCount, Color color)
    {
        unsafe
        {
            fixed (Vector2* pointsPtr = &points)
                Raylib.DrawLineStrip(pointsPtr, pointCount, color);
        }
    }

    public static void DrawCircle(int centerX, int centerY, float radius, Color color) => Raylib.DrawCircle(centerX, centerY, radius, color);
    public static void DrawCircle(Vector2 center, float radius, Color color) => Raylib.DrawCircleV(center, radius, color);
    public static void DrawCircleLines(int centerX, int centerY, float radius, Color color) => Raylib.DrawCircleLines(centerX, centerY, radius, color);
    public static void DrawCircleSector(Vector2 center, float radius, float startAngle, float endAngle, int segments, Color color) => Raylib.DrawCircleSector(center, radius, startAngle, endAngle, segments, color);
    public static void DrawCircleSectorLines(Vector2 center, float radius, float startAngle, float endAngle, int segments, Color color) => Raylib.DrawCircleSectorLines(center, radius, startAngle, endAngle, segments, color);
    public static void DrawCircleGradient(int centerX, int centerY, float radius, Color color1, Color color2) => Raylib.DrawCircleGradient(centerX, centerY, radius, color1, color2);
    public static void DrawEllipse(int centerX, int centerY, float radiusH, float radiusV, Color color) => Raylib.DrawEllipse(centerX, centerY, radiusH, radiusV, color);
    public static void DrawEllipseLines(int centerX, int centerY, float radiusH, float radiusV, Color color) => Raylib.DrawEllipseLines(centerX, centerY, radiusH, radiusV, color);
    public static void DrawRing(Vector2 center, float innerRadius, float outerRadius, float startAngle, float endAngle, int segments, Color color) => Raylib.DrawRing(center, innerRadius, outerRadius, startAngle, endAngle, segments, color);
    public static void DrawRingLines(Vector2 center, float innerRadius, float outerRadius, float startAngle, float endAngle, int segments, Color color) => Raylib.DrawRingLines(center, innerRadius, outerRadius, startAngle, endAngle, segments, color);
    public static void DrawRectangle(int posX, int posY, int width, int height, Color color) => Raylib.DrawRectangle(posX, posY, width, height, color);
    public static void DrawRectangle(Vector2 pos, Vector2 size, Color color) => Raylib.DrawRectangleV(pos, size, color);
    public static void DrawRectangle(Rectangle rec, Color color) => Raylib.DrawRectangleRec(rec, color);
    public static void DrawRectangle(Rectangle rec, Vector2 origin, float rotation, Color color) => Raylib.DrawRectanglePro(rec, origin, rotation, color);
    public static void DrawRectangleGradient(Rectangle rec, Color col1, Color col2, Color col3, Color col4) => Raylib.DrawRectangleGradientEx(rec, col1, col2, col3, col4);
    public static void DrawRectangleGradientV(int posX, int posY, int width, int height, Color color1, Color color2) => Raylib.DrawRectangleGradientV(posX, posY, width, height, color1, color2);
    public static void DrawRectangleGradientH(int posX, int posY, int width, int height, Color color1, Color color2) => Raylib.DrawRectangleGradientH(posX, posY, width, height, color1, color2);
    public static void DrawRectangleLines(int posX, int posY, int width, int height, Color color) => Raylib.DrawRectangleLines(posX, posY, width, height, color);
    public static void DrawRectangleLines(Rectangle rec, float lineThick, Color color) => Raylib.DrawRectangleLinesEx(rec, lineThick, color);
    public static void DrawRectangleRounded(Rectangle rec, float roundness, int segments, Color color) => Raylib.DrawRectangleRounded(rec, roundness, segments, color);
    public static void DrawRectangleRoundedLines(Rectangle rec, float roundness, int segments, float lineThick, Color color) => Raylib.DrawRectangleRoundedLines(rec, roundness, segments, lineThick, color);
    public static void DrawTriangle(Vector2 v1, Vector2 v2, Vector2 v3, Color color) => Raylib.DrawTriangle(v1, v2, v3, color);
    public static void DrawTriangleLines(Vector2 v1, Vector2 v2, Vector2 v3, Color color) => Raylib.DrawTriangleLines(v1, v2, v3, color);
    
    [Obsolete("Now has a version that does not use unsafe code")]
    public static unsafe void DrawTriangleFan(Vector2* points, int pointCount, Color color) => Raylib.DrawTriangleFan(points, pointCount, color);
    public static void DrawTriangleFan(ref Vector2 points, int pointCount, Color color)
    {
        unsafe
        {
            fixed (Vector2* pointsPtr = &points)
                Raylib.DrawTriangleFan(pointsPtr, pointCount, color);
        }
    }

    [Obsolete("Now has a version that does not use unsafe code")]
    public static unsafe void DrawTriangleStrip(Vector2* points, int pointCount, Color color) => Raylib.DrawTriangleStrip(points, pointCount, color);
    public static void DrawTriangleStrip(ref Vector2 points, int pointCount, Color color)
    {
        unsafe
        {
            fixed (Vector2* pointsPtr = &points)
                Raylib.DrawTriangleStrip(pointsPtr, pointCount, color);
        }
    }

    public static void DrawPoly(Vector2 center, int sides, float radius, float rotation, Color color) => Raylib.DrawPoly(center, sides, radius, rotation, color);
    public static void DrawPolyLines(Vector2 center, int sides, float radius, float rotation, Color color) => Raylib.DrawPolyLines(center, sides, radius, rotation, color);
    public static void DrawPolyLines(Vector2 center, int sides, float radius, float rotation, float lineThick, Color color) => Raylib.DrawPolyLinesEx(center, sides, radius, rotation, lineThick, color);
    public static bool CheckCollisionRecs(Rectangle rec1, Rectangle rec2) => Raylib.CheckCollisionRecs(rec1, rec2);
    public static bool CheckCollisionCircles(Vector2 center1, float radius1, Vector2 center2, float radius2) => Raylib.CheckCollisionCircles(center1, radius1, center2, radius2);
    public static bool CheckCollisionCircleRec(Vector2 center, float radius, Rectangle rec) => Raylib.CheckCollisionCircleRec(center, radius, rec);
    public static bool CheckCollisionPointRec(Vector2 point, Rectangle rec) => Raylib.CheckCollisionPointRec(point, rec);
    public static bool CheckCollisionPointCircle(Vector2 point, Vector2 center, float radius) => Raylib.CheckCollisionPointCircle(point, center, radius);
    public static bool CheckCollisionPointTriangle(Vector2 point, Vector2 p1, Vector2 p2, Vector2 p3) => Raylib.CheckCollisionPointTriangle(point, p1, p2, p3);
    
    [Obsolete("Now has a version that does not use unsafe code")]
    public static unsafe bool CheckCollisionPointPoly(Vector2 point, Vector2* points, int pointCount) => Raylib.CheckCollisionPointPoly(point, points, pointCount);
    public static bool CheckCollisionPointPoly(ref Vector2 point, ref Vector2 points, int pointCount)
    {
        unsafe
        {
            fixed (Vector2* pointsPtr = &points)
                return Raylib.CheckCollisionPointPoly(point, pointsPtr, pointCount);
        }
    }

    [Obsolete("Now has a version that does not use unsafe code")]
    public static unsafe bool CheckCollisionLines(Vector2 startPos1, Vector2 endPos1, Vector2 startPos2, Vector2 endPos2, Vector2* collisionPoint) => Raylib.CheckCollisionLines(startPos1, endPos1, startPos2, endPos2, collisionPoint);
    public static bool CheckCollisionLines(Vector2 startPos1, Vector2 endPos1, Vector2 startPos2, Vector2 endPos2, ref Vector2 collisionPoint)
    {
        unsafe
        {
            fixed (Vector2* collisionPointPtr = &collisionPoint)
                return Raylib.CheckCollisionLines(startPos1, endPos1, startPos2, endPos2, collisionPointPtr);
        }
    }

    public static bool CheckCollisionPointLine(Vector2 point, Vector2 p1, Vector2 p2, int threshold) => Raylib.CheckCollisionPointLine(point, p1, p2, threshold);
    public static Rectangle GetCollisionRec(Rectangle rec1, Rectangle rec2) => Raylib.GetCollisionRec(rec1, rec2);

}