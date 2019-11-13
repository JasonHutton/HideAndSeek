using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnapToSurface : MonoBehaviour
{
    public Vector3 SnapInDirection;

    // Start is called before the first frame update
    void Start()
    {
        RaycastHit hitInfo;
        if(Physics.Raycast(transform.position, SnapInDirection, out hitInfo))
        {
            transform.position = hitInfo.point;
        }

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
