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
    public ListNode DeleteDuplicates(ListNode head) 
    {
        // empty or list with one element case
        if(head is null || head.next is null)
        {
            return head;
        }

        // get first distinct node for head
        head = GetFirstNodeDistinct(head);
        
        // remove elements
        for(ListNode h = head?.next, prev = head; h != null; prev = h, h = h?.next)
        {
            h = GetFirstNodeDistinct(h);

            // make link p -> h
            prev.next = h;
        }
        
        return head;
    }
    
    private ListNode GetFirstNodeDistinct(ListNode node)
    {
        if(node is null)
        {
            return null;
        }
        
        if(node.val != node.next?.val)
        {
            return node;
        }
        
        // as long we have duplicates, advance
        var next = node.next;
        while(next is not null && node.val == next.val)
        {
            next = next.next;
        }
        
        return GetFirstNodeDistinct(next);
    }
}