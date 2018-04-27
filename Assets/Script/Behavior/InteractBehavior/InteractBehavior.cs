using UnityEngine;
using System.Collections;

[RequireComponent(typeof(InteractiveObject))]
public abstract class InteractBehavior : MonoBehaviour,IEventBehaviour
{
    public EventListener[] EventListeners { get; set; }

    public EventBus EventTarget { get; set; }

    public virtual void Start()
    {
        if (GetComponent<InteractiveObject>())
            this.Bind(GetComponent<InteractiveObject>());
    }

    [EventListener(InteractiveObject.EVENT_ON_INTERACT)]
    public abstract void OnInteract(Entity trigger);

}
