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
    public IList<int> RightSideView(TreeNode root) 
    {
        List<int> view = new();
        
        // create a queue in order to travese the tree 
        Queue<NodeInfo> nodes = new();
            
        nodes.Enqueue(new NodeInfo(root, 0));
        while(nodes.Any())
        {
            var (node, level) = nodes.Dequeue();
            
            if(node is null)
            {
                continue;
            }
            
            // ensure we have a list with enough space to hold the view
            if(level + 1 > view.Count)
            {
                view.Add(-1);
            }
            view[level] = node.val;
            
            // move to next
            nodes.Enqueue(new NodeInfo(node.left, level + 1));
            nodes.Enqueue(new NodeInfo(node.right, level + 1));
        }
        
        return view;
    }

    private record NodeInfo(TreeNode Node, int Level);
}