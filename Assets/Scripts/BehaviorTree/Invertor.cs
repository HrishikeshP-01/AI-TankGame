using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Invertor : BT_Node
{
    // Child node to evaluate
    BT_Node m_node;

    // Constructor requires child node
    public Invertor(BT_Node childNode) { m_node = childNode; }

    // Getter fn that returns child node
    public BT_Node node { get { return m_node; } }

    /* If node returns success decorator returns fail. If node returns fail decorator returns success. If node is running decorator returns running*/
    public override NodeStates Evaluate()
    {
        switch(node.Evaluate())
        {
            case NodeStates.FAILURE:
                nodeState = NodeStates.SUCCESS;
                break;
            case NodeStates.SUCCESS:
                nodeState = NodeStates.FAILURE;
                break;
            case NodeStates.RUNNING:
                nodeState = NodeStates.RUNNING;
                break;
            default:
                nodeState = NodeStates.RUNNING;
                break;
        }
        return nodeState;
    }
}