using UnityEngine;
using System.Collections;

[ExecuteInEditMode]
public class Wall : MonoBehaviour
{
    public Vector3 Normal;
    public Vector3 Bottom;
    public float Length;
    public float Width;
    public float Height;
    // Use this for initialization
    void Start()
    {
        var collider = GetComponent<BoxCollider>();
        Bottom = Vector3.down * (collider.center.y - collider.size.y / 2);
        Normal = Vector3.right;
        Length = collider.size.z;
        Width = collider.size.x;
        Height = collider.size.y;
    }

    // Update is called once per frame
    void Update()
    {

    }
}
