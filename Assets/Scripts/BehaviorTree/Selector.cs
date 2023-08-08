using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Selector : BT_Node
{
    // Child nodes for this selector
    protected List<BT_Node> m_nodes = new List<BT_Node>();

    // Constructor that requires a list of nodes
    public Selector(List<BT_Node> childNodes)
    {
        m_nodes = childNodes;
    }

    /* If any child nodes of selector returns true it immediately returns true else returns false if all child nodes return false.
     If a child node is running the selector also runs until a value is returned by child node*/
    public override NodeStates Evaluate()
    {
        foreach(BT_Node node in m_nodes)
        {
            switch(node.Evaluate())
            {
                case NodeStates.FAILURE:
                    continue;
                case NodeStates.SUCCESS:
                    nodeState = NodeStates.SUCCESS;
                    return nodeState;
                case NodeStates.RUNNING:
                    nodeState = NodeStates.RUNNING;
                    return nodeState;
                default:
                    continue;
            }
        }
        nodeState = NodeStates.FAILURE;
        return nodeState;
    }
}
