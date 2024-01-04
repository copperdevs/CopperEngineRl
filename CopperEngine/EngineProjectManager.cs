using CopperEngine.Info;
using ImGuiNET;

namespace CopperEngine;

public static class EngineProjectManager
{
    internal static string CurrentProjectPath = null!;
    internal static bool CreateProjectPopupOpen = false;
    internal static string CreateProjectPopupInputName = "New Project";

    public static void SaveCurrentProject()
    {
        
    }

    public static void LoadProject(string path)
    {
        CurrentProjectPath = path;
    }

    public static void CreateProject(string name)
    {
        if (Directory.Exists($"{Directory.GetCurrentDirectory()}/Projects/{name}"))
            return;
        
        Directory.CreateDirectory($"{Directory.GetCurrentDirectory()}/Projects/{name}");
        Directory.CreateDirectory($"{Directory.GetCurrentDirectory()}/Projects/{name}/Assets");
        Directory.CreateDirectory($"{Directory.GetCurrentDirectory()}/Projects/{name}/ProjectSettings");
        
        File.WriteAllText($"{Directory.GetCurrentDirectory()}/Projects/{name}/name.txt", name);
        
         CurrentProjectPath = $"{Directory.GetCurrentDirectory()}/Projects/{name}";
    }

    public static void CreateProjectPopup()
    {
        if (CreateProjectPopupOpen)
        {
            ImGui.OpenPopup($"EngineProjectManager_NewProjectPopup");
            if (ImGui.BeginPopupModal("EngineProjectManager_NewProjectPopup"))
            {
                ImGui.Text($"Project Location");;
                ImGui.InputText($"Project Name", ref CreateProjectPopupInputName, 128);

                if (ImGui.Button("Create Project"))
                {
                    CreateProject(CreateProjectPopupInputName);

                    CreateProjectPopupInputName = "New Project";

                    CreateProjectPopupOpen = false;
                }
                
                ImGui.EndPopup();
            }
        }
    }
}