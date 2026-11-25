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
public class BSTIterator 
{
    private readonly Stack<TreeNode> _dfsStack = new();

    public BSTIterator(TreeNode root)
    {
       PerformLeftBranchDfs(root);
    }
    
    public int Next() 
    {
        var leftest = _dfsStack.Pop();

        // now we need to left taverse the right part of current node
        PerformLeftBranchDfs(leftest.right);

        return leftest.val;
    }
    
    public bool HasNext() => _dfsStack.Any();

    // Perform dfs of only left branches starting from root
    // Because leftbranches should be processed first (inorder traversal)
    private void PerformLeftBranchDfs(TreeNode root)
    {
        if(root is null)
        {
            return;
        }

         // push in queue only the left nodes.
        for(; root is not null; root = root.left)
        {
            _dfsStack.Push(root);
        }
    }
}

/**
 * Your BSTIterator object will be instantiated and called as such:
 * BSTIterator obj = new BSTIterator(root);
 * int param_1 = obj.Next();
 * bool param_2 = obj.HasNext();
 */