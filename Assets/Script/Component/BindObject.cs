using UnityEngine;
using System.Collections;

[ExecuteInEditMode]
public class BindObject : MonoBehaviour
{
    public GameObject BindTo;
    public bool BindPosition = false;
    public bool BindRotation = false;
    //public bool BindScale = true;
    public Quaternion targetOriginRotation = Quaternion.identity;
    public Quaternion relativeRotation = Quaternion.identity;
    public Vector3 relativePosition = Vector3.zero;
    float lastUpdateTime = 0;
    //public Vector3 relativeScale;

    // Use this for initialization
    void Start()
    {

    }

    //[ExecuteInEditMode]
    void Update()
    {
        if (Time.time == lastUpdateTime)
            return;
        lastUpdateTime = Time.time;
        if (BindTo)
        {
            if (BindPosition)
                transform.position = BindTo.transform.position + (BindTo.transform.rotation * relativePosition);
            if (BindRotation)
            {
                transform.rotation = BindTo.transform.rotation * relativeRotation;
            }
        }
        UpdateChildren();
    }

    private void FixedUpdate()
    {
        /*return;
        if (BindTo)
        {
            if (BindPosition)
                transform.position = BindTo.transform.position + (BindTo.transform.rotation * relativePosition);
            if (BindRotation)
            {
                transform.rotation = BindTo.transform.rotation * relativeRotation;
            }
        }*/
    }
    

    void UpdateChildren()
    {
        var children = GetComponentsInChildren<BindObject>(false);
        foreach(var child in children)
        {
            child.Update();
        }
    }
}
