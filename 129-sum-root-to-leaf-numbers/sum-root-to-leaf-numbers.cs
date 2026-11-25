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
    public int SumNumbers(TreeNode root) 
    {
        // no more nodes left in the tree
        if(root is null)
        {
            return 0;
        }

        return SumNumbers(root, 0);

        int SumNumbers(TreeNode root, int partialSum)
        {
            if(root is null)
            {
                return 0;
            }

            if(root.right is null && root.left is null)
            {
                return partialSum * 10 + root.val;
            }

            var leftSum = SumNumbers(root.left, partialSum * 10 + root.val);
            var rightSum = SumNumbers(root.right, partialSum * 10 + root.val);

            return leftSum + rightSum;
        }
    }
}