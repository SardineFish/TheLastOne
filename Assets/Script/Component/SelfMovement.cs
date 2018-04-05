using UnityEngine;
using System.Collections;

public class SelfMovement : MonoBehaviour
{
    public float Speed;
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(transform.forward * Speed*Time.deltaTime, Space.World);
    }
}
