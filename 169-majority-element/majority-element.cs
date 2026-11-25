public class Solution 
{
    public int MajorityElement(int[] nums) 
    {
        var majority = 0;
        var m = nums.Length;

        // an integer will be represented on 32 bits
        for(var bit = 0; bit < 32; ++bit)
        {
            var freq = 0;

            // find the majority for each bits (either 0 or 1)
            for(int i = 0; i < m; ++i)
            {
                if((nums[i] & (1 << bit)) != 0) 
                {
                    freq++;
                }
            }

            // if 1 has the majority => set that bit to 1 in the result
            if(freq > (m >> 1))
            {
                majority += 1 << bit;
            }
        }

        // not reachable
        return majority;
    }
}