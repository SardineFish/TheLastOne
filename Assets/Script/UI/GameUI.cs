using UnityEngine;

public abstract class HoverUIComponent:MonoBehaviour
{
    public abstract GameObject RenderUI();
    public virtual void Update() { }

}