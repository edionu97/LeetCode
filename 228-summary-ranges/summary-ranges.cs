public class Solution 
{
    public IList<string> SummaryRanges(int[] nums) 
    {
        List<string> s = new();
        
        int n = nums.Length;
        for(var i = 0; i < n; ++i)
        {
            var left = i;
            
            //try move the right up  
            while(i + 1 < n && nums[i + 1]  - nums[i] == 1)
            {
                ++i;
            }
            
            if(i - left == 0)
            {
                s.Add(nums[i].ToString());
                continue;
            }
            
            s.Add($"{nums[left]}->{nums[i]}");
        }
        
        return s;
    }
}