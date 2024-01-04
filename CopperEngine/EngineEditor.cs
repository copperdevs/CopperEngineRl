﻿using System.Numerics;
using CopperEngine.Editor;
using CopperEngine.Editor.DearImGui;
using CopperEngine.Project;
using ImGuiNET;
using Raylib_cs;

namespace CopperEngine;

internal static class EngineEditor
{
    private static bool initialized;

    internal static bool EditorWindowFocused;
    internal static bool GameWindowFocused;

    private static readonly List<LoadedEditorWindow> EditorWindows = new();

    internal static void Initialize()
    {
        if (initialized)
            return;
        initialized = true;

        if (Engine.State is not Engine.EngineState.Editor)
            return;

        // ui
        rlImGui.Setup();
        LoadConfig();
        LoadStyle();

        // windows
        LoadWindows();
    }

    internal static void Render()
    {
        if (Raylib.IsKeyPressed(KeyboardKey.KEY_F1))
        {
            Engine.State = Engine.State switch
            {
                Engine.EngineState.Game => Engine.EngineState.Editor,
                Engine.EngineState.Editor => Engine.EngineState.Game,
                _ => Engine.EngineState.Editor
            };
        }

        if (Engine.State is not Engine.EngineState.Editor)
            return;

        rlImGui.Begin();
        ImGui.DockSpaceOverViewport();

        if (ImGui.BeginMainMenuBar())
        {
            if (ImGui.BeginMenu("File"))
            {
                ImGui.MenuItem("New Project", null, ref ProjectManager.CreateProjectPopupOpen);
                ImGui.MenuItem("Open Project", null, ref ProjectManager.LoadProjectPopupOpen);
                ImGui.EndMenu();
            }

            if (ImGui.BeginMenu("Windows"))
            {
                EditorWindows.ForEach(window => { ImGui.MenuItem(window.WindowName, null, ref window.IsOpen); });
                ImGui.EndMenu();
            }

            ImGui.EndMainMenuBar();
        }

        EditorWindows.ForEach(window => window.RenderWindow());
        
        ProjectManager.LoadProjectPopup();
        ProjectManager.CreateProjectPopup();

        rlImGui.End();
    }

    internal static void Stop()
    {
        EditorWindows.ForEach(window => window.StopWindow());
    }

    private static void LoadConfig()
    {
        var io = ImGui.GetIO();
        io.ConfigFlags |= ImGuiConfigFlags.DockingEnable;
        io.ConfigFlags |= ImGuiConfigFlags.ViewportsEnable;
        io.ConfigFlags |= ImGuiConfigFlags.NavEnableKeyboard;
        io.ConfigFlags |= ImGuiConfigFlags.NavEnableGamepad;
        io.ConfigWindowsMoveFromTitleBarOnly = true;
        io.ConfigWindowsResizeFromEdges = true;
        io.ConfigViewportsNoTaskBarIcon = true;

        ImGui.GetStyle().WindowRounding = 5;
        ImGui.GetStyle().ChildRounding = 5;
        ImGui.GetStyle().FrameRounding = 5;
        ImGui.GetStyle().PopupRounding = 5;
        ImGui.GetStyle().ScrollbarRounding = 5;
        ImGui.GetStyle().GrabRounding = 5;
        ImGui.GetStyle().TabRounding = 5;

        ImGui.GetStyle().TabBorderSize = 1;

        ImGui.GetStyle().WindowTitleAlign = new Vector2(0.5f);
        ImGui.GetStyle().SeparatorTextAlign = new Vector2(0.5f);
        ImGui.GetStyle().SeparatorTextPadding = new Vector2(20, 5);
    }

    private static void LoadStyle()
    {
        var colors = ImGui.GetStyle().Colors;
        colors[(int)ImGuiCol.WindowBg] = new Vector4(0.1f, 0.105f, 0.11f, 1.0f);

        // Headers
        colors[(int)ImGuiCol.Header] = new Vector4(0.2f, 0.205f, 0.21f, 1.0f);
        colors[(int)ImGuiCol.HeaderHovered] = new Vector4(0.3f, 0.305f, 0.31f, 1.0f);
        colors[(int)ImGuiCol.HeaderActive] = new Vector4(0.15f, 0.1505f, 0.151f, 1.0f);

        // Buttons
        colors[(int)ImGuiCol.Button] = new Vector4(0.2f, 0.205f, 0.21f, 1.0f);
        colors[(int)ImGuiCol.ButtonHovered] = new Vector4(0.3f, 0.305f, 0.31f, 1.0f);
        colors[(int)ImGuiCol.ButtonActive] = new Vector4(0.15f, 0.1505f, 0.151f, 1.0f);

        // Frame BG
        colors[(int)ImGuiCol.FrameBg] = new Vector4(0.2f, 0.205f, 0.21f, 1.0f);
        colors[(int)ImGuiCol.FrameBgHovered] = new Vector4(0.3f, 0.305f, 0.31f, 1.0f);
        colors[(int)ImGuiCol.FrameBgActive] = new Vector4(0.15f, 0.1505f, 0.151f, 1.0f);

        // Tabs
        colors[(int)ImGuiCol.Tab] = new Vector4(0.15f, 0.1505f, 0.151f, 1.0f);
        colors[(int)ImGuiCol.TabHovered] = new Vector4(0.38f, 0.3805f, 0.381f, 1.0f);
        colors[(int)ImGuiCol.TabActive] = new Vector4(0.28f, 0.2805f, 0.281f, 1.0f);
        colors[(int)ImGuiCol.TabUnfocused] = new Vector4(0.15f, 0.1505f, 0.151f, 1.0f);
        colors[(int)ImGuiCol.TabUnfocusedActive] = new Vector4(0.2f, 0.205f, 0.21f, 1.0f);

        // Title
        colors[(int)ImGuiCol.TitleBg] = new Vector4(0.15f, 0.1505f, 0.151f, 1.0f);
        colors[(int)ImGuiCol.TitleBgActive] = new Vector4(0.15f, 0.1505f, 0.151f, 1.0f);
        colors[(int)ImGuiCol.TitleBgCollapsed] = new Vector4(0.15f, 0.1505f, 0.151f, 1.0f);
    }

    private static void LoadWindows()
    {
        var assemblies = AppDomain.CurrentDomain.GetAssemblies();
        var baseType = typeof(BaseEditorWindow);

        var derivedTypes = assemblies
            .SelectMany(assembly => assembly.GetTypes())
            .Where(type => baseType.IsAssignableFrom(type) && type != baseType);

        foreach (var type in derivedTypes)
        {
            EditorWindows.Add(new LoadedEditorWindow((Activator.CreateInstance(type) as BaseEditorWindow)!));
        }
    }

    private class LoadedEditorWindow
    {
        public BaseEditorWindow Window;
        public readonly EditorWindowAttribute? Attribute;

        public bool IsOpen = true;
        public ImGuiWindowFlags WindowFlags;
        public string WindowName;

        public LoadedEditorWindow(BaseEditorWindow window)
        {
            Window = window;

            Attribute = (EditorWindowAttribute)System.Attribute.GetCustomAttribute(window.GetType(),
                typeof(EditorWindowAttribute))!;

            if (Attribute is null)
            {
                WindowFlags = ImGuiWindowFlags.None;
                WindowName = window.GetType().Name;
                Window.Start();
                return;
            }

            WindowName = Attribute.WindowName;
            WindowFlags = Attribute.WindowFlags;
            IsOpen = Attribute.StartingState;
            Window.Start();
        }

        public void RenderWindow()
        {
            Window.Update();

            if (!IsOpen)
                return;


            ImGui.PushStyleVar(ImGuiStyleVar.WindowPadding, new Vector2(0, 0));
            ImGui.SetNextWindowSizeConstraints(new Vector2(128, 128),
                new Vector2(Raylib.GetScreenWidth(), Raylib.GetScreenHeight()));

            Window.PreRender();
            if (ImGui.Begin(WindowName, ref IsOpen, WindowFlags))
            {
                Window.Render();
                ImGui.End();
            }

            Window.PostRender();

            ImGui.PopStyleVar();
        }

        public void StopWindow()
        {
            Window.Stop();
        }
    }
}