public class Solution 
{
    public int ThreeSumClosest(int[] nums, int target)
    {
        // If the length of the array is less than 3, return -1 as we cannot find a triplet
        if (nums.Length < 3)
        {
            return -1;
        }

        Array.Sort(nums);

        ClosestSum closestSum = new(int.MaxValue, int.MaxValue);

        // Iterate through the array and use a two-pointer approach to find the closest sum to target that can be formed using
        //  numbers[left] and two other numbers in the array from range [left + 1, n)
        // Complexity : O(n^2)
        for (int left = 0; left < nums.Length; ++left)
        {
            int mid = left + 1, right = nums.Length - 1;
            while (left < mid && mid < right && right < nums.Length)
            {
                int sum = nums[left] + nums[mid] + nums[right];

                // Compute the difference between the current sum and the target
                int difference = Math.Abs(sum - target);
                if (difference == 0)
                {
                    return sum;
                }

                if (closestSum > difference)
                {
                    closestSum = new ClosestSum(sum, difference);
                }

                // If we are too far away from the target, we need to move the right pointer to the left to decrease the sum (we'll obtain only bigger values from now on)
                // If we are too close to the target, we need to move the mid, pointer to the right to increase the sum (we'll obtain only smaller values from now on)
                _ = sum > target ? --right : ++mid;
            }
        }


        return closestSum.Value;
    }

    private readonly record struct ClosestSum(int Value, int Difference)
    {
        public static bool operator <(ClosestSum left, int a)
        {
            return left.Difference < a;
        }

        public static bool operator >(ClosestSum left, int a)
        {
            return left.Difference > a;
        }
    }
}