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
internal static class Extensions
{
    // Check if a node is leaf
    public static bool IsLeaf(this TreeNode node)
    {
        _ = node ?? throw new ArgumentNullException(nameof(node), "Not allowed empty nodes");

        return node.left is null && node.right is null;
    }

    // Perform logaritmic eponentiation
    public static int LogPow(this int x, int y)
    {
        // x^0 = 1
        if(y == 0)
        {
            return 1;
        }

        // x^ 1 = x
        if(y == 1)
        {
            return x;
        }

        // if even exponent (x^2)^(y/2), if odd exponent x * ((x^2))^(y/2)
        return y % 2 == 0 ?  (x * x).LogPow(y / 2) :  (x * x).LogPow(y / 2) * x;
    }
}

public class Solution 
{
    public int CountNodes(TreeNode root) 
    {
        // handle empty tree case
        if(root is null)
        {
            return 0;
        }

        // handle leaf case
        if(root.IsLeaf())
        {
            return 1;
        }

        // save the leftest nodes from the inorder traversal
        var nodes = GetPartialDfsStack(root);

        // get the number of nodes from first h - 1 levels
        // those levels are complete => have 2 ^ p nodes => total number 2^(p+1) - 1
        // where p = height - 1 => total up = 2 ^ height - 1
        var upNodes = 2.LogPow(nodes.Count) - 1;

        // count nodes from last level
        int lastLevelNodesNo = 1, levels = nodes.Count;
        while(nodes.Any())
        {
            var (node, level) = nodes.Pop();

            // count leaves
            if(node.IsLeaf() && level == levels)
            {
                ++lastLevelNodesNo;
                continue;
            }

            // get right element of each node
            var right = node.right;
            if(right is null)
            {
                break;
            }

            // push all left nodes in the stack
            for(; right is not null; right = right.left)
            {
                nodes.Push(new NodeInfo(right, ++level));
            }
        }

        return lastLevelNodesNo + upNodes;

        Stack<NodeInfo> GetPartialDfsStack(TreeNode root)
        {
            Stack<NodeInfo> nodes = new();

            // always go in left directions
            var level = 0;
            for(; root is not null; root = root = root.left)
            {
                // do not add leaf nodes
                if(root.IsLeaf())
                {
                    break;
                }

                nodes.Push(new NodeInfo(root, level++));
            }

            return nodes;
        }
    }

    private record NodeInfo(TreeNode Node, int level);
}