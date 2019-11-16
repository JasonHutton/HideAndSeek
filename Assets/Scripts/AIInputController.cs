using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIInputController : InputControllerI
{
    private float directionChangeInterval;
    private float timer;
    public float minDirectionChangeInterval;
    public float maxDirectionChangeInterval;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        //RandomWanderViaButtons();
        Wander();
    }

    void Wander()
    {

    }

    void RandomWanderViaButtons()
    {
        timer += Time.deltaTime;

        if (timer > directionChangeInterval)
        {
            timer = 0;
            directionChangeInterval = Random.Range(minDirectionChangeInterval, maxDirectionChangeInterval);
            ResetMovement();

            int decision = Mathf.FloorToInt(Random.Range(0.0f, 8.0f));
            switch (decision)
            {
                case 0:
                    MoveUp = true;
                    break;
                case 1:
                    MoveDown = true;
                    break;
                case 2:
                    MoveLeft = true;
                    break;
                case 3:
                    MoveRight = true;
                    break;
                case 4:
                    MoveUp = true;
                    MoveLeft = true;
                    break;
                case 5:
                    MoveUp = true;
                    MoveRight = true;
                    break;
                case 6:
                    MoveDown = true;
                    MoveLeft = true;
                    break;
                case 7:
                    MoveDown = true;
                    MoveRight = true;
                    break;
            }
        }
    }

    /*
    // https://github.com/libgdx/gdx-ai/wiki/Steering-Behaviors
    public static float calculateOrientationFromLinearVelocity(Steerable<T> character)
    {
        // If we haven't got any velocity, then we can do nothing.
        if (character.getLinearVelocity().isZero(character.getZeroLinearSpeedThreshold()))
            return character.getOrientation();

        return character.vectorToAngle(character.getLinearVelocity());
    }


    private void applySteering(SteeringAcceleration<Vector2> steering, float time)
    {
        // Update position and linear velocity. Velocity is trimmed to maximum speed
        this.position.mulAdd(linearVelocity, time);
        this.linearVelocity.mulAdd(steering.linear, time).limit(this.getMaxLinearSpeed());

        // Update orientation and angular velocity
        if (independentFacing)
        {
            this.orientation += angularVelocity * time;
            this.angularVelocity += steering.angular * time;
        }
        else
        {
            // For non-independent facing we have to align orientation to linear velocity
            float newOrientation = calculateOrientationFromLinearVelocity(this);
            if (newOrientation != this.orientation)
            {
                this.angularVelocity = (newOrientation - this.orientation) * time;
                this.orientation = newOrientation;
            }
        }
    }*/
}

/*
class KinematicWander
{   struct Static
    {
        Vector3 position;
        float orientation;
    }

    struct Kinematic
    {
        Vector3 position;
        float orientation;
        Vector3 velocity;
        float rotation;
    }

    struct SteeringOutput
    {
        Vector3 linear;
        float angular;
    }

    // Holds the static data for the character
    private Static character;

    // Holds the maximum speed the character can travel
    public float maxSpeed;

    // Holds the maximum rotation speed we’d like, probably
    // should be smaller than the maximum possible, to allow
    // a leisurely change in direction
    public float maxRotation;

    KinematicSteeringOutput getSteering()
    {

        // Create the structure for output
        steering = new KinematicSteeringOutput()

    // Get velocity from the vector form of the orientation
        steering.velocity = maxSpeed *
    character.orientation.asVector()
    Steering Behaviors 57

    // Change our orientation randomly
        steering.rotation = randomBinomial() * maxRotation

    // Output the steering
        return steering
            }
}
*/