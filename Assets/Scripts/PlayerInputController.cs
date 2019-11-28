using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInputController : InputControllerI
{
    private float Horizontal;
    private float Vertical;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        Horizontal = Input.GetAxisRaw("Horizontal");
        Vertical = Input.GetAxisRaw("Vertical");

        ResetMovement();

        if (Horizontal < 0.0f)
            TurnLeft = true;
        if (Horizontal > 0.0f)
            TurnRight = true;
        if (Vertical < 0.0f)
            MoveBackward = true;
        if (Vertical > 0.0f)
            MoveForward = true;

        // Disable movement direction if inputs are contradicting each other.
        if (TurnLeft && TurnRight)
            TurnLeft = TurnRight = false;
        if (MoveForward && MoveBackward)
            MoveForward = MoveBackward = false;

        Fire = Input.GetButtonDown("Fire1"); // Left Control
        Shield = Input.GetButtonDown("Jump"); // Space

        //Debug.Log(string.Format("MoveLeft: {0} MoveRight: {1} MoveUp: {2} MoveDown: {3}", MoveLeft, MoveRight, MoveUp, MoveDown));
    }
}
