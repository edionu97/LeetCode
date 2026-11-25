/**
 * Definition for singly-linked list.
 * public class ListNode {
 *     public int val;
 *     public ListNode next;
 *     public ListNode(int x) {
 *         val = x;
 *         next = null;
 *     }
 * }
 */
public class Solution 
{
    public ListNode? DetectCycle(ListNode head)
    {
        ListNode? slow = head, fast = head;

        // Apply floyd algorithm, using fast and slow approach
        // Idea is that we're trying to traverse the list using two pointers, and if there is a cycle, 
        // the fast pointer will decrease the distance between itself and the slow by one at each cycle turn
        // eventually they will be the same
        while(slow is not null && fast is not null)
        {
            slow = slow.next;
            fast = fast.next?.next;

            // Cycle is detected
            if(slow == fast)
            {
                // Once we found a cycle we start advancing from the head, and is mathematically guaranteed that those two pointers
                // will meet at the starting of the cycle
                while(head != fast)
                {
                    head = head?.next!;
                    fast = fast?.next;
                }

                return head;
            }
        }

        return null;
    }
}