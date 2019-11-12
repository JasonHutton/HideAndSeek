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
            MoveLeft = true;
        if (Horizontal > 0.0f)
            MoveRight = true;
        if (Vertical < 0.0f)
            MoveDown = true;
        if (Vertical > 0.0f)
            MoveUp = true;

        // Disable movement direction if inputs are contradicting each other.
        if (MoveLeft && MoveRight)
            MoveLeft = MoveRight = false;
        if (MoveUp && MoveDown)
            MoveUp = MoveDown = false;

        //Debug.Log(string.Format("MoveLeft: {0} MoveRight: {1} MoveUp: {2} MoveDown: {3}", MoveLeft, MoveRight, MoveUp, MoveDown));
    }
}
