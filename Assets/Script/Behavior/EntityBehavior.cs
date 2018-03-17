using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityBehavior<TEntity> : MonoBehaviour where TEntity: Entity
{
    public TEntity Entity
    {
        get
        {
            var trans = transform;
            Next:
            if (trans.GetComponent<TEntity>())
                return trans.GetComponent<TEntity>();
            trans = trans.parent;
            goto Next;
        }
    }
}
