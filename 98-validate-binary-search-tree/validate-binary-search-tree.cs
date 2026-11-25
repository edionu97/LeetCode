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
    public bool IsValidBST(TreeNode root) 
    {
        Dictionary<int, int> nodeMap = new();

        TraverseBst(root);

        for(var idx = 1; idx < nodeMap.Count; ++idx)
        {
            if(nodeMap[idx - 1] >= nodeMap[idx])
            {
                return false;
            }
        }

        return true;

        void TraverseBst(TreeNode root)
        {
            if(root is null)
            {
                return;
            }

            TraverseBst(root.left);
            nodeMap[nodeMap.Count] = root.val;
            TraverseBst(root.right);
        }
    }

    private record MinMaxValue(int MinL, int MaxR);
}