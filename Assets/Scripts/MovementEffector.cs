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

    public bool Fire;
    public bool Shield;

    private Rigidbody rb;
    private Tank tank;
    private Gun GunScript;
    public Shield ShieldScript;

    public bool ShieldStatus;

    private float maxSpeed;
    private float maxRotSpeed;

    public float GetShieldCharge()
    {
        return ShieldScript.shieldCharge;
    }

    public float GetShieldDepletionPerTick()
    {
        return ShieldScript.shieldDepletionPerTick;
    }

    public float GetTotalShieldCharge()
    {
        return ShieldScript.totalShieldCharge;
    }

    public bool IsGunReady()
    {
        return GunScript.ReadyToFire();
    }

    void Start()
    {
        ControllerScript = GetComponent<InputControllerI>();
        rb = GetComponentInChildren<Rigidbody>();
        tank = GetComponent<Tank>();
        maxSpeed = tank.maxSpeed;
        maxRotSpeed = tank.maxRotSpeed;
        GunScript = GetComponent<Gun>();
        ShieldScript = GetComponentInChildren<Shield>(true);
        if (ShieldScript)
        {
            ShieldScript.SetShieldActive(ShieldStatus);
            tank.shielded = ShieldScript.GetShieldActive();
        }
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

            Fire = ControllerScript.Fire;
            Shield = ControllerScript.Shield;
        }
    }

    private void FixedUpdate()
    {
        HandleMovement();
        HandleActions();
        //Debug.Log(transform.forward);
    }

    void HandleMovement()
    {
        Vector3 position = Vector3.zero;// transform.position;// rb.position;
        Vector3 rotation = rb.rotation.eulerAngles;
        float forwardBack = 0.0f;
        float leftRight = 0.0f;

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

        rb.velocity = rb.transform.forward * forwardBack * maxSpeed * Time.fixedDeltaTime;
        rb.angularVelocity = rb.transform.up * leftRight * maxRotSpeed * Time.fixedDeltaTime;
    }

    void HandleActions()
    {
        if(GunScript && Fire)
        {
            // Check firing delay.
            // Fire
            GunScript.Fire();
        }
        if(ShieldScript && Shield)
        {
            // Check shield delay
            // Shield
            ShieldScript.SetShieldActive(!ShieldScript.GetShieldActive());
            ShieldStatus = ShieldScript.GetShieldActive();
            tank.shielded = ShieldScript.GetShieldActive();
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Barrier")
        {
            // This might be redundant? Leaving it.
            Debug.Log("Collided with a Barrier!");
        }
    }


}
