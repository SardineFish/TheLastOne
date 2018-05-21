using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class StelaInteract : InteractBehavior
{
    public override void OnInteract(Entity trigger)
    {
        Debug.Log(trigger.name + " interacted.");
    }
}