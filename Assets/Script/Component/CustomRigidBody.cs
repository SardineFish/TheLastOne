using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class CustomRigidBody:MonoBehaviour
{
    public float mass = 1;
    public bool UseGravity = false;
    public Vector3 acceleration { get; set; }
    public Vector3 velocity
    {
        get
        {
            return (transform.position - lastPos) / Time.fixedDeltaTime;
        }
        set
        {
            momentum = value * mass;
        }
    }
    public Vector3 momentum = Vector3.zero;
    public float drag = 0;
    
    private Vector3 lastPos = Vector3.zero;

    private void Start()
    {
        lastPos = transform.position;
    }

    public void FixedUpdate()
    {
        momentum += Physics.gravity * mass * Time.fixedDeltaTime;
        momentum += acceleration * Time.fixedDeltaTime * mass;
        var drag = this.drag * mass;
        drag = Mathf.Clamp(drag, 0, momentum.magnitude / mass);
        //Debug.Log(drag);
        this.AddForce(-velocity.normalized * drag, ForceMode.VelocityChange);

        lastPos = transform.position;
        //transform.Translate(momentum / mass * Time.fixedDeltaTime, Space.World);
        GetComponent<Rigidbody>().MovePosition(transform.position + momentum / mass * Time.fixedDeltaTime);
        
    }

    public void AddForce(Vector3 f,ForceMode forceMode)
    {
        switch (forceMode)
        {
            case ForceMode.Acceleration:
                acceleration += f;
                break;
            case ForceMode.Force:
                acceleration += f / mass;
                break;
            case ForceMode.Impulse:
                momentum += f;
                break;
            case ForceMode.VelocityChange:
                momentum += f * mass;
                break;
        }
    }


}