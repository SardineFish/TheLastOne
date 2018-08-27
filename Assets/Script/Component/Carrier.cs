using UnityEngine;
using System.Collections;

[ExecuteInEditMode]
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
        if (Carrying && ! Carrying.Carrier)
        {
            var carryObj = Carrying;
            Carrying = null;
            carryObj.AttachTo(this);
        }
    }

    void Attach()
    {

    }

    public void Release()
    {
        if (Carrying)
            Carrying.Detach();
    }
}
