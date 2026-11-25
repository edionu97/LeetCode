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
    public IList<double> AverageOfLevels(TreeNode root) 
    {
        Dictionary<int, LevelInfo> levelData = new();
        
        // create a queue for looping through nodes
        Queue<NodeInfo> queue = new();
        
        queue.Enqueue(new NodeInfo(root, 0));
        
        while(queue.Any())
        {
            var(node, level) = queue.Dequeue();
            
            // parent was a leaf so jump over
            if(node is null)
            {
                continue;
            }
            
            // ensure there is always a key in level info
        if(!levelData.ContainsKey(level))
            {
                levelData[level] = new LevelInfo(0,0);
            }
            
            // update level info
            var(no, sum) = levelData[level];
            levelData[level] = levelData[level] with { NodesNo = no + 1, Sum = sum + node.val };
            
            // advance
            queue.Enqueue(new NodeInfo(node.left, level + 1));
            queue.Enqueue(new NodeInfo(node.right, level + 1));
        }
        
        return levelData
            .Select(data =>
            {
                var (no, sum) = data.Value;
                return sum / no;
            })
            .ToList();
    }
    
    private record NodeInfo(TreeNode Node, int level);
    
    private record LevelInfo(int NodesNo, double Sum);
}