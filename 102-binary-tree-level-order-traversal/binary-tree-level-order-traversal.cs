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
    public IList<IList<int>> LevelOrder(TreeNode root) 
    {
        // create a map for keeping all nodes from each level
        Dictionary<int, IList<int>> levelNodes = new();
        
        // create the queue
        Queue<NodeInfo> queue = new();
        queue.Enqueue(new NodeInfo(root, 0));
        
        // perorm bfs
        while(queue.Any())
        {
            var (node, level) = queue.Dequeue();
            
            // stop if no valid node
            if(node is null)
            {
                continue;
            }
            
            // ensure we have always a key in dictionary
            if(!levelNodes.ContainsKey(level))
            {
                levelNodes[level] = new List<int>();
            }
            
            // add current node in its level data
            levelNodes[level].Add(node.val);
            
            queue.Enqueue(new NodeInfo(node.left, level + 1));
            queue.Enqueue(new NodeInfo(node.right, level + 1));
        }
        
        return levelNodes.Select(x => x.Value).ToList();
    }
    
    private record NodeInfo(TreeNode Node, int Level);
}

