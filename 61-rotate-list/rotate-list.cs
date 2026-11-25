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
    public ListNode RotateRight(ListNode head, int k) 
    {
        if(head is null || k == 0)
        {
            return head;
        }
        
        // build a map with node indexes
        Dictionary<int, Info> nodeIdx = new();
        for(var idx = 0; head != null; head = head.next, ++idx)
        {
            if(idx == 0)
            {
                nodeIdx[idx] = new Info(head, null);
                continue;
            }
            
            nodeIdx[idx] = new Info(head, nodeIdx[idx - 1].Node);
        }
        
        // get number of indexes
        var n = nodeIdx.Count;
        
        // if multiple => rotate list = initial list
        if(k % n == 0)
        {
           return nodeIdx[0].Node;  
        }
        
        // get the idx of the node which will be first
        var firstNodeIdx = n - k % n;
        
        var (h, prev) = nodeIdx[firstNodeIdx];
        var (last, _) = nodeIdx[n - 1];
        var (first, _) = nodeIdx[0];
        
        prev.next = null;
        last.next = first;
        
        return h;
    }
    
    private record Info(ListNode Node, ListNode Prev);
}