using CopperEngine.Info;
using ImGuiNET;

namespace CopperEngine;

public static class EngineProjectManager
{
    internal static bool CreateProjectPopupOpen = false;
    internal static string CreateProjectPopupInputPath = "";
    internal static string CreateProjectPopupInputName = "New Project";

    public static void SaveCurrentProject()
    {
    }

    public static void LoadProject(string path)
    {
    }

    public static void CreateProject(string path, string name)
    {
        if (Directory.Exists($"{path}/{name}"))
            return;
        
        Directory.CreateDirectory($"{path}/{name}");
        Directory.CreateDirectory($"{path}/{name}/Assets");
        Directory.CreateDirectory($"{path}/{name}/ProjectSettings");
        
        File.WriteAllText($"{path}/{name}/name.txt", name);
    }

    public static void CreateProjectPopup()
    {
        if (CreateProjectPopupOpen)
        {
            ImGui.OpenPopup($"EngineProjectManager_NewProjectPopup");
            if (ImGui.BeginPopupModal("EngineProjectManager_NewProjectPopup"))
            {
                ImGui.Text($"Project Location");
                ImGui.InputText($"Project Location", ref CreateProjectPopupInputPath, uint.MaxValue-1);
                ImGui.InputText($"Project Name", ref CreateProjectPopupInputName, uint.MaxValue-1);

                if (ImGui.Button("Create Project"))
                {
                    CreateProject(CreateProjectPopupInputPath, CreateProjectPopupInputName);

                    CreateProjectPopupInputPath = "";
                    CreateProjectPopupInputName = "New Project";

                    CreateProjectPopupOpen = false;
                }
                
                ImGui.EndPopup();
            }
        }
    }
}