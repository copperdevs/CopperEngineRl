using System.Numerics;
using CopperEngine.Logs;
using CopperEngine.Utility;
using Raylib_CsLo;
using rlModel = Raylib_CsLo.Model;

namespace CopperEngine.Components;

public class Model : GameComponent
{
    private rlModel model;
    private readonly string modelPath;

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
        ModelUtil.DrawModel(model, Vector3.Zero, 1, ColorUtil.White);
    }

    protected internal override void Sleep()
    {
        ModelUtil.Unload(model); 
    }
}