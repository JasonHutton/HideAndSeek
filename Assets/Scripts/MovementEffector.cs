using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementEffector : MonoBehaviour
{
    // Start is called before the first frame update
    private InputControllerI ControllerScript;

    public bool MoveLeft;
    public bool MoveRight;
    public bool MoveUp;
    public bool MoveDown;

    void Start()
    {
        ControllerScript = GetComponent<InputControllerI>();
    }

    // Update is called once per frame
    void Update()
    {
        MoveLeft = ControllerScript.MoveLeft;
        MoveRight = ControllerScript.MoveRight;
        MoveUp = ControllerScript.MoveUp;
        MoveDown = ControllerScript.MoveDown;
    }
}
