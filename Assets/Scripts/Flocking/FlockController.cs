using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlockController : MonoBehaviour
{
    //Number of boids in the flock
    [SerializeField]
    private int flockSize = 20;
    [SerializeField]
    private float speedModifier = 5.0f;
    //Weight modifier for alignment value's contribution to flocking direction
    [SerializeField]
    private float alignmentWeight = 1.0f;
    //Weight modifier for cohesion value's contribution to flocking direction
    [SerializeField]
    private float cohesionWeight = 1.0f;
    [SerializeField]
    private float seperationWeight = 1.0f;
    [SerializeField]
    private float followWeight = 5.0f;

    [Header("Boid Data")]
    [SerializeField]
    Boid boidPrefab;
    [SerializeField]
    private float spawnRadius = 3.0f;
    private Vector3 spawnLocation = Vector3.zero;

    [Header("Target Data")]
    [SerializeField]
    public Transform target;

    //Used to calculate average center of entire flock. Used in calculating cohesion
    private Vector3 flockCenter;
    //Used to calculate the entire flock's direction. Which is then used to calculate alignment
    private Vector3 flockDirection;
    //Used to calculate direction to flocking target
    private Vector3 targetDirection;

    //Seperation value
    private Vector3 seperation;

    public List<Boid> flockList = new List<Boid>();

    public float SpeedModifier { get { return speedModifier; } }

    private void Awake()
    {
        flockList = new List<Boid>(flockSize);
        for(int i=0;i<flockSize;i++)
        {
            //To avoid weird artifacts we try to spawn boids within a radius rather than the same position
            spawnLocation = Random.insideUnitSphere * spawnRadius + transform.position;
            Boid boid = Instantiate(boidPrefab, spawnLocation, transform.rotation) as Boid;

            boid.transform.parent = transform;
            boid.FlockController = this;
            flockList.Add(boid);
        }
    }
    
    public Vector3 Flock(Boid boid, Vector3 boidPosition, Vector3 boidDirection)
    {
        flockDirection = Vector3.zero;
        flockCenter = Vector3.zero;
        targetDirection = Vector3.zero;
        seperation = Vector3.zero;

        for(int i=0;i<flockList.Count;i++)
        {
            Boid neighbor = flockList[i];

            // Check only against neighbors
            if (neighbor == boid) { continue; }

            //Aggregate the direction of all the boids
            flockDirection += neighbor.Direction;
            //Aggregate the position of all boids
            flockCenter += neighbor.transform.localPosition;
            //Aggregate the delta to all boids
            seperation += neighbor.transform.localPosition - boidPosition;
            seperation *= -1;
        }

        //Alignment - Avg direction of all boids
        flockDirection /= flockSize;
        flockDirection = flockDirection.normalized * alignmentWeight;

        //Cohesion - The centroid of the flock
        flockCenter /= flockSize;
        flockCenter = flockCenter.normalized * cohesionWeight;

        //Seperation
        seperation /= flockSize;
        seperation = seperation.normalized * seperationWeight;

        //Direction vector to the target of the flock
        targetDirection = target.localPosition - boidPosition;
        targetDirection = targetDirection * followWeight;
        /*We don't normalize targetDirection.
         Even if we do it won't change the result drastically.
        But if the target starts to move very fast, the boids will
        float away casually.*/

        return flockDirection + flockCenter + seperation + targetDirection;
    }    
}
