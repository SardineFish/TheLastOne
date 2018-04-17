using UnityEngine;
using System.Collections;

public abstract class InteractBehavior : ScriptableObject
{
    public abstract void Interact(Entity trigger);

}
