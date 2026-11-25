/**
 * Definition for a binary tree node.
 * public class TreeNode {
 *     public int val;
 *     public TreeNode left;
 *     public TreeNode right;
 *     public TreeNode(int x) { val = x; }
 * }
 */
public class Solution 
{
    public TreeNode? LowestCommonAncestor(TreeNode root, TreeNode p, TreeNode q)
    {
        Dictionary<int, Metadata> lcaMetadata = [];

        Dfs(root, 0, null);

        while (p != q)
        {
            Metadata pData = lcaMetadata[p.val];
            Metadata qData = lcaMetadata[q.val];

            if(pData.Depth > qData.Depth)
            {
                p = pData.Parent!;
                continue;
            }

            if(qData.Depth > pData.Depth)
            {
                q = qData.Parent!;
                continue;
            }

            if (pData.Depth == qData.Depth)
            {
                p = pData.Parent!;
                q = qData.Parent!;
            } 
        }

        return p;

        void Dfs(TreeNode? root, int depth, TreeNode? parent)
        {
            // Handle null case
            if (root is null)
            {
                return;
            }
        
            lcaMetadata[root.val] = new Metadata(root, parent, depth);

            // Do dfs for both subtrees
            Dfs(root.left, depth + 1, root);
            Dfs(root.right, depth + 1, root);
        }
    }

    private record Metadata(TreeNode Node, TreeNode? Parent, int Depth);
}