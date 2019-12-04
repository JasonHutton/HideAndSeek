using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIInputController2 : InputControllerI
{
    private float Horizontal;
    private float Vertical;

    public Kinematic target;
    private Kinematic self;
    private Tank tank;

    // Start is called before the first frame update
    void Start()
    {
        self = GetComponent<Kinematic>();
        tank = GetComponent<Tank>();
    }

    // Update is called once per frame
    void Update()
    {
        SteeringOutput steering = new SteeringOutput();
        // Blend Steering
        steering += Seek.GetSteering(target._static.position, self._static.position, tank.maxSpeed).Weight(1.0f);
        Rigidbody srb = tank.GetComponentInChildren<Rigidbody>();
        //steering.Crop();
        float angle = Vector3.SignedAngle(steering.velocity.normalized, srb.transform.forward, transform.up);

        Horizontal = Input.GetAxisRaw("Horizontal");
        Vertical = Input.GetAxisRaw("Vertical");

        ResetMovement();

        if (angle > -90 && angle < 90)
            MoveForward = true;
        if (angle < -90 || angle > 90)
            MoveBackward = true;
        if (angle > 0)
            TurnLeft = true;
        if (angle < 0)
            TurnRight = true;

        // Disable movement direction if inputs are contradicting each other.
        if (TurnLeft && TurnRight)
            TurnLeft = TurnRight = false;
        if (MoveForward && MoveBackward)
            MoveForward = MoveBackward = false;

        //Fire = Input.GetButtonDown("Fire1"); // Left Control
        //Shield = Input.GetButtonDown("Jump"); // Space

        //Debug.Log(string.Format("MoveLeft: {0} MoveRight: {1} MoveUp: {2} MoveDown: {3}", MoveLeft, MoveRight, MoveUp, MoveDown));
    }
}