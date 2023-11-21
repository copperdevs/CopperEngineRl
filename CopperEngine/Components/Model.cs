using Raylib_CsLo;
using rlModel = Raylib_CsLo.Model;

namespace CopperEngine.Components;

public class Model : GameComponent
{
    internal rlModel model;

    public Model(string path)
    {
        this.model = Raylib.LoadModel(path);
    }
}