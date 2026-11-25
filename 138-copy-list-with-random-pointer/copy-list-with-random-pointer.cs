/*
// Definition for a Node.
public class Node {
    public int val;
    public Node next;
    public Node random;
    
    public Node(int _val) {
        val = _val;
        next = null;
        random = null;
    }
}
*/

public class Solution 
{
    public Node CopyRandomList(Node head) 
    {
        if(head == null)
        {
            return null;
        }
        
        // create a node map, to use it latter for building a list with a similar pattern
        Dictionary<Node, Node> nodeMap = new();

        // iterate nodes
        for(var h = head; h != null; h = h.next)
        {
            // get the reference for the new node
            var newH = GetNewNodeRef(h);

            // set the next of the newH
            newH.next = GetNewNodeRef(h.next);

            // set the random of the newH
            newH.random = GetNewNodeRef(h.random);
        }

        return nodeMap[head];
        
        Node GetNewNodeRef(Node h)
        {
            // null does not require a deep copy
            if(h == null)
            {
                return null;
            }

            // if the node was already added in the map, get the refence
            if(nodeMap.ContainsKey(h))
            {
                return nodeMap[h];
            }

            // create a new node with the same value
            nodeMap[h] = new Node(h.val);
            return nodeMap[h];
        }
    }
}