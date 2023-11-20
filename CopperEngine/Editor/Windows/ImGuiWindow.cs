using ImGuiNET;

namespace CopperEngine.Editor.Windows;

[EditorWindow("Style")]
public class ImGuiWindow : BaseEditorWindow
{
    private static bool showAboutWindow;
    private static bool showDemoWindow;
    private static bool showMetricWindow;
    private static bool showDebugLogWindow;
    private static bool showStackToolWindow;
    
    private static bool fontSelectorTabOpen;
    private static bool styleSelectorTabOpen;
    private static bool styleEditorTabOpen;
    private static bool userGuideTabOpen;

    internal override void Update()
    {
        if (showAboutWindow)
            ImGui.ShowAboutWindow(ref showAboutWindow);
        if (showDemoWindow)
            ImGui.ShowDemoWindow(ref showDemoWindow);
        if (showMetricWindow)
            ImGui.ShowMetricsWindow(ref showMetricWindow);
        if (showDebugLogWindow)
            ImGui.ShowDebugLogWindow(ref showDebugLogWindow);
        if (showStackToolWindow)
            ImGui.ShowStackToolWindow(ref showStackToolWindow);

        if (ImGui.BeginMainMenuBar())
        {
            if (ImGui.BeginMenu("Windows"))
            {
                if (ImGui.BeginMenu("ImGui"))
                {
                    ImGui.MenuItem("About", null, ref showAboutWindow);
                    ImGui.MenuItem("Demo", null, ref showDemoWindow);
                    ImGui.MenuItem("Metrics", null, ref showMetricWindow);
                    ImGui.MenuItem("Debug Log", null, ref showDebugLogWindow);
                    ImGui.MenuItem("Stack Tool", null, ref showStackToolWindow);
                    ImGui.EndMenu();
                }

                ImGui.EndMenu();
            }

            ImGui.EndMainMenuBar();
        }
    }

    internal override void Render()
    {
        if (ImGui.BeginTabBar("imgui_window_tab_bar", ImGuiTabBarFlags.Reorderable))
        {
            if (ImGui.BeginTabItem("Fonts Selector", ref fontSelectorTabOpen))
            {
                ImGui.ShowFontSelector("Font Selector");
                ImGui.EndTabItem();
            }
        
            if (ImGui.BeginTabItem("Style Selector", ref styleSelectorTabOpen))
            {
                ImGui.ShowStyleSelector("Style Selector");
                ImGui.EndTabItem();
            }
        
            if (ImGui.BeginTabItem("Style Editor", ref styleEditorTabOpen))
            {
                ImGui.ShowStyleEditor();
                ImGui.EndTabItem();
            }
        
            if (ImGui.BeginTabItem("User Guide", ref userGuideTabOpen))
            {
                ImGui.ShowUserGuide();
                ImGui.EndTabItem();
            }
        
            ImGui.EndTabBar();
        }
    }
}