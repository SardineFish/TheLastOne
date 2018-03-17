using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelfDestruct : EntityBehavior<Entity>
{
    public float Time = 1;

    float startTime;

    private void OnEnable()
    {
        startTime = UnityEngine.Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        if (UnityEngine.Time.time - startTime > this.Time)
            GameObject.Destroy(gameObject);
    }
}