public class Solution 
{
    public void Rotate(int[] nums, int k) 
    {
        var m = nums.Length;

        // find the actual number of rotations
        // when k is multiple of m => no rotation are required
        k = k % m;

        // corner case (no rotations are required)
        if(k == 0)
        {
            return;
        }

        // copy the array
        var numsCopy = new int[m];
        for(var i = 0; i < m; ++i)
        {
            numsCopy[i] = nums[i];
        }

        // put first nums in the array
        int startIdx = m - k, idx = 0;
        for(var i = startIdx; i < m; ++i)
        {
            nums[idx++] = numsCopy[i];
        }

        // put last part
        for(var i = 0; i < startIdx; ++i)
        {
            nums[idx++] = numsCopy[i];
        }
    }
}