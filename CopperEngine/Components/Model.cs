using System.Numerics;
using CopperEngine.Utility;
using Raylib_CsLo;
using rlModel = Raylib_CsLo.Model;

namespace CopperEngine.Components;

public class Model : GameComponent
{
    internal rlModel model;

    public Model(string path)
    {
        model = Raylib.LoadModel(path);
    }

    protected internal override void Update()
    {
        Raylib.DrawModel(model, Vector3.Zero, 1, ColorUtil.White);
    }
}