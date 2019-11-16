using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementEffector : MonoBehaviour
{
    // Start is called before the first frame update
    private InputControllerI ControllerScript;

    public bool MoveForward;
    public bool MoveBackward;
    public bool TurnLeft;
    public bool TurnRight;

    private Rigidbody rb;

    void Start()
    {
        ControllerScript = GetComponent<InputControllerI>();
        rb = GetComponentInChildren<Rigidbody>();
        //rb.isKinematic = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (ControllerScript != null)
        {
            MoveForward = ControllerScript.MoveUp;
            MoveBackward = ControllerScript.MoveDown;
            TurnLeft = ControllerScript.TurnLeft;
            TurnRight = ControllerScript.TurnRight;
        }

        //getNewOrientation(transform.eulerAngles, rb.velocity);
        //Debug.DrawLine(transform.position, getNewOrientation(transform.eulerAngles, rb.velocity));
        Vector3 vel = rb.velocity;
        /*if (Vector3.Magnitude(vel) > 0)
            vel = Vector3.Normalize(vel);
        else
            vel = Vector3.Normalize(transform.rotation.eulerAngles);*/
        //Debug.DrawLine(transform.position, transform.forward * 2);
    }

    private void FixedUpdate()
    {
        HandleMovement();
    }

    void HandleMovement()
    {
        Vector3 position = Vector3.zero;// transform.position;// rb.position;
        Vector3 rotation = rb.rotation.eulerAngles;
        float forwardBack = 0.0f;
        float speed = 10.0f;

        //if (TurnLeft)
            //rotation.y -= 0.1f;

        //if (TurnRight)
            //rotation.y += 0.1f;

        if (MoveForward)
            forwardBack = 1.0f;
        //forwardBack += 0.1f;
        //position.z += 0.1f;


        if (MoveBackward)
            forwardBack = -1.0f;
        //forwardBack -= 0.1f;
        //position.z -= 0.1f;
        if (forwardBack == 0.0f)
            return;
        //rb.MovePosition(position);
        Vector3 movement = new Vector3(forwardBack * speed, 0, 0);
        //rb.MovePosition(transform.position + movement * Time.fixedDeltaTime);
        rb.MovePosition(rb.transform.position + movement * Time.fixedDeltaTime);
        //rb.MoveRotation(Quaternion.Euler(rotation));
        Debug.Log(movement);
    }

    /*
     * 
     * Vector3 direction = (target.transform.position - transform.position).normalized;
           rigidbody.MovePosition(transform.position + direction * movementSpeed * Time.deltaTime);
     * 
     * */

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Barrier")
        {
            rb.velocity = Vector3.zero;
            Debug.Log("Collided with a Barrier!");
        }
    }

    Vector3 getNewOrientation(Vector3 currentOrientation, Vector3 currentVelocity)
    {
        
        // Make sure we have a velocity
        if (Vector3.Magnitude(currentVelocity) > 0)
        {
            // Calculate orientation using an arc tangent of the velocity components.
            //return Mathf.Atan2(-currentVelocity.x, currentVelocity.z);
            return Vector3.Normalize(currentVelocity);
        }
        // Otherwise use the current orientation
        else
        {
            return Vector3.Normalize(transform.rotation.eulerAngles);
        }
    }
}
