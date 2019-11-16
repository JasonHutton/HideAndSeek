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
            MoveForward = ControllerScript.MoveForward;
            MoveBackward = ControllerScript.MoveBackward;
            TurnLeft = ControllerScript.TurnLeft;
            TurnRight = ControllerScript.TurnRight;
        }

        //getNewOrientation(transform.eulerAngles, rb.velocity);
        //Debug.DrawLine(transform.position, getNewOrientation(transform.eulerAngles, rb.velocity));
        //Vector3 vel = rb.velocity;
        //if (Vector3.Magnitude(vel) > 0)
            //vel = Vector3.Normalize(vel);
        //else
            //vel = Vector3.Normalize(rb.transform.forward);
        //vel = Vector3.Normalize(transform.rotation.eulerAngles);
        //Debug.DrawLine(transform.position, transform.forward * 2);


        //Debug.DrawLine(rb.transform.position, rb.transform.position + Vector3.Normalize(rb.transform.forward) * 2.0f);
        Debug.DrawLine(rb.transform.position, rb.transform.position + Vector3.Normalize(rb.transform.forward) * 2.0f);
        //Debug.DrawLine(rb.transform.position, Vector3.Normalize(rb.velocity) * 2.0f);
    }

    private void FixedUpdate()
    {
        HandleMovement();
        //Debug.Log(transform.forward);
    }

    void HandleMovement()
    {
        Vector3 position = Vector3.zero;// transform.position;// rb.position;
        Vector3 rotation = rb.rotation.eulerAngles;
        float forwardBack = 0.0f;
        float leftRight = 0.0f;
        float speed = 1000.0f;
        float rotationSpeed = 100.0f;
        float maxSpeed = 100.0f;

        if (TurnLeft)
            leftRight = -1.0f;

        if (TurnRight)
            leftRight = 1.0f;

        if (MoveForward)
            forwardBack = 1.0f;

        if (MoveBackward)
            forwardBack = -1.0f;

        if (forwardBack < 0.0f)
            leftRight = -leftRight;

        rb.velocity = rb.transform.forward * forwardBack * speed * Time.fixedDeltaTime;
        rb.angularVelocity = rb.transform.up * leftRight * rotationSpeed * Time.fixedDeltaTime;
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
            //transform.rotation = Quaternion.FromToRotation(Vector3.up, hit.normal);
            //rb.velocity = Vector3.zero; // This is simplified, need to only stop in the direction of the barrier.
            Debug.Log("Collided with a Barrier!");

            //ContactPoint contact = collision.contacts[0];

            // Rotate the object so that the y-axis faces along the normal of the surface
            //Quaternion rot = Quaternion.FromToRotation(Vector3.up, contact.normal);
            //rb.rotation = rot;
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
