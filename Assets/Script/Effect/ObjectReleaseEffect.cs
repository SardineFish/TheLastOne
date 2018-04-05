using UnityEngine;
using System.Collections;

public class ObjectReleaseEffect : MonoBehaviour
{
    public float TotalTime = 1;
    float startTime;
    bool start = false;
    // Use this for initialization
    void Start()
    {

    }

    private void OnEnable()
    {
        startTime = Time.time;
        start = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time - startTime > TotalTime)
            Destroy(gameObject);
    }
}
