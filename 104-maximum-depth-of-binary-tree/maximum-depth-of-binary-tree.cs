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
    public int MaxDepth(TreeNode root)
    { 
        return DoDfs(root, 0);
        
        int DoDfs(TreeNode root, int level)
        {
            if(root == null)
            {
                return level;
            }
    
            var st = DoDfs(root.left, level + 1);
            
            var dr = DoDfs(root.right, level + 1);
            
            return Math.Max(st, dr);
        }
    }
}