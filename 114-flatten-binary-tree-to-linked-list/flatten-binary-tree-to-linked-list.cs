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
public class Solution {
    public void Flatten(TreeNode root) 
    {
        _ = PreorderFlatten(root);

        TreeNode PreorderFlatten(TreeNode root)
        {
            // root is leaf or root does not exists
            if(root?.left is null && root?.right is null)
            {
                return root;
            }

            // flat both subtrees (left and right)
            var leftN = PreorderFlatten(root.left);
            var rightN = PreorderFlatten(root.right);

            if(leftN is not null)
            {
                leftN.right = root.right;
                root.right = root.left;
                root.left = null;
            } 

            return rightN is not null ? rightN : leftN;          
        }
    }
}