using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestingAStar : MonoBehaviour
{
    private Vector3 startPosition;
    private Vector3 endPosition;

    public Node startNode { get; set; }
    public Node endNode { get; set; }

    private ArrayList pathArray;

    private GameObject StartObj;
    private GameObject GoalObj;
    private float elapsedTime = 0.0f;
    private float intervalTime = 1.0f;
    private GridManager gridManager;

    private void Start()
    {
        gridManager = GameObject.FindObjectOfType<GridManager>();
        StartObj = GameObject.FindGameObjectWithTag("Start");
        GoalObj = GameObject.FindGameObjectWithTag("Goal");

        // Calculate path using A* code
        pathArray = new ArrayList();
        FindPath();
    }

    private void Update()
    {
        elapsedTime += Time.deltaTime;

        if(elapsedTime>intervalTime)
        {
            FindPath();
            elapsedTime = 0.0f;
        }
    }

    private void FindPath()
    {
        startPosition = StartObj.transform.position;
        endPosition = GoalObj.transform.position;

        startNode = new Node(gridManager.GetGridCellCenter(gridManager.GetGridIndex(startPosition)));
        endNode = new Node(gridManager.GetGridCellCenter(gridManager.GetGridIndex(endPosition)));

        pathArray = AStar.FindPath(startNode, endNode);
    }

    private void OnDrawGizmos()
    {
        if (pathArray == null) { return; }

        for (int i = 0; i < pathArray.Count - 1; i++)
        {
            Node currentNode = (Node)pathArray[i];
            Node nextNode = (Node)pathArray[i + 1];
            Debug.DrawLine(currentNode.position, nextNode.position, Color.green);
        }
    }
}
