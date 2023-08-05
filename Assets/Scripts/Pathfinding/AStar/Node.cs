using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Node : IComparable
{
    // Total cost so far for the node 
    public float gCost;
    // Estimated cost from this node to target node
    public float hCost;
    // Is this node an obstacle
    public bool bObstacle;
    // Parent of the Node in the linked list
    public Node parent;
    // Position of the node in world space
    public Vector3 position;

    public Node()
    {
        hCost = 0.0f;
        gCost = 1.0f;
        bObstacle = false;
        parent = null;
    }

    public Node(Vector3 pos)
    {
        hCost = 0.0f;
        gCost = 1.0f;
        bObstacle = false;
        parent = null;
        position = pos;
    }

    public void MarkAsObstacle() { bObstacle = true; }

    // IComparable interface implementation
    public int CompareTo(object obj)
    {
        Node node = (Node)obj;

        if (hCost < node.hCost) { return -1; }
        else if (hCost > node.hCost) { return 1; }
        return 0;
    }

    /* Node class implements IComparable interface
     This requires us to implement CompareTo() fn to satisfy the interface contract.
    An interface is like a contract. By inheriting an interface we are agreeing to implement all the
    methods with the provided signatures in the implementing class.
    Therefore, when another class calls any method from an interface in this class, it's guarenteed that such a method exists.*/
}
