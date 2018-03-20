using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseInput : InputDevice {
    public float MaxDistance = 1000;
    Camera camera;
    private void Start()
    {
        camera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
    }
    void Update()
    {
        var ray = camera.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, MaxDistance))
        {
            InputManager.SetTarget(hit.point);
        }
    }


}
