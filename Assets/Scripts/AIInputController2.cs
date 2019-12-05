using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class AIInputController2 : InputControllerI
{
    private float Horizontal;
    private float Vertical;

    public Kinematic target;
    private Kinematic self;
    private Tank tank;
    private MovementEffector mEffector;
    private DecisionTreeNode dTree;

    // Start is called before the first frame update
    void Start()
    {
        self = GetComponent<Kinematic>();
        tank = GetComponent<Tank>();
        mEffector = GetComponent<MovementEffector>();
        dTree = new CheckShieldOnDecision();
        ((Decision)dTree).testData = mEffector.ShieldStatus;
        ((CheckShieldOnDecision)dTree).trueNode = new Action(); // Shield is on. Keep the shield on.
        ((CheckShieldOnDecision)dTree).falseNode = new CanEnableShieldDecision(); // Shield is off. Should we have the shield on?
        ((CanEnableShieldDecision)((CheckShieldOnDecision)dTree).falseNode).trueNode = new ShieldOnAction(); // Turn the shield on
        ((CanEnableShieldDecision)((CheckShieldOnDecision)dTree).falseNode).falseNode = new Action(); // Can't turn shield on.
    }

    // Update is called once per frame
    void Update()
    {
        bool bWantToFire = false;
        bool bWantToShield = false;

        ((Decision)dTree).testData = mEffector.ShieldStatus;
        //((Decision)(((Decision)dTree).falseNode)).testData = mEffector.ShieldStatus;
        //((CanEnableShieldDecision)(((CheckShieldOnDecision)dTree).trueNode)).testData = mEffector.ShieldStatus;
        //((Decision)dTree).testData = mEffector.ShieldStatus;

        DecisionTreeNode node = dTree.MakeDecision();
        if (node is ShieldOnAction)
        {
            bWantToShield = true;
        }

        SteeringOutput steering = new SteeringOutput();
        // Blend Steering
        steering += Seek.GetSteering(target._static.position, self._static.position, tank.maxSpeed).Weight(1.0f);
        //steering += Flee.GetSteering(target._static.position, self._static.position, tank.maxSpeed).Weight(1.0f);
        //steering += AlignToTarget2.GetSteering(target._static.position, self._static.position, self._static.orientation).Weight(1.0f);
        Rigidbody srb = tank.GetComponentInChildren<Rigidbody>();
        //steering.Crop();
        float angle = Vector3.SignedAngle(steering.velocity.normalized, srb.transform.forward, transform.up);

        //Horizontal = Input.GetAxisRaw("Horizontal");
        //Vertical = Input.GetAxisRaw("Vertical");

        ResetMovement();

        if (steering.velocity.magnitude > 0)
        {
            if (angle > -90 && angle < 90)
                MoveForward = true;
            if (angle < -90 || angle > 90)
                MoveBackward = true;
            if (angle > 0)
                TurnLeft = true;
            if (angle < 0)
                TurnRight = true;
        }
        if (steering.rotation < 0)
            TurnLeft = true;
        if (steering.rotation > 0)
            TurnRight = true;

        // Disable movement direction if inputs are contradicting each other.
        if (TurnLeft && TurnRight)
            TurnLeft = TurnRight = false;
        if (MoveForward && MoveBackward)
            MoveForward = MoveBackward = false;

        Fire = bWantToFire;
        Shield = bWantToShield;

        //Debug.Log(string.Format("MoveLeft: {0} MoveRight: {1} MoveUp: {2} MoveDown: {3}", MoveLeft, MoveRight, MoveUp, MoveDown));
    }
}