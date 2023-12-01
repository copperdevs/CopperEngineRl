﻿using CopperEngine.Data;
using CopperEngine.Scenes;

namespace CopperEngine.Components;

public class GameComponent
{
    protected internal GameObject Parent;
    protected internal Scene ParentScene => Parent.ParentScene;
    protected internal Transform Transform
    {
        get => Parent.Transform;
        set => Parent.Transform = value;
    }
    
    protected internal virtual void Start() { }
    protected internal virtual void Awake() { }
    protected internal virtual void PreUpdate() { }
    protected internal virtual void Update() { }
    protected internal virtual void PostUpdate() { }
    protected internal virtual void EditorUpdate() { }
    protected internal virtual void Sleep() { }
    protected internal virtual void Stop() { }

    public T GetFirstComponentOfType<T>() where T : GameComponent => Parent.GetFirstComponentOfType<T>()!;
}