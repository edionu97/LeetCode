public class Solution 
{
    public int MinSubArrayLen(int target, int[] nums) 
    {
        int partialSum = 0, head = 0, len = int.MaxValue;
        
        // start with a window of 0 elements first
        for(var i = 0; i < nums.Length; ++i)
        {
            // asume current is part of continous subarray starting from head to i
            partialSum += nums[i];
            
            // try to advance the subarray window in right direction
            while(partialSum >= target && head < nums.Length)
            {
                len = Math.Min(len, i - head + 1);
                
                partialSum -= nums[head++];
            }
         
        }
        
        return len == int.MaxValue ? 0 : len;
    }
}