using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using System;

[ExecuteInEditMode]
public class CarriableObject : MonoBehaviour
{
    [NonSerialized]
    public Carrier Carrier = null;
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
            transform.localRotation = Quaternion.Inverse(CarryRotation);
            transform.localPosition = -(Quaternion.Inverse(CarryRotation) * CarryPostion);
        }
    }

    public void AttachTo(Carrier carrier)
    {
        gameObject.SetLayerRecursive(carrier.gameObject.layer);
        transform.SetParent(null);
        SceneManager.MoveGameObjectToScene(gameObject, carrier.gameObject.scene);
        gameObject.layer = carrier.gameObject.layer;
        carrier.Carrying?.Detach();
        transform.SetParent(carrier.transform);
        carrier.Carrying = this;
        this.Carrier = carrier;
    }

    public void Detach()
    {
        transform.SetParent(null);
        gameObject.AddComponent<ObjectReleaseEffect>();
    }
}
