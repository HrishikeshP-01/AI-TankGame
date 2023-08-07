using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlockController : MonoBehaviour
{
    [SerializeField]
    private int flockSize = 20;
    [SerializeField]
    private float speedModifier = 5.0f;
    [SerializeField]
    private float alignmentWeight = 1.0f;
    [SerializeField]
    private float cohesionWeight = 1.0f;
    [SerializeField]
    private float seperationWeight = 1.0f;
    [SerializeField]
    private float followWeight = 5.0f;

    [SerializeField]
    Boid boidPrefab;
    [SerializeField]
    private float spawnRadius = 3.0f;
    private Vector3 spawnLocation = Vector3.zero;

    [SerializeField]
    public Transform target;
    // Start is called before the first frame update

    public float SpeedModifier { get { return speedModifier; } }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public Vector3 Flock(Boid boid, Vector3 boidPosition, Vector3 boidDirection)
    {
        return Vector3.zero;
    }
}
