using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawFacingLine : MonoBehaviour
{
    Rigidbody rb;
    public float length;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        if (rb == null)
        {
            rb = GetComponentInChildren<Rigidbody>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(rb != null)
        { 
            Debug.DrawLine(rb.transform.position, rb.transform.position + Vector3.Normalize(rb.transform.forward) * length);
        }
    }
}
