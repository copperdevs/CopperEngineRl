using System.Numerics;
using CopperEngine.Logs;
using CopperEngine.Utility;
using CopperEngine.Utils;
using ImGuiNET;
using Raylib_CsLo;
using Color = CopperEngine.Data.Color;
using rlModel = Raylib_CsLo.Model;

namespace CopperEngine.Components;

public class Model : GameComponent
{
    private rlModel model;
    private readonly string modelPath;

    public Color ModelTint = Color.White;
    
    public Model(string path)
    {
        modelPath = path;
    }

    protected internal override void Awake()
    {
        model = ModelUtil.Load(modelPath);

        unsafe
        {
            model.materials[0] = MaterialUtil.LoadDefault();
        }
    }

    protected internal override void Update()
    {
        ModelUtil.DrawModel(model, Vector3.Zero, 1, ModelTint);
    }

    protected internal override void EditorUpdate()
    {
        ImGui.LabelText("Model Path", modelPath);

        if (ImGui.CollapsingHeader("rlModel Info"))
        {
            ImGui.Indent();
            EditorUtil.DragMatrix4X4("Model Transform", model.transform);
            ImGui.Unindent();
            var meshCount = model.meshCount;
            ImGui.DragInt("Mesh Count", ref meshCount);
            var materialCount = model.materialCount;
            ImGui.DragInt("Material Count", ref materialCount);
        }
    }

    protected internal override void Sleep()
    {
        ModelUtil.Unload(model); 
    }
}