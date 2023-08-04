using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Perspective : Sense
{
    public int fieldOfView = 45;
    public int viewDistance = 100;

    private Transform playerTransform;
    private Vector3 rayDirection;

    protected override void Initialize()
    {
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
    }

    protected override void UpdateSense()
    {
        elapsedTime += Time.deltaTime;
        if (elapsedTime > detectionRate) { DetectAspect(); }
    }

    void DetectAspect()
    {
        RaycastHit hit;
        rayDirection = playerTransform.position - transform.position;

        // Check is player is within view range
        if(Vector3.Angle(rayDirection,transform.forward)<fieldOfView)
        {
            // Check if player is within field of view
            if(Physics.Raycast(transform.position, rayDirection, out hit,viewDistance))
            {
                Aspect aspect = hit.collider.GetComponent<Aspect>();
                if (aspect != null)
                {
                    // Check the aspect
                    if (aspect.aspectType != aspectName) { Debug.Log("Enemy detected"); }
                }
            }
        }
    }

    void OnDrawGizmos()
    {
        if (playerTransform == null) { return; }

        Debug.DrawLine(transform.position, playerTransform.position, Color.red);

        Vector3 frontRayPoint = transform.position + (transform.forward * viewDistance);

        // Approx. persective visualization
        Vector3 leftRayPoint = frontRayPoint;
        leftRayPoint.x -= fieldOfView * 0.5f;
        Vector3 rightRayPoint = frontRayPoint;
        rightRayPoint.x += fieldOfView * 0.5f;

        Debug.DrawLine(transform.position, frontRayPoint, Color.green);
        Debug.DrawLine(transform.position, leftRayPoint, Color.green);
        Debug.DrawLine(transform.position, rightRayPoint, Color.green);
    }
}
