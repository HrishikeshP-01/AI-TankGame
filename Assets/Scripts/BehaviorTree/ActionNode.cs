using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*Action node implemented has a degree of flexibility & restriction
 Flexibility - We create a delegate which is like a pointer to a function that can be assigned during runtime
 Restriction - We have only implemented a single delegate that doesn't take any arguments */

[System.Serializable]
public class ActionNode : BT_Node
{
    // Method signature for the action
    public delegate NodeStates ActionNodeDelegate();

    // Delegate that's called to evaluate this node
    private ActionNodeDelegate m_action;

    /*This node doesn't contain logic. The logic is passed in as delegate. As signature states, action needs to return a NodeState enum.
     A delegate is basically a pointer to a function.*/
    public ActionNode(ActionNodeDelegate action) { m_action = action; }

    // Evaluates the node accouding the the delegate that was passed into m_action
    public override NodeStates Evaluate()
    {
        switch(m_action())
        {
            case NodeStates.FAILURE:
                nodeState = NodeStates.FAILURE;
                break;
            case NodeStates.RUNNING:
                nodeState = NodeStates.RUNNING;
                break;
            case NodeStates.SUCCESS:
                nodeState = NodeStates.SUCCESS;
                break;
            default:
                nodeState = NodeStates.FAILURE;
                break;
        }
        return nodeState;
    }
}