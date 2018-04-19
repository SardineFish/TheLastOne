using UnityEngine;

public abstract class GameUI:ScriptableObject
{
    public abstract GameObject RenderUI();
    public virtual void Update() { }

}