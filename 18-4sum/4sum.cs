public class Solution 
{
    public IList<IList<int>> FourSum(int[] nums, int target) 
    {
        // Handle the case number of items is less than 4
        if (nums.Length < 4)
        {
            return [];
        }

        Array.Sort(nums);

        List<IList<int>> result = [];
        
        // Iterate through the array with two nested loops to fix the first two numbers of the quadruplet.
        // Then use two pointers to find the other two numbers that complete the quadruplet.
        // Complexity: O(n^3)
        for (int i = 0; i < nums.Length; i = AdvanceWithoutDuplicates(i))
        {
            for (int j = i + 1; j < nums.Length; j = AdvanceWithoutDuplicates(j))
            {
                int right = nums.Length - 1, left = j + 1;

                // Use a while loop to find the other two numbers that complete the quadruplet.
                while (j < left && left < right && right < nums.Length)
                {
                    // Check for overflow and underflow by using Int128 to store the sum of the four numbers.
                    Int128 sum = (Int128)nums[i] + nums[j] + nums[left] + nums[right];

                    if (sum > int.MaxValue)
                    {
                        --right;
                        continue;
                    }

                    if (sum < int.MinValue)
                    {
                        ++left;
                        continue;
                    }

                    // If the sum is equal to the target, we add the quadruplet to the result and move both pointers to find other potential quadruplets.
                    if (sum == target)
                    {
                        result.Add([nums[i], nums[j], nums[left], nums[right]]);
                    }

                    // If the sum is greater than the target, we need to decrease the sum by moving the right pointer to the left. 
                    if (sum > target)
                    {
                        while (right - 1 > left && nums[right] == nums[right - 1])
                        {
                            --right;
                        }
                        --right;

                        continue;
                    }

                    // If the sum is less than the target, we need to increase the sum by moving the left pointer to the right.
                    while (left + 1 < right && nums[left + 1] == nums[left])
                    {
                        left++;
                    }
                    ++left;
                }
            }
        }

        return result;

        int AdvanceWithoutDuplicates(int i)
        {
            while (i + 1 < nums.Length && nums[i] == nums[i + 1])
            {
                i++;
            }
            
            return i + 1;
        }
    }
}