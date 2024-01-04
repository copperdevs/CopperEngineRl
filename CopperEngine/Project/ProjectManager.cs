using ImGuiNET;

namespace CopperEngine.Project;

public static class ProjectManager
{
    internal static string CurrentProjectPath = null!;
    internal static bool CreateProjectPopupOpen = false;
    internal static string CreateProjectPopupInputName = "New Project";
    
    internal static bool LoadProjectPopupOpen = false;

    public static void SaveCurrentProject()
    {
        
    }

    public static void LoadProject(string path)
    {
        CurrentProjectPath = path;
    }

    public static void LoadProjectPopup()
    {
        if (LoadProjectPopupOpen)
        {
            ImGui.OpenPopup($"EngineProjectManager_LoadProjectPopup");
            if (ImGui.BeginPopupModal("EngineProjectManager_LoadProjectPopup"))
            {
                var projects = Directory.GetDirectories("Projects");

                foreach (var project in projects)
                {
                    var projectName = project[(project.IndexOf(@"\", StringComparison.Ordinal) + 1)..];
                    if (ImGui.Button(projectName))
                    {
                        LoadProject(projectName);
                        LoadProjectPopupOpen = false;
                    }
                }
                
                ImGui.EndPopup();
            }
        }
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