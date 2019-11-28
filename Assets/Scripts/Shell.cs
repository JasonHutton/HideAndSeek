using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shell : MonoBehaviour
{
    // Start is called before the first frame update
    public Vector3 velocity;
    private Rigidbody rb;
    //public bool alive;
    private float shotTime;

    void Start()
    {
        //alive = true;
        shotTime = Time.fixedTime;
        rb = GetComponent<Rigidbody>();
        rb.velocity = velocity;
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.fixedTime > shotTime + 2)
            Destroy(this.gameObject);
            //alive = false;
    }

    private void FixedUpdate()
    {
        
    }
}
