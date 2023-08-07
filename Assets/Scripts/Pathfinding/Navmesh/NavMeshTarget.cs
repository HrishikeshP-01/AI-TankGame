using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NavMeshTarget : MonoBehaviour
{
    private NavMeshAgent[] navmeshAgents;
    [SerializeField]
    private Transform targetMarkerTransform;

    // Start is called before the first frame update
    private void Start()
    {
        navmeshAgents = FindObjectsOfType(typeof(NavMeshAgent)) as NavMeshAgent[];
    }

    private void UpdateTargets(Vector3 targetPosition)
    {
        foreach(NavMeshAgent agent in navmeshAgents)
        {
            agent.destination = targetPosition;
        }
    }

    bool GetInput()
    {
        if (Input.GetMouseButtonDown(0)) { return true; }
        return false;
    }

    // Update is called once per frame
    void Update()
    {
        if (!GetInput()) { return; }

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hitInfo;
        if(Physics.Raycast(ray.origin,ray.direction,out hitInfo))
        {
            Vector3 targetPosition = hitInfo.point;
            UpdateTargets(targetPosition);
            targetPosition.y += 0.8f;
            targetMarkerTransform.position = targetPosition;
        }
    }

    private void OnDrawGizmos()
    {
        Debug.DrawLine(targetMarkerTransform.position, targetMarkerTransform.position + Vector3.up * 5f, Color.red);
    }
}
