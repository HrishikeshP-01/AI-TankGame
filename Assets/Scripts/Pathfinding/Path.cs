using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Path : MonoBehaviour
{
    [SerializeField]
    private Vector3[] waypoints;

    public bool isDebug = true;
    public float radius = 3.0f;

    // PathLength is a PROPERTY that provides a public getter for the private field - waypoints.Length (waypoints is a private variable)
    public float PathLength
    {
        get { return waypoints.Length; }
    }

    public Vector3 GetWaypoint(int index)
    {
        return waypoints[index];
    }

    private void OnDrawGizmos()
    {
        if (!isDebug) { return; }

        for(int i=0;i<waypoints.Length-1;i++)
        {
            Debug.DrawLine(waypoints[i], waypoints[i + 1], Color.yellow);
        }
    }
}
