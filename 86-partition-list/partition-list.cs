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
    public ListNode Partition(ListNode head, int x) 
    {
        // create a sentinel node which poits to the head of the sublist with elements less than x and one for larger or equal to x
        ListNode sL = new(int.MinValue), sR = new(int.MaxValue), l = sL, r = sR;
    
        // partitionate list
        while(head != null)
        {
            // get current node
            var next = head.next;
            
            // remove link since it will be made latter
            head.next = null;
            
            // advance right pointer
            if(head.val >= x)
            {
                sR = sR.next = head;
            }
            else
            {
                // advance left pointer
                sL = sL.next = head;
            }
            
            head = next;
        } 
        
        sL.next = r.next;
        return l.next; 
    }
}