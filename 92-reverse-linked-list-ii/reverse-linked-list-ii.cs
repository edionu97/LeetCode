/**
 * Definition for singly-linked list.
 * public class ListNode {
 *     public int val;
 *     public ListNode next;
 *     public ListNode(int val=0, ListNode next=null) {
 *         this.val = val;
 *         this.next = next;
 *     }
 * }
 */
public class Solution 
{
    public ListNode ReverseBetween(ListNode head, int left, int right) 
    {
        if(head is null)
        {
            return null;
        }
        
        // get the left before left
        var beforeLeftNode = GetNodeFromPosition(left - 1, head);
        
        // get the nodes we need to reverse (between left and right)
        // when beforeLeft is null => use head
        var toBeReversed = GetNodesToReverse(beforeLeftNode?.next ?? head, left, right);
        
        // reverse nodes between left and right
        var rNode = toBeReversed.Peek();
       
        var newRNode = Reverse(toBeReversed);
        
        // if we also reversed also the head node => we have a new head
        if(beforeLeftNode is null)
        {
            return rNode;
        }
        
        beforeLeftNode.next = rNode;
        return head;
    }
    
    private static ListNode Reverse(Stack<ListNode> reversable)
    {
        ListNode rNode = reversable.Pop(), afterRNode = rNode.next;
        
        while(reversable.Any())
        {
            //get the node in left of right nodr
            var leftRNode = reversable.Pop();

            // l -> r => r -> l
            rNode = rNode.next = leftRNode;  
        }

        rNode.next = afterRNode;
        
        return rNode;
    }

    private static Stack<ListNode> GetNodesToReverse(ListNode nodeLeft, int left, int right)
    {
        Stack<ListNode> reversable = new();
        
        while(left++ <= right && nodeLeft is not null)
        {
            reversable.Push(nodeLeft);
            nodeLeft = nodeLeft.next;
        }
        
        return reversable;
    }
    
    // Get the node from p position or null if does not exist
    private static ListNode GetNodeFromPosition(int p, ListNode head)
    {
        // handle the null case
        if(head is null)
        {
            return null;
        }
        
        if(p <= 0)
        {
            return null;
        }
        
        // as long as we need to move pointer right advance
        while(--p > 0 && head is not null)
        {
            head = head.next;
        }
        
        // node from position p
        return head;
    }
}