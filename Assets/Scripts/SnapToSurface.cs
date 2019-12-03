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
            Vector3 newPosition = hitInfo.point;
            newPosition.y += transform.localScale.y / 2; // SnapInDirection would be far better for this, but not going to deal with it right now.
            transform.position = newPosition;
        }

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
