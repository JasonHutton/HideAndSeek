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

        ((ShotApproachingDecision)((CheckShieldOnDecision)dTree).trueNode).trueNode = new IsGunReadyDecision(); // A shot is coming, leave the shield alone. Can we return fire?
        ((IsGunReadyDecision)(((ShotApproachingDecision)((CheckShieldOnDecision)dTree).trueNode).trueNode)).trueNode = new FacingEnemyDecision(); // Are we facing the enemy?
        ((FacingEnemyDecision)(((IsGunReadyDecision)(((ShotApproachingDecision)((CheckShieldOnDecision)dTree).trueNode).trueNode)).trueNode)).trueNode = new ShootGunAction(); // Shoot!
        ((FacingEnemyDecision)(((IsGunReadyDecision)(((ShotApproachingDecision)((CheckShieldOnDecision)dTree).trueNode).trueNode)).trueNode)).falseNode = new Action(); // Not facing the enemy. Do nothing new.
        ((IsGunReadyDecision)(((ShotApproachingDecision)((CheckShieldOnDecision)dTree).trueNode).trueNode)).falseNode = new Action();// We can't return fire. Do nothing new.

        ((ShotApproachingDecision)((CheckShieldOnDecision)dTree).trueNode).falseNode = new ShieldOffAction(); // Turn the shield off to conserve power. We don't need it right now.

        ((CheckShieldOnDecision)dTree).falseNode = new CanEnableShieldDecision(); // Shield is off. Can we have the shield on?

        ((CanEnableShieldDecision)((CheckShieldOnDecision)dTree).falseNode).trueNode = new ShotApproachingDecision(); // Is there a shot approaching the tank? SHOULD we turn it on?
        ((ShotApproachingDecision)(((CanEnableShieldDecision)((CheckShieldOnDecision)dTree).falseNode).trueNode)).trueNode = new ShieldOnAction(); // Turn the shield on
        ((ShotApproachingDecision)(((CanEnableShieldDecision)((CheckShieldOnDecision)dTree).falseNode).trueNode)).falseNode = new IsGunReadyDecision(); // Don't turn the shield on, but is the gun ready?
        (((IsGunReadyDecision)((ShotApproachingDecision)(((CanEnableShieldDecision)((CheckShieldOnDecision)dTree).falseNode).trueNode)).falseNode)).trueNode = new FacingEnemyDecision(); // Are we facing the enemy?
        ((FacingEnemyDecision)(((IsGunReadyDecision)((ShotApproachingDecision)(((CanEnableShieldDecision)((CheckShieldOnDecision)dTree).falseNode).trueNode)).falseNode)).trueNode).trueNode = new ShootGunAction(); // Shoot!
        ((FacingEnemyDecision)(((IsGunReadyDecision)((ShotApproachingDecision)(((CanEnableShieldDecision)((CheckShieldOnDecision)dTree).falseNode).trueNode)).falseNode)).trueNode).falseNode = new Action(); // Do nothing new
        (((IsGunReadyDecision)((ShotApproachingDecision)(((CanEnableShieldDecision)((CheckShieldOnDecision)dTree).falseNode).trueNode)).falseNode)).falseNode = new Action(); // Do nothing new


        ((CanEnableShieldDecision)((CheckShieldOnDecision)dTree).falseNode).falseNode = new IsGunReadyDecision(); // Can't turn shield on. But, can we shoot?
        ((IsGunReadyDecision)(((CanEnableShieldDecision)((CheckShieldOnDecision)dTree).falseNode).falseNode)).trueNode = new FacingEnemyDecision(); // Are we facing the enemy?
        (((FacingEnemyDecision)(((IsGunReadyDecision)(((CanEnableShieldDecision)((CheckShieldOnDecision)dTree).falseNode).falseNode)).trueNode))).trueNode = new ShootGunAction(); // Are we facing the enemy?
        (((FacingEnemyDecision)(((IsGunReadyDecision)(((CanEnableShieldDecision)((CheckShieldOnDecision)dTree).falseNode).falseNode)).trueNode))).falseNode = new Action(); // Don't do anything new

        ((IsGunReadyDecision)(((CanEnableShieldDecision)((CheckShieldOnDecision)dTree).falseNode).falseNode)).falseNode = new Action(); // Do nothing new.




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

    public float AngleToTarget() // This would be way better for gun targeting, if we took into account distance too, not just angle. And target vector. But not touching that for now.
    {
        Rigidbody srb = tank.GetComponentInChildren<Rigidbody>();
        if (srb != null)
        {
            return Vector3.Angle(srb.transform.forward, target._static.position - self._static.position);
        }

        return 180f; // We're definitely not facing the target if we can't find it.
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        bool bWantToFire = false;
        bool bWantToShield = false;

        
        // This would be better to traverse and set based on what each node is. For now, this is good enough if gross.
        ((Decision)dTree).testData = mEffector.ShieldStatus;
        ((ShotApproachingDecision)(((Decision)dTree).trueNode)).testData = CheckForNearestShellDistance();
        ((IsGunReadyDecision)(((ShotApproachingDecision)(((Decision)dTree).trueNode)).trueNode)).testData = mEffector.IsGunReady();
        ((FacingEnemyDecision)(((IsGunReadyDecision)(((ShotApproachingDecision)(((Decision)dTree).trueNode)).trueNode)).trueNode)).testData = AngleToTarget();

        ((Decision)(((Decision)dTree).falseNode)).testData = mEffector.ShieldStatus;
        ((ShotApproachingDecision)(((Decision)(((Decision)dTree).falseNode)).trueNode)).testData = CheckForNearestShellDistance();
        ((IsGunReadyDecision)(((Decision)(((Decision)dTree).falseNode)).falseNode)).testData = mEffector.IsGunReady();
        ((FacingEnemyDecision)((IsGunReadyDecision)(((Decision)(((Decision)dTree).falseNode)).falseNode)).trueNode).testData = AngleToTarget();

        ((IsGunReadyDecision)(((ShotApproachingDecision)(((Decision)(((Decision)dTree).falseNode)).trueNode)).falseNode)).testData = mEffector.IsGunReady();
        ((FacingEnemyDecision)(((IsGunReadyDecision)(((ShotApproachingDecision)(((Decision)(((Decision)dTree).falseNode)).trueNode)).falseNode)).trueNode)).testData = AngleToTarget();

        DecisionTreeNode node = dTree.MakeDecision();
        if (node is ShieldOnAction)
        {
            bWantToShield = true;
        }
        if(node is ShieldOffAction)
        {
            bWantToShield = false; // Seems to be a bug here, shield isn't wanting to turn off. *Might be a logic issue with toggling a button to enable/disable stuff?)
        }
        if(node is ShootGunAction)
        {
            bWantToFire = true;
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