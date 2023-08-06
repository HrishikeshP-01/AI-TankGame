using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AStar
{
    public static PriorityQueue openList;
    public static PriorityQueue closedList;

    public static ArrayList CalculatePath(Node node)
    {
        ArrayList path = new ArrayList();
        while(node!=null)
        {
            path.Add(node);
            node = node.parent;
        }
        path.Reverse();
        return path;
    }

    // Calculate estimated Heuristic cost to goal
    public static float EstimatedHeuristicCost(Node curNode, Node goal)
    {
        Vector3 vecCost = goal.position - curNode.position;
        return vecCost.magnitude;
    }

    // Find path from start node to goal using A* algorithm
    public static ArrayList FindPath(Node start, Node goal)
    {
        openList = new PriorityQueue();
        start.gCost = 0.0f;
        start.hCost = EstimatedHeuristicCost(start, goal);
        openList.Push(start);

        closedList = new PriorityQueue();
        Node node = null;

        GridManager gridManager = GameObject.FindObjectOfType<GridManager>();
        if (gridManager == null) { return null; }

        while(openList.Length != 0)
        {
            node = openList.GetFirstNode();

            // Check if we are at the goal, if yes calculate path
            if(node.position==goal.position)
            {
                return CalculatePath(node);
            }

            // Get all neighbors
            ArrayList neighbors = new ArrayList();
            gridManager.GetNeighbors(node, neighbors);
            // Update cost of each neighbor node
            for(int i=0;i<neighbors.Count;i++)
            {
                Node neighborNode = (Node)neighbors[i];
                if(!closedList.Contains(neighborNode))
                {
                    // Cost from node to neighbor node
                    float cost = EstimatedHeuristicCost(node, neighborNode);
                    // Total cost from start to this neighbor node
                    neighborNode.gCost = node.gCost + cost;
                    // Estimated cost for neighbor to goal
                    neighborNode.hCost = EstimatedHeuristicCost(neighborNode, goal);
                    // Assign parent to neighbor node
                    neighborNode.parent = node;

                    if (!openList.Contains(neighborNode)) { openList.Push(neighborNode); }

                    // REVISIT - what if neighborNode is aldready in openList but now the cost is lesser ?
                }
            }

            closedList.Push(node);
            openList.Remove(node);
        }

        if (node.position != goal.position) 
        { 
            Debug.LogError("No valid path from target to goal");
            return null;
        }

        // Else path exists. The final node is the goal so we need to calculate path now
        return CalculatePath(node);
    }
}