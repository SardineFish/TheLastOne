using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityBehavior<TEntity> : EventBehaviour where TEntity: Entity
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

    protected void UseEventListener()
    {
        if (Entity.GetComponent<EventBus>())
            Bind(Entity.GetComponent<EventBus>());
    }
}
