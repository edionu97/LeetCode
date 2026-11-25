public class Solution 
{
    public bool CanJump(int[] nums) 
    {
        var current = nums.Length - 1;
        for(var i = current - 1; i >= 0; --i)
        {
            if(i + nums[i] >= current)
            {
                current = i;
            }
        }

        return current == 0;
    }
}