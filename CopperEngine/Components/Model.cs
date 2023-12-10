using System.Numerics;
using CopperEngine.Utility;
using ImGuiNET;
using Color = CopperEngine.Data.Color;
using rlModel = Raylib_cs.Model;

namespace CopperEngine.Components;

public sealed class Model : Component, IDisposable
{
    private rlModel model;
    private bool modelLoaded;

    private readonly string modelPath;

    public Color ModelTint = Color.White;

    public Model(string path)
    {
        modelPath = path;
        modelLoaded = false;
    }

    protected internal override void Awake()
    {
        if (modelLoaded)
            return;

        model = ModelUtil.Load(modelPath);
        modelLoaded = true;

        unsafe
        {
            model.Materials[0] = MaterialUtil.LoadDefault();
        }
    }

    protected internal override void Update()
    {
        if (modelLoaded)
            ModelUtil.DrawModel(model, Vector3.Zero, 1, ModelTint);
    }

    protected internal override void EditorUpdate()
    {
        ImGui.LabelText("Model Path", modelPath);

        if (ImGui.CollapsingHeader("rlModel Info"))
        {
            ImGui.Indent();
            EditorUtil.DragMatrix4X4("Model Transform", model.Transform);
            ImGui.Unindent();
            var meshCount = model.MeshCount;
            ImGui.DragInt("Mesh Count", ref meshCount);
            var materialCount = model.MaterialCount;
            ImGui.DragInt("Material Count", ref materialCount);
        }
    }

    protected internal override void Sleep()
    {
        if (!modelLoaded)
            return;

        ModelUtil.Unload(model);
        modelLoaded = false;
    }

    public void Dispose()
    {
        if (modelLoaded)
            ModelUtil.Unload(model);
    }
}