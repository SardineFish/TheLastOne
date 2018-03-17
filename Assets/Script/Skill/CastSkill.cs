using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class CastSkill : AnimationSkill
{
    public float Range;
    public GameObject ObjectPrefab;

    public override bool Activate()
    {
        return Activate(Entity.transform.position + Entity.transform.forward * Range);
    }

    public override bool Activate(Entity target)
    {
        return Activate(target.transform.position);
    }

    public override bool Activate(Vector3 target)
    {
        
    }
}
