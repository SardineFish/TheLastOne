﻿using UnityEngine;
using System.Collections;

public class CarriableObject : MonoBehaviour
{
    public Vector3 CarryPostion;
    public Quaternion CarryRotation;
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if(transform.parent)
        {
            transform.localRotation = CarryRotation;
            transform.localPosition = -CarryPostion;
        }
    }

    public void AttachTo(Carrier carrier)
    {
        carrier.Carrying?.Detach();
        transform.SetParent(carrier.transform);
    }

    public void Detach()
    {
        transform.SetParent(null);
        gameObject.AddComponent<ObjectReleaseEffect>();
    }
}