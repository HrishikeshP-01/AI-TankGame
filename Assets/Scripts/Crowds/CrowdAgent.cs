using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CrowdAgent : MonoBehaviour
{
    private NavMeshAgent navMeshAgent;
    [SerializeField]
    private Transform targetTransform;

    // Start is called before the first frame update
    void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        navMeshAgent.speed = Random.Range(4.0f, 5.0f);
        navMeshAgent.SetDestination(targetTransform.position);
    }
}
