public class Solution {
    public int Jump(int[] nums)
    {
        var jumps = new int[nums.Length];

        // iterate 
        for(var i = 0; i < nums.Length; ++i)
        {
            var j = i;
            while(nums[i]-- > 0 && j + 1 < nums.Length)
            {
                if(jumps[++j] == 0)
                {
                    jumps[j] = jumps[i] + 1;
                }
            }
        }

        return jumps[nums.Length - 1];
    }
}