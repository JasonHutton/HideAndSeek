using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputControllerI : MonoBehaviour
{
    public bool MoveForward;
    public bool MoveBackward;
    public bool TurnLeft;
    public bool TurnRight;

    // Start is called before the first frame update
    void Start()
    {
        ResetMovement();
    }
    public void ResetMovement()
    {
        MoveForward = false;
        MoveBackward = false;
        TurnLeft = false;
        TurnRight = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
