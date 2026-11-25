/**
 * Definition for a binary tree node.
 * public class TreeNode {
 *     public int val;
 *     public TreeNode left;
 *     public TreeNode right;
 *     public TreeNode(int val=0, TreeNode left=null, TreeNode right=null) {
 *         this.val = val;
 *         this.left = left;
 *         this.right = right;
 *     }
 * }
 */
public class Solution 
{
    public int KthSmallest(TreeNode root, int k) 
    {
        // keep a map with all the nodes idxes from the tree
        Dictionary<int, TreeNode> nodes = new();
        
        GetNodeIndexInOrderedList(root);

        return nodes[--k].val;

        void GetNodeIndexInOrderedList(TreeNode node)
        {
            // not take in consideration null nodes
            if(node is null)
            {
                return;
            }

            // visit first left
            GetNodeIndexInOrderedList(node.left);
            
            // current node is on position i
            nodes[nodes.Count] = node;

            // visit left
            GetNodeIndexInOrderedList(node.right);
        }
    }

}