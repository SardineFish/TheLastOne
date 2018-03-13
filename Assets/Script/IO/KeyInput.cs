using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyInput : MonoBehaviour {
    public InputManager InputManager;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        var moveInput = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        InputManager.Move(moveInput);

    }
}
