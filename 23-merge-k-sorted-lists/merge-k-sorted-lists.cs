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
    // Total complexity O(K * logN)
    public ListNode MergeKLists(ListNode[] lists) 
    {   
        // create a mapping between starting list node and 
        var idxToNodeMap = lists
            .Select((lst, i) => new { Index = i, Node = lst })
            .ToDictionary(x => x.Index, x => x.Node);
        
        return MergeKLists(0, lists.Length - 1); 
        
        // Merge K sorted list in O(log2(M)) where M = len(lists)
        ListNode MergeKLists(int li, int ls)
        {
            // when we are out of bounds return null
            if(li < 0 || ls >= lists.Length || li > ls)
            {
                return null;
            }

            // when left and right are equal => return the pointer to the right
            if(li == ls)
            {
                return idxToNodeMap[li];
            }

            // merge two lists 
            if(ls - li == 1)
            {
                return MergeTwoSordedList(idxToNodeMap[li], idxToNodeMap[ls]);
            }

            // get the middle
            var m = (li + ls) >> 1;

            // merge left and right sides
            var left = MergeKLists(li, m);
            var right = MergeKLists(m + 1, ls);

            // merge two parts togheter
            idxToNodeMap[li] = MergeTwoSordedList(left, right);

            // return the sorted list
            return idxToNodeMap[li];
        }
    }

    // Merge two lists in O(N)
    private static ListNode MergeTwoSordedList(ListNode left, ListNode right)
    {
        // if left is null => return right
        if(left is null)
        {
            return right;
        }

        // if right is null => return left
        if(right is null)
        {
            return left;
        }

        // put the lowest value in left
        if(left.val <= right.val)
        {
            left.next = MergeTwoSordedList(left.next, right);
            return left;
        }

        // put the rightest value in right
        right.next = MergeTwoSordedList(left, right.next);
        return right;
    }
}