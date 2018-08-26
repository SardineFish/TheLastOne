using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyboardMovement : MovementInput
{
    public const int IdxVerticalPositive = 0;
    public const int IdxVerticalNegative = 1;
    public const int IdxHorizontalPositive = 2;
    public const int IdxHorizontalNegative = 3;

    public Vector2 InputDirection;

    public static Vector2[] Directions = new Vector2[] { Vector2.up, Vector2.down, Vector2.left, Vector2.right };

    public static List<Vector2> HorizontalInputs = new List<Vector2>(new Vector2[] { Vector2.zero });
    public static List<Vector2> VerticalInputs = new List<Vector2>(new Vector2[] { Vector2.zero });

    public KeyCode VerticalPositive
    {
        get
        {
            return InputKeys[IdxVerticalPositive];
        }
        set
        {
            InputKeys[IdxVerticalPositive] = value;
        }
    }
    public KeyCode VerticalNegative
    {
        get
        {
            return InputKeys[IdxVerticalNegative];
        }
        set
        {
            InputKeys[IdxVerticalNegative] = value;
        }
    }

    public KeyCode HorizontalPositive
    {
        get
        {
            return InputKeys[IdxHorizontalPositive];
        }
        set
        {
            InputKeys[IdxHorizontalPositive] = value;
        }
    }
    
    public KeyCode HorizontalNegative
    {
        get
        {
            return InputKeys[IdxHorizontalNegative];
        }
        set
        {
            InputKeys[IdxHorizontalNegative] = value;
        }
    }

    public override Vector2 GetMovement()
    {
        return InputDirection;
    }

    public override void Interrupt()
    {

    }

    // Use this for initialization
    void Start () {
	}
	
	// Update is called once per frame
	void Update ()
    {
     /*   var x = Input.GetAxisRaw("Horizontal");
        var y = Input.GetAxisRaw("Vertical");

        Debug.Log(x.ToString() + "," + y.ToString() + " " + new Vector2(x, y).magnitude);*/
        if (InputKeys == null)
            InputKeys = new KeyCode[4];
        if (InputKeys.Length != 4)
            System.Array.Resize(ref InputKeys, 4);

        for (var i = 0; i < Directions.Length; i++)
        {
            if(i/2==0)
            {
                if (InputManager.Instance.GetKeyDown(InputKeys[i]))
                    VerticalInputs.Add(Directions[i]);
                else if (InputManager.Instance.GetKeyUp(InputKeys[i]))
                    VerticalInputs.Remove(Directions[i]);
            }
            else
            {
                if (InputManager.Instance.GetKeyDown(InputKeys[i]))
                    HorizontalInputs.Add(Directions[i]);
                else if (InputManager.Instance.GetKeyUp(InputKeys[i]))
                    HorizontalInputs.Remove(Directions[i]);
            }
        }

        InputDirection = HorizontalInputs[HorizontalInputs.Count - 1] + VerticalInputs[VerticalInputs.Count - 1];


        /*
        if (InputManager.Current.GetKeyDown(HorizontalPositive))
            Horizontal += 1;
        if (InputManager.Current.GetKeyDown(HorizontalNegative))
            Horizontal -= 1;
        if (InputManager.Current.GetKeyDown(VerticalPositive))
            Vertical += 1;
        if (InputManager.Current.GetKeyDown(VerticalNegative))
            Vertical -= 1;
            */
    }
}
