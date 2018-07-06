using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class StelaInteract : InteractBehavior
{
    public StelaData StelaData = null;
    public override void Start()
    {
        base.Start();
        StelaData = StelaSystem.Instance.GenerateStelaData();
    }
    public override void OnInteract(Entity trigger)
    {
        StelaData = StelaSystem.Instance.GenerateStelaData();
        StelaUIManager.Instance.StelaData = StelaData;
        StelaUIManager.Instance.Display();
    }
}