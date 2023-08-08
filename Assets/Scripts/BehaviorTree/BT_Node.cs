using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public abstract class BT_Node
{
    //Delegate that returns the state of the node
    public delegate NodeStates NodeReturn();

    //Current state of the node
    protected NodeStates nodeState;

    // Getter fn for value of nodeState
    public NodeStates NodeState { get { return nodeState; } }

    //Constructor for Node
    BT_Node() { }

    //Implementing classes use this method to evaluate the desired set of conditions
    public abstract NodeStates Evaluate();
}
