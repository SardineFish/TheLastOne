using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

[ExecuteInEditMode]
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
            transform.localRotation = Quaternion.Inverse(CarryRotation);
            transform.localPosition = -(Quaternion.Inverse(CarryRotation) * CarryPostion);
        }
    }

    public void AttachTo(Carrier carrier)
    {
        gameObject.SetLayerRecursive(carrier.gameObject.layer);
        SceneManager.MoveGameObjectToScene(gameObject, carrier.gameObject.scene);
        gameObject.layer = carrier.gameObject.layer;
        carrier.Carrying?.Detach();
        transform.SetParent(carrier.transform);
    }

    public void Detach()
    {
        transform.SetParent(null);
        gameObject.AddComponent<ObjectReleaseEffect>();
    }
}
