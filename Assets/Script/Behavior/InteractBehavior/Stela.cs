﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

[RequireComponent(typeof(InteractiveObject))]
public class Stela : InteractBehavior
{
    public override void OnInteract(Entity trigger)
    {
        Debug.Log(trigger.name + " interacted.");
    }
}