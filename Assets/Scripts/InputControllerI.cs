using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputControllerI : MonoBehaviour
{
    public bool MoveLeft;
    public bool MoveRight;
    public bool MoveUp;
    public bool MoveDown;

    // Start is called before the first frame update
    void Start()
    {
        ResetMovement();
    }
    public void ResetMovement()
    {
        MoveUp = false;
        MoveDown = false;
        MoveLeft = false;
        MoveRight = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
