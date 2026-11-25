public class Solution 
{
    public int[] TwoSum(int[] numbers, int target)
    {
        int left = 0, right = numbers.Length - 1;
        
        while(left <= right && left >= 0 && right < numbers.Length)
        {
            // compute edge sum
            var sum = numbers[left] + numbers[right];
            
            // stop condition
            if(sum == target)
            {
                return new int[]{ left + 1, right + 1 };
            }
            
            if(sum > target)
            {
                --right;
                continue;
            }
            
            ++left; 
        }
            
        return new int[]{};
    }
}