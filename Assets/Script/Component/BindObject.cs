using UnityEngine;
using System.Collections;

[ExecuteInEditMode]
public class BindObject : MonoBehaviour
{
    public GameObject BindTo;
    public bool BindPosition = true;
    public bool BindRotation = true;
    //public bool BindScale = true;
    public Quaternion relativeRotation = Quaternion.identity;
    public Vector3 relativePosition = Vector3.zero;
    //public Vector3 relativeScale;

    // Use this for initialization
    void Start()
    {

    }

    [ExecuteInEditMode]
    void Update()
    {
        if (BindTo)
        {
            if (BindPosition)
                transform.position = BindTo.transform.position + relativePosition;
            if (BindRotation)
                transform.rotation = BindTo.transform.rotation * relativeRotation;
        }
    }

    private void FixedUpdate()
    {
        if (BindTo)
        {
            if (BindPosition)
                transform.position = BindTo.transform.position + relativePosition;
            if (BindRotation)
                transform.rotation = BindTo.transform.rotation * relativeRotation;
        }
        
    }
}
