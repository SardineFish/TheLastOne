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
    public float GroundDrag = 0;
    public float AirDrag = 0;
    public bool OnGround = false;
    int ground = 0;

    private Vector3 lastPos = Vector3.zero;

    private void Start()
    {
        lastPos = transform.position;
    }
    

    public void FixedUpdate()
    {
        if(OnGround)
        {
            momentum.y = 0;
        }
        else if (UseGravity)
            momentum += Physics.gravity * mass * Time.fixedDeltaTime;

        momentum += acceleration * Time.fixedDeltaTime * mass;

        var drag = 0f;
        if(OnGround)
        {
            drag = GroundDrag * mass;

        }
        else
        {
            drag = AirDrag * mass;
        }
        drag = Mathf.Clamp(drag, 0, momentum.magnitude / mass);
        //Debug.Log(drag);
        this.AddForce(-velocity.normalized * drag, ForceMode.VelocityChange);

        lastPos = transform.position;
        //transform.Translate(momentum / mass * Time.fixedDeltaTime, Space.World);
        if (momentum.magnitude < 0.001f)
            momentum = Vector3.zero;
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

    private void OnCollisionEnter(Collision collision)
    {
        /*if (collision.gameObject.layer == 10)
            OnGround = true;
        Debug.Log("Enter");
        return;*/
        ground += 1;
        OnGround = ground > 0;
    }

    private void OnCollisionExit(Collision collision)
    {
        /*if (collision.gameObject.layer == 10)
            OnGround = false;
        Debug.Log("Exit");
        return;*/
        ground -= 1;
        OnGround = ground > 0;
    }
}