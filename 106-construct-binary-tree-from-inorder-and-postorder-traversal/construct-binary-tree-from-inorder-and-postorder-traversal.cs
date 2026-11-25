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
    public TreeNode BuildTree(int[] inorder, int[] postorder) 
    {
        Dictionary<int, int> inorderRootIdx = new();

        // keep the index of each element from the inorder traversal
        var m = inorder.Length;
        for(var i = 0; i < m; ++i)
        {
            inorderRootIdx[inorder[i]] = i;
        }

        var rootIdx = m - 1;
        return BuildTree(0, m - 1, ref rootIdx);

        TreeNode BuildTree(int li, int ls, ref int idx)
        {
            if(li > ls)
            {
                return null;
            }

            if(li < 0 || ls > m)
            {
                return null;
            }

            // element from the idx node will be the root of the current subtree
            var root = new TreeNode(postorder[idx--]);
        
            // get the idx of the root idx in the inorder array => to help us buld the left and right subtrees
            var rootIdx = inorderRootIdx[root.val];

            // put roght root nodes in the right of the current tree first (postorder)
            root.right = BuildTree(rootIdx + 1, ls, ref idx);

            // put the nodes fom left
            root.left = BuildTree(li, rootIdx - 1, ref idx);
            
            // we have a root
            return root;
        }
    }
}