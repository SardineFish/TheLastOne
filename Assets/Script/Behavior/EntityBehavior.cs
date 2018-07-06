using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityBehavior<TEntity> : MonoBehaviour,IEventBehaviour where TEntity: Entity
{
    public TEntity Entity
    {
        get
        {
            var trans = transform;
            Next:
            if (!trans)
                return null;
            if (trans.GetComponent<TEntity>())
                return trans.GetComponent<TEntity>();
            trans = trans.parent;
            goto Next;
        }
    }

    public ReflectEventListener[] EventListeners { get; set; }

    public EventBus EventTarget { get; set; }
}
