using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputControllerI : MonoBehaviour
{
    public bool MoveForward;
    public bool MoveBackward;
    public bool TurnLeft;
    public bool TurnRight;
    public bool Fire;
    public bool Shield;

    // Start is called before the first frame update
    void Start()
    {
        ResetMovement();
        ResetActions();
    }
    public void ResetMovement()
    {
        MoveForward = false;
        MoveBackward = false;
        TurnLeft = false;
        TurnRight = false;
    }

    public void ResetActions()
    {
        Fire = false;
        Shield = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
