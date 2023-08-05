using System.Collections;

public class PriorityQueue
{
    private ArrayList nodes = new ArrayList();

    public int Length
    {
        get { return nodes.Count; }
    }

    public bool Contains(object node)
    {
        return nodes.Contains(node);
    }

    public Node GetFirstNode()
    {
        if (nodes.Count > 0) { return (Node)nodes[0]; }
        return null;
    }

    public void Push(Node node)
    {
        nodes.Add(node);

        nodes.Sort();
        /* How does sort work?
         The Sort() fn relies on the implementation of the CompareTo fn in Node class.
        Therefore, in this case it will sort according to node's hCost*/
    }

    public void Remove(Node node)
    {
        nodes.Remove(node);

        nodes.Sort();
        /* How does sort work?
         The Sort() fn relies on the implementation of the CompareTo fn in Node class.
        Therefore, in this case it will sort according to node's hCost*/
    }
}