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
        //Wander();
        ObstacleAndWallAvoidance();
    }



    void ObstacleAndWallAvoidance()
    {

        /*
        class ObstacleAvoidance (Seek):
2
3.3 Steering Behaviors 95
3 # Holds a collision detector
4 collisionDetector
5
6 # Holds the minimum distance to a wall (i.e., how far
7 # to avoid collision) should be greater than the
8 # radius of the character.
9 avoidDistance
10
11 # Holds the distance to look ahead for a collision
12 # (i.e., the length of the collision ray)
13 lookahead
14
15 # ... Other data is derived from the superclass ...
16
17 def getSteering():
18
19 # 1. Calculate the target to delegate to seek
20
21 # Calculate the collision ray vector
22 rayVector = character.velocity
23 rayVector.normalize()
24 rayVector *= lookahead
25
26 # Find the collision
27 collision = collisionDetector.getCollision(
28 character.position, rayVector)
29
30 # If have no collision, do nothing
31 if not collision: return None
32
33 # Otherwise create a target
34 target = collision.position + collision.normal* avoidDistance
35
36 # 2. Delegate to seek
37 return Seek.getSteering()*/
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
            /*
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
            */
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
