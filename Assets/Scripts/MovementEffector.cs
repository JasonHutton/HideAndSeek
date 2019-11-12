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

    private Rigidbody rb;

    void Start()
    {
        ControllerScript = GetComponent<InputControllerI>();
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (ControllerScript != null)
        {
            MoveLeft = ControllerScript.MoveLeft;
            MoveRight = ControllerScript.MoveRight;
            MoveUp = ControllerScript.MoveUp;
            MoveDown = ControllerScript.MoveDown;
        }
    }

    private void FixedUpdate()
    {
        HandleMovement();
    }

    void HandleMovement()
    {
        Vector3 position = transform.position;

        if (MoveLeft)
            position.x -= 0.1f;

        if (MoveRight)
            position.x += 0.1f;

        if (MoveUp)
            position.z += 0.1f;

        if (MoveDown)
            position.z -= 0.1f;

        transform.position = position;
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Barrier")
        {
            rb.velocity = Vector3.zero;
            Debug.Log("Collided with a Barrier!");
        }
    }
}
