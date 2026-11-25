public class Solution 
{
    public int RemoveDuplicates(int[] nums) 
    {
        int idx = 0, m = nums.Length;
        for(int i = 1; i < m; ++i)
        {
            // find seq with all numbers equal
            int seqLength = 1;
            while(i < m && nums[idx] == nums[i])
            {
                ++i; ++seqLength;
            }
            
            // if seq has more than one element, add a duplicate
            if(seqLength >= 2)
            {
                nums[++idx] = nums[idx - 1];
            }

            // no more items left
            if(i >= m)
            {
                break;
            }

            nums[++idx] = nums[i];
        }

        return idx + 1;
    }
}