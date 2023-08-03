using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class TankScript : MonoBehaviour
{
    private GameObject player;
    private Animator animator;

    public Transform pointA;
    public Transform pointB;
    public NavMeshAgent navMeshAgent;

    private int currentTarget;
    private float distanceToTarget;
    private Transform[] waypoints = null;

    private float currentDistance;

    public void Awake()
    {
        // GameObject is the class that contains helper methods for managing MonoBehavior components 
        player = GameObject.FindWithTag("Player");
        // gameObject is a property of Component class and returns the game object associated with this component
        animator = gameObject.GetComponent<Animator>();
        pointA = GameObject.Find("Pt1").transform;
        pointB = GameObject.Find("Pt2").transform;
        navMeshAgent = gameObject.GetComponent<NavMeshAgent>();

        waypoints = new Transform[2] { pointA, pointB };
        currentTarget = 0;
        navMeshAgent.SetDestination(waypoints[currentTarget].position);
    }

    public void SetNextWaypoint()
    {
        currentTarget = (currentTarget == 0) ? 1 : 0;
        navMeshAgent.SetDestination(waypoints[currentTarget].position);
        Debug.Log("Set next waypoint");
    }

    private void FixedUpdate()
    {
        // Check distance to player
        currentDistance = Vector3.Distance(player.transform.position, transform.position);
        animator.SetFloat("distanceFromPlayer", distanceToTarget);

        // Check for visibility
        // !! There are some variables at the top that I haven't written do that while handling this part

        // Get distance to next waypoint target
        distanceToTarget = Vector3.Distance(waypoints[currentTarget].position, transform.position);
        animator.SetFloat("distanceFromWaypoint", distanceToTarget);
    }
}
