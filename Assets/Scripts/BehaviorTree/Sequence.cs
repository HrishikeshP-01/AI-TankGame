using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Sequence : BT_Node
{
    // List of child nodes
    protected List<BT_Node> nodes = new List<BT_Node>();

    // Constructor that requires initial set of children
    public Sequence(List<BT_Node> childNodes) { nodes = childNodes; }

    // If any child returns false the entire node fails. It returns true only if all children return true
    public override NodeStates Evaluate()
    {
        bool isChildRunning = false;

        foreach(BT_Node node in nodes)
        {
            switch(node.Evaluate())
            {
                case NodeStates.FAILURE:
                    nodeState = NodeStates.FAILURE;
                    return nodeState;
                case NodeStates.SUCCESS:
                    continue;
                case NodeStates.RUNNING:
                    isChildRunning = true;
                    continue;
                default:
                    nodeState = NodeStates.SUCCESS;
                    continue;
            }
        }

        nodeState = (isChildRunning) ? NodeStates.RUNNING : NodeStates.SUCCESS;
        return nodeState;
    }
}