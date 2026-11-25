public class Solution 
{
    public int RemoveDuplicates(int[] nums)
    {
        int m = nums.Length, idx = 0;
        
        // build with one traversal
        for(int i = 1; i < m; ++i)
        {
            // find next number different from the current one
            while(i < m && nums[i] == nums[idx])
            {
                ++i;
            }
            
            // nothing left
            if(i >= m)
            {
                break;
            }
            
            nums[++idx] = nums[i];
        }
        
        return idx + 1;
    }
}