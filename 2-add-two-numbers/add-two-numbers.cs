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
    public ListNode AddTwoNumbers(ListNode l1, ListNode l2)
    {
        ListNode h1 = l1, h2 = l2, hr = new();
        
        // as long as we have elements in two lists add corresponding elements 
        var reminder = 0;
        
        ListNode current = hr;
        for(; h1 != null && h2 != null; h1 = h1.next, h2 = h2.next)
        {
            // compute sum of figures
            var val = h1.val + h2.val + reminder;
            
            // keep the rest and set the value of current node
            reminder = val / 10;
            current.val = val % 10;
            
            // do not create new node if next iteration we're exiting the loop
            if(h1.next == null || h2.next == null)
            {
                continue;
            }
            
            // advance with next figures
            var next = new ListNode();
            current.next = next;
            current = next;
        }
        
        // check for remainig items in l1
        for(; h1 != null; h1 = h1.next)
        {
            var val = h1.val + reminder;
            
            reminder = val / 10;
            
            current.next = new ListNode(val % 10);
            
            current = current.next;
        }
        
        //check for remining items in l2
        for(; h2 != null; h2 = h2.next)
        {
            var val = h2.val + reminder;
            
            reminder = val / 10;
            current.next = new ListNode(val % 10);
            
            current = current.next;
        }
        
        if(reminder == 1)
        {
            current.next = new ListNode(1);
        }
        
        return hr;
    }
}