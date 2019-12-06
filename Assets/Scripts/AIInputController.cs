using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class AIInputController : InputControllerI
{
    private float Horizontal;
    private float Vertical;

    public Kinematic target;
    public Kinematic self;
    public Tank tank;
    private MovementEffector mEffector;
    private DecisionTreeNode dTree;

    private StateMachine sm;
    public SteeringOutput steering;

    // Start is called before the first frame update
    void Start()
    {
        self = GetComponent<Kinematic>();
        tank = GetComponent<Tank>();
        mEffector = GetComponent<MovementEffector>();
        dTree = new CheckShieldOnDecision();

        ((Decision)dTree).testData = mEffector.ShieldStatus;
        ((CheckShieldOnDecision)dTree).trueNode = new ShotApproachingDecision(); // Shield is on. Is a shot approaching? (Should we leave it on?)

        ((ShotApproachingDecision)((CheckShieldOnDecision)dTree).trueNode).trueNode = new Action(); // Leave the shield on, a shot is coming!
        ((ShotApproachingDecision)((CheckShieldOnDecision)dTree).trueNode).falseNode = new ShieldOffAction(); // Turn the shield off to conserve power. We don't need it right now.

        ((CheckShieldOnDecision)dTree).falseNode = new CanEnableShieldDecision(); // Shield is off. Can we have the shield on?

        ((CanEnableShieldDecision)((CheckShieldOnDecision)dTree).falseNode).trueNode = new ShotApproachingDecision(); // Is there a shot approaching the tank?
        ((ShotApproachingDecision)(((CanEnableShieldDecision)((CheckShieldOnDecision)dTree).falseNode).trueNode)).trueNode = new ShieldOnAction(); // Turn the shield on
        ((ShotApproachingDecision)(((CanEnableShieldDecision)((CheckShieldOnDecision)dTree).falseNode).trueNode)).falseNode = new Action(); // Don't turn shield on.

        ((CanEnableShieldDecision)((CheckShieldOnDecision)dTree).falseNode).falseNode = new Action(); // Can't turn shield on.
        

        List<State> states = new List<State>();
        
        State fightState = new FightState();
        fightState.GetTransitions().Add(new Transition());
        states.Add(fightState);
        
        State fleeState = new FleeState();
        fleeState.GetTransitions().Add(new Transition());
        states.Add(fleeState);

        states[0].GetTransitions()[0].targetState = states[1];
        states[1].GetTransitions()[0].targetState = states[0];

        //states[0].GetTransitions()[0].bTriggered = true;
        sm = new StateMachine(this, states);
    }

    float CheckForNearestShellDistance()
    {
        float shortest = 100f; // Far away.

        GameObject[] shells = GameObject.FindGameObjectsWithTag("Shell");
        if (shells.Length == 0)
            return shortest;

        foreach (GameObject go in shells)
        {
            Rigidbody goRB = go.GetComponent<Rigidbody>();
            if (goRB != null)
            {
                float dist = Vector3.Distance(self._static.position, goRB.position);
                if (dist < shortest)
                {
                    shortest = dist;
                }
            }
        }

        return shortest;
    }

    public bool CheckShieldDanger()
    {
        float percentDanger = 40.0f;
        float percent = (mEffector.GetShieldCharge() / mEffector.GetTotalShieldCharge()) * 100.0f;

        return percent < percentDanger;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        bool bWantToFire = false;
        bool bWantToShield = false;

        

        ((Decision)dTree).testData = mEffector.ShieldStatus;
        ((ShotApproachingDecision)(((Decision)dTree).trueNode)).testData = CheckForNearestShellDistance();

        ((Decision)(((Decision)dTree).falseNode)).testData = mEffector.ShieldStatus;
        ((ShotApproachingDecision)(((Decision)(((Decision)dTree).falseNode)).trueNode)).testData = CheckForNearestShellDistance();
        //((CanEnableShieldDecision)(((CheckShieldOnDecision)dTree).trueNode)).testData = mEffector.ShieldStatus;
        //((Decision)dTree).testData = mEffector.ShieldStatus;

        DecisionTreeNode node = dTree.MakeDecision();
        if (node is ShieldOnAction)
        {
            bWantToShield = true;
        }
        if(node is ShieldOffAction)
        {
            bWantToShield = false; // Seems to be a bug here, shield isn't wanting to turn off.
        }

        steering = new SteeringOutput();

        // Blend Steering
        //steering += Seek.GetSteering(target._static.position, self._static.position, tank.maxSpeed).Weight(1.0f);
        sm.Update();
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