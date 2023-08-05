using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Avoidance : MonoBehaviour
{
    [SerializeField]
    private float movementSpeed = 20.0f;
    [SerializeField]
    private float rotationSpeed = 5.0f;
    [SerializeField]
    private float force = 50.0f;
    [SerializeField]
    private float minimumAvoidanceDistance = 20.0f;
    [SerializeField]
    private float toleranceRadius = 3.0f;

    private float currentSpeed;
    private Vector3 targetPoint;
    private RaycastHit mouseHit;
    private Camera mainCamera;
    private Vector3 direction;
    private Quaternion targetRotation;
    private RaycastHit avoidanceHit;
    private Vector3 HitNormal;

    // Start is called before the first frame update
    void Start()
    {
        mainCamera = Camera.main;
        targetPoint = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        CheckInput();
        Debug.Log(targetPoint);
        direction = targetPoint - transform.position;
        direction.Normalize();

        // Apply obstacle avoidance
        ApplyObstacleAvoidance(ref direction);

        if (Vector3.Distance(targetPoint, transform.position) < toleranceRadius) { return; }

        currentSpeed = movementSpeed * Time.deltaTime;

        // Rotate the agent towards its target direction
        targetRotation = Quaternion.LookRotation(direction);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        // Now that the agent is looking in the direction its needs to go it just has to move forward
        transform.position += transform.forward * currentSpeed;
    }

    private void CheckInput()
    {
        if(Input.GetMouseButtonDown(0))
        {
            var ray = mainCamera.ScreenPointToRay(Input.mousePosition);
            if(Physics.Raycast(ray,out mouseHit, 5000.0f))
            {
                targetPoint = mouseHit.point;
            }
            else { Debug.LogError("Not"); }
        }
    }

    private void ApplyObstacleAvoidance(ref Vector3 direction)
    {
        // Only detect layer 8 obstacles
        /*We use bitshifting to create a layermask with a value of 0100000000
         Here only the 8th position has a 1 i.e., only the 8th position is active.
        This means only the 8th layer will be used. Layers start from 0 so 8th is actually 9*/
        int layerMask = 1 << 8;
        /* 1 << 8 is a bit operation
         0000 0000 0001, the 1 is shifted left 8 times
         0001 0000 0000*/

        // Check the agent hit with obstacles within its minimum distance to avoid
        if(Physics.Raycast(transform.position,transform.forward,out avoidanceHit, minimumAvoidanceDistance, layerMask))
        {
            // Get normal of hit to calculate new direction
            HitNormal = avoidanceHit.normal;
            HitNormal.y = 0.0f; // Don't want to move along y axis

            // Get new directional vector by adding a force to agent's forward vector
            direction = transform.forward + HitNormal * force;
        }
    }
}
