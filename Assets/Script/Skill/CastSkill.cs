using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class CastSkill : AnimationSkill
{
    public float Range;
    public GameObject ObjectPrefab;
    public Vector3 InstantiatePosition;

    public override bool Activate(params object[] additionalData)
    {
        return Activate(Entity.transform.position + Entity.transform.forward * Range);
    }

    public override bool Activate(Entity target, params object[] additionalData)
    {
        return Activate(target.transform.position);
    }

    public override bool Activate(Vector3 target, params object[] additionalData)
    {
        if (base.Activate())
        {
            GameObject.Instantiate(ObjectPrefab, InstantiatePosition, Quaternion.identity, transform.root);
            return true;
        }
        else
            return false;
    }
}
