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
    public int MaxPathSum(TreeNode root) 
    {
        // impossible case but worth checking
        if(root is null)
        {
            return 0;
        }

        // get the maximum sums of each category
        var (nodeMax, subtree, left, right) = ComputeMaxPathSum(root);
        
        // get the max value between all categories
        return Math.Max(Math.Max(left, right), Math.Max(subtree, nodeMax));

        SumPathMax ComputeMaxPathSum(TreeNode root)
        {
            // non existing root
            if(root is null)
            {
                return new SumPathMax(int.MinValue, int.MinValue, int.MinValue, int.MinValue);
            }

            // when root is leaf or does not exist => max sum for each category is the node value itself  
            if(root.left is null && root.right is null)
            {
                var v = root.val;

                // max value is the node itself
                return new SumPathMax(v, v, v, v);
            }

            // compute the maximum sum per each category from both left and right subtrees
            var (nodeMaxL, subtreeMaxL, leftMaxL, rightMaxL) = ComputeMaxPathSum(root.left);
            var (nodeMaxR, subtreeMaxR, leftMaxR, rightMaxR) = ComputeMaxPathSum(root.right);
            
            // the value of the root's left sum can be either it's left node or the maximum sum in the left subtree
            var leftMax = Math.Max(root.left?.val ?? 0, Math.Max(leftMaxL, rightMaxL));

            // the value of the root's right sum can be either it's right node or the maximum sum in the right subtree
            var rightMax = Math.Max(root.right?.val ?? 0, Math.Max(leftMaxR, rightMaxR));

            // max node can either be one of left, right or the current
            var nodeMax = Math.Max(root.val, Math.Max(nodeMaxL, nodeMaxR));

            // compute the subtree max
            var subtreeMax = Math.Max(
                
                // max between existing subtrees
                Math.Max(   
                    // it's either maximum between any left and right subtrees
                    Math.Max(subtreeMaxL, subtreeMaxR),
                    // or can be either left subtree or right subtree
                    Math.Max(Math.Max(leftMaxL, rightMaxL), Math.Max(leftMaxR, rightMaxR))),

                // or can be either root + left + right or root + maxL + maxR
                Math.Max(root.val + root.left?.val ?? 0 + root.right?.val ?? 0, root.val + rightMax + leftMax));
            
            // return the computed sum
            return new SumPathMax(
                nodeMax,
                subtreeMax,
                root.val + leftMax,
                root.val + rightMax);
        }
    }

    // Define this structure to hold information about the sum path for a specific node
    // SumPath.Node => maximum sum formed from a single node value
    // SumPath.Subtree => maximum sum formed from a combination from left and right nodes having a common root
    // SumPath.Left => maximum sum formed by current node and its left subtree
    // SumPath.Right = maximum sum formed by current node and its right subtree
    private record SumPathMax(int Node, int Subtree, int Left, int Right);
}