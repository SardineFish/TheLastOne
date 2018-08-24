using UnityEngine;
using System.Collections;

public class DebugInput : MonoBehaviour
{

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            Time.timeScale *= 0.5f;
        }
    }
}
