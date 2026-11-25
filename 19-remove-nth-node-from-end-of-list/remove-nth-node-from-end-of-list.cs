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
    public ListNode RemoveNthFromEnd(ListNode head, int k)
    {
        Dictionary<int, ListNode> nodeMap = new();
        
        // create a map between nodes and their indexes
        var index = 0;
        for(var h = head; h != null; h = h.next)
        {
            nodeMap[index++] = h;
        }
        
        var toBeRemovedIdx = nodeMap.Count - k;
        
        // first node removal
        if(toBeRemovedIdx == 0)
        {
            return head.next;
        }
        
        nodeMap[toBeRemovedIdx - 1].next = nodeMap[toBeRemovedIdx].next;
        
        return head;
    }
}