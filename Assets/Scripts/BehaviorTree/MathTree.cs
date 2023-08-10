using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Test class for behavior tree nodes that were created earlier

public class MathTree : MonoBehaviour
{
    public Color m_evaluating;
    public Color m_succeeded;
    public Color m_failed;

    // Nodes at Tier 1
    public Selector m_RootNode;

    // Nodes at Tier 2
    public ActionNode m_Node2a;
    public Invertor m_Node2b;
    public ActionNode m_Node2c;

    // Nodes at Tier 3
    public ActionNode m_Node3;

    // GameObjects for visualizing the nodes
    public GameObject ob_RootNode;
    public GameObject ob_Node2a;
    public GameObject ob_Node2b;
    public GameObject ob_Node2c;
    public GameObject ob_Node3;

    public int targetValue = 20;
    private int currentValue = 0;

    [SerializeField]
    private TextMesh m_valueLabel;

    // Function to check if current value is equal to target value, this is going to be run by action node
    private NodeStates NotEqualToTarget()
    {
        if (currentValue != targetValue) { return NodeStates.SUCCESS; }

        return NodeStates.FAILURE;
    }
    
    // Fn to add 1 to current value. This is going to be run by action node
    private NodeStates AddOne()
    {
        currentValue += 1;
        m_valueLabel.text = currentValue.ToString();

        if (currentValue == targetValue) { return NodeStates.SUCCESS; }

        return NodeStates.FAILURE;
    }

    // Instantiate nodes from bottom up & assign children in that order
    private void Start()
    {
        // Tier 3
        // Deepest level node is node 3 & has no children
        m_Node3 = new ActionNode(NotEqualToTarget);

        // Tier 2
        m_Node2a = new ActionNode(AddOne);
        m_Node2b = new Invertor(m_Node3);
        m_Node2c = new ActionNode(AddOne);

        // Tier 1
        List<BT_Node> rootChildren = new List<BT_Node>();
        rootChildren.Add(m_Node2a);
        rootChildren.Add(m_Node2b);
        rootChildren.Add(m_Node2c);

        m_RootNode = new Selector(rootChildren);

        m_valueLabel.text = currentValue.ToString();

        m_RootNode.Evaluate();
        UpdateBoxes();
    }

    private void UpdateBoxes()
    {
        SetColorToBox(ob_RootNode, m_RootNode.NodeState);
        SetColorToBox(ob_Node2a, m_Node2a.NodeState);
        SetColorToBox(ob_Node2b, m_Node2b.NodeState);
        SetColorToBox(ob_Node2c, m_Node2c.NodeState);
        SetColorToBox(ob_Node3, m_Node3.NodeState);
    }

    private void SetColorToBox(GameObject box, NodeStates state)
    {
        switch(state)
        {
            case NodeStates.FAILURE:
                box.GetComponent<Renderer>().material.color = m_failed;
                break;
            case NodeStates.RUNNING:
                box.GetComponent<Renderer>().material.color = m_evaluating;
                break;
            case NodeStates.SUCCESS:
                box.GetComponent<Renderer>().material.color = m_succeeded;
                break;
        }
    }
}
