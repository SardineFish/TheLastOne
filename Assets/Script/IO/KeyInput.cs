using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyInput : InputDevice {
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        var moveInput = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        InputManager.SetMove(moveInput);
        if (Input.GetKeyDown(KeyCode.Alpha1))
            InputManager.Action1Pressed();
        if (Input.GetKeyUp(KeyCode.Alpha1))
            InputManager.Action1Released();
        if (Input.GetKeyDown(KeyCode.Alpha2))
            InputManager.Action2Pressed();
        if (Input.GetKeyUp(KeyCode.Alpha2))
            InputManager.Action2Released();

    }
}
