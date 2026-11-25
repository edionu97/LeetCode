#nullable  enable

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
    public ListNode? SwapPairs(ListNode? head)
    {
        // Handle the case we have an empty list or a list with a single element
        if (head?.next is null)
        {
            return head;
        }

        // Introduce a sentinel node to simplify the swapping logic, especially for the head of the list.
        ListNode prev = new(-1, head);

        // Apply internal swap
        SwapPairsInternal(prev);

        // Return the result, ignoring the sentinel we've added
        return prev.next;

        /*
        * Helper function to swap the nodes in pairs.
        * It takes a previous node as input and swaps the next two nodes after it.
        */
        static void SwapPairsInternal(ListNode prev)
        {
            // [prev] -> [current] -> [mid] -> [next]
            for (ListNode current = prev.next; current?.next is not null; prev = current, current = current.next)
            {
                // Get the nodes to swap
                ListNode mid = current.next;
                ListNode next = mid.next;

                // Update the links to swap the nodes
                prev.next = mid;
                mid.next = current;
                current.next = next;
            }
        }
    }
}