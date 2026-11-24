public class Solution 
{
    public int[] TwoSum(int[] nums, int target) 
    {
        // compute the occurence dictionay
        Dictionary<int, int> o = new();
        for(var i = 0; i < nums.Length; ++i)
        {
            // for each number keep first occurence position
            if(!o.ContainsKey(nums[i]))
            {
                o[nums[i]] = i;
                continue;
            }
        }
               
        for(var i = 0; i < nums.Length; ++i)
        {
            var d = target - nums[i];
            
            //nums[i] not a valid candidate
            if(!o.ContainsKey(d))
            {
                continue;
            }
            
            // not use same number
            if(i == o[d])
            {
                continue;
            }
            
            return new int[]{i, o[d]};
        }
        
        return new int[]{};
    }
}