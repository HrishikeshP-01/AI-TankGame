using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boid : MonoBehaviour
{
    [SerializeField]
    private FlockController flockController;
    //Modified direction of the boid
    private Vector3 targetDirection;
    //Current direction of the boid
    private Vector3 direction;

    public FlockController FlockController
    {
        get { return FlockController; }
        set { flockController = value; }
    }

    public Vector3 Direction { get { return direction; } }

    private void Awake()
    {
        direction = transform.forward.normalized;
        if(flockController==null)
        {
            Debug.LogError("Flock controller not initialized");
            return;
        }
    }

    private void Update()
    {
        targetDirection = flockController.Flock(this, transform.localPosition, direction);

        if (targetDirection == Vector3.zero) { return; }

        direction = targetDirection.normalized;
        direction *= FlockController.speedModifier;
        transform.Translate(direction * Time.deltaTime);
    }
}
