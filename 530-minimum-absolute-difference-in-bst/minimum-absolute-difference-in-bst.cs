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
    public int GetMinimumDifference(TreeNode root) 
    {
        Queue<TreeNode> q = new();
        q.Enqueue(root);

        List<int> values = new();
        while(q.Any())
        {
            var t = q.Dequeue();
            if(t is null)
            {
                continue;
            }

            values.Add(t.val);
            q.Enqueue(t.left);
            q.Enqueue(t.right);
        }

        var ordered = values.OrderBy(x => x).ToList();

        var min = int.MaxValue;
        for(var i = 1; i < ordered.Count; ++i)
        {
            min = Math.Min(ordered[i] - ordered[i - 1], min);
        }

        return min;
    }
}