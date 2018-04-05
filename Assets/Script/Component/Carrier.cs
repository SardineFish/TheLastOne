using UnityEngine;
using System.Collections;

public class Carrier : MonoBehaviour
{
    public CarriableObject Carrying;
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (!Carrying)
            Carrying = GetComponentInChildren<CarriableObject>();
    }

    public void Release()
    {
        Carrying?.Detach();
    }
}
