public class Solution 
{
    public int RemoveElement(int[] nums, int val) 
    {
        int m = nums.Length, i = 0;
        while(i < m)
        {
            // handle positive case
            if(nums[i] != val)
            {
                ++i;
                continue;
            }
            
            
            m = FindLastIndexOfStartingWith(nums, m, val);
        
            // no other value to swap
            if(m == -1 || m < i)
            {
                break;
            }
            nums[i++] = nums[m];
        }
        
        return i;
    }

    /// Finds the index of last item from the <paramRef="nums"/> which is not equal to <paramRef="val"/>
    public int FindLastIndexOfStartingWith(int[] nums, int position, int val)
    {
        for(int idx = position - 1; idx >= 0;  --idx)
        { 
            if(nums[idx] != val)
            { 
               return idx;
            }
        }
        
        return -1;
    }
}