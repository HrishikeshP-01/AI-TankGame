using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetScript : MonoBehaviour
{
    public Transform targetMarker;

    // Having an empty Start, Update etc. events can have a performance cost but an empty Start lets us have an enable/disable button in the inspector
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        int button = 0;
        // Get point of hit position when mouse is clicked
        if(Input.GetMouseButtonDown(button))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hitInfo;

            if(Physics.Raycast(ray.origin,ray.direction,out hitInfo))
            {
                Vector3 targetPosition = hitInfo.point;
                targetPosition.y = targetMarker.position.y;
                targetMarker.position = targetPosition;
            }
        }
    }
}
