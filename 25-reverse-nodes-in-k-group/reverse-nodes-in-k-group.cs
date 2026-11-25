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
    public ListNode ReverseKGroup(ListNode head, int k) 
    {
        if(k == 1)
        {
            return head;
        }
        
        ListNode newH = null, last = null;
        for(; head != null; last = head, head = head.next)
        {
            var (_, h) = ReverseFirstKFromList(head, k);
           
            // there is no list with k elements left
            if(h is null)
            {
                break;
            }
            
            // save the value of the new reversed list
            if(newH is null)
            {
                newH = h;
            }
             
            // make connection between lists
            if(last is not null)
            {
                last.next = h;
            }
        }
        
        return newH;
    }
    
    private Info ReverseFirstKFromList(ListNode current, int k)
    {
        // reached element
        if(k == 1)
        {
            // head of the reversed sublist will be current
            return new Info(current, current);
        }
        
        // reached end and we do not have a new sublist
        if(current is null)
        {
            return new Info(null, null);
        }
        
        // get the upper list
        var (up, h) = ReverseFirstKFromList(current.next, k - 1);
        // reverse links
        if(h is not null)
        {
            // reverse links
            current.next = up.next;
            up.next = current;
        }
        
        // reverse further
        return new Info(current, h);
    }
    
    private record Info(ListNode Up, ListNode Head);
}