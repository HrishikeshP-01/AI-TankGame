using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlockingTargetMovement : MonoBehaviour
{
    [SerializeField]
    private Vector3 bounds;
    [SerializeField]
    private float movementSpeed = 10.0f;
    [SerializeField]
    private float turnSpeed = 3.0f;
    [SerializeField]
    private float targetPointTolerance = 5.0f;

    private Vector3 initialPosition;
    private Vector3 nextPosition;

    private void Awake()
    {
        initialPosition = transform.position;
        CalculateNextMovementPoint();
    }

    private void CalculateNextMovementPoint()
    {
        Vector3 targetPosition;
        targetPosition.x = Random.Range(initialPosition.x - bounds.x * 0.5f, initialPosition.x + bounds.x * 0.5f);
        targetPosition.y = Random.Range(initialPosition.y - bounds.y * 0.5f, initialPosition.y + bounds.y * 0.5f);
        targetPosition.z = Random.Range(initialPosition.z - bounds.z * 0.5f, initialPosition.z + bounds.z * 0.5f);

        nextPosition = targetPosition;
    }

    private void Update()
    {
        if (Vector3.Distance(transform.position, nextPosition) < targetPointTolerance) { CalculateNextMovementPoint(); }

        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(nextPosition - transform.position), turnSpeed * Time.deltaTime);
        transform.Translate(transform.forward * movementSpeed * Time.deltaTime);
    }

    private void OnDrawGizmos()
    {
        //Visualize the bounds of the target
        Gizmos.DrawWireCube(initialPosition, bounds);
        Gizmos.DrawWireSphere(nextPosition, 1.0f);
    }
}
