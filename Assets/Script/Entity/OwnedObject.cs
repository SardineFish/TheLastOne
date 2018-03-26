using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class OwnedObject : MonoBehaviour
{
    private Entity owner = null;
    public Entity Owner
    {
        get
        {
            if(!owner)
            {
                var trans = transform;
                do
                {
                    owner = trans.GetComponent<Entity>();
                    trans = trans.parent;
                }
                while (!owner && trans);
            }
            return owner;
        }
    }
}