public class Solution 
{
    public int[] ProductExceptSelf(int[] nums) 
    {
        var m = nums.Length;
        var output = new int[m];
        
        // compute output containg left *
        for(var i = m - 1; i >= 0; --i)
        {
            // corner case
            if(i + 1 >= m)
            {
                output[i] = nums[i];
                continue;
            }
            
            // compute partial sum to the left
            output[i] = nums[i] * output[i + 1];
        }
        
        // compute sum to the right
        for(var i = 0; i < m; ++i)
        {
            if(i - 1 < 0)
            {
                // nums[i] = nums[i];
                continue;
            }
            
            nums[i] = nums[i - 1] * nums[i];
        }
        
        // use recurency formula o[i] = nums[i -1] × o[i + 1];
        for(var i = 0; i < m; ++i)
        {
            if(i - 1 < 0)
            {
                output[i] = output[i + 1];
                continue;
            }
            
            if(i + 1 >= m)
            {
                output[i] = nums[i - 1];
                continue;
            }
            
            output[i] = nums[i - 1] * output[i + 1];
        }
        
        return output;
    }
}