using UnityEngine;

public abstract class GameUI:MonoBehaviour
{
    public abstract GameObject RenderUI();
    public virtual void Update() { }

}