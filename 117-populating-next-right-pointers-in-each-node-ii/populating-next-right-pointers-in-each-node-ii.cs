/*
// Definition for a Node.
public class Node {
    public int val;
    public Node left;
    public Node right;
    public Node next;

    public Node() {}

    public Node(int _val) {
        val = _val;
    }

    public Node(int _val, Node _left, Node _right, Node _next) {
        val = _val;
        left = _left;
        right = _right;
        next = _next;
    }
}
*/

public class Solution 
{
    public Node Connect(Node root) 
    {
        // initially link all the sibling nodes
        LinkSiblings(root);

        ConnectSameLevel(root, null);
        return root;
    }

    // Connects all nodes which have the same parent
    private static void LinkSiblings(Node root)
    {
        // no more nodes left
        if(root is null)
        {
            return;
        }

        // get left and right nodes
        Node left = root.left, right = root.right;

        // link left and right via next pointer
        if(left is not null)
        {
            left.next = right;
        }

        // link the other nodes
        LinkSiblings(left);
        LinkSiblings(right);
    }

    private static void ConnectSameLevel(Node left, Node parrent)
    {
        // handle null case
        // in this scenario we've finished a level, but not all levels
        if(left is null)
        {
            // if we have no left nodes it does not mean we're are on the end level
            // search the next possible leftest node by looking at the parrents
            left = parrent?.right;
            while(left is null && parrent is not null)
            {   
                parrent = parrent?.next;
                left = parrent?.left ?? parrent?.right;
            }
            
            // there is another level
            if(left is not null)
            {
                ConnectSameLevel(left, parrent);
            }
            return;
        }

        // cannot make connections via non existing parent
        if(parrent is null)
        {
            ConnectSameLevel(left.left, left);
            return;
        }

        // try to compute next of all the elements from the same line
        // use next pointer to navigate between parrents 
        for(var lineN = left; lineN != null; lineN = lineN.next)
        {
            // find leftest node on the same level
            while(lineN.next is null && parrent is not null)
            {
                parrent = parrent.next;
                lineN.next = parrent?.left ?? parrent?.right;
            }
        }

        ConnectSameLevel(left.left, left);
    }
}