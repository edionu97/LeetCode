using System.Runtime.InteropServices;

public class Solution 
{
    public int SubarraySum(int[] nums, int k)
    {
        // Intialize a variable to compute the prefix sums till current index
        int[] prefixSumArray = new int[nums.Length + 1];

        // Initialize a variable to keep track of the count prefix sums encountered so far
        Dictionary<int, int> prefixSumCount = [];

        // Build prefix sums, with index 0 representing the empty prefix.
        for (int i = 0; i < nums.Length; i++)
        {
            prefixSumArray[i + 1] = prefixSumArray[i] + nums[i];

            // Increment the count of the current prefix sum in the prefixSumCount dictionary
            ref int count = ref CollectionsMarshal.GetValueRefOrAddDefault(prefixSumCount, prefixSumArray[i + 1], out _);
            count++;
        }

        int subArrayCount = 0;
        for (int i = 0; i < nums.Length; i++)
        {
            // Each time we move to the next index, we decrement the count of the current prefix sum in the prefixSumCount dictionary
            if (i > 0)
            {
                prefixSumCount[prefixSumArray[i]]--;
            }

            // We compute the y = k + prefixSumArray[i] and check if y exists in the prefixSumCount dictionary.
            // If it does, it meas that we can use that y (which is a prefix sum) to obtain k by doing k = y - prefixSumArray[i]. 
            if (prefixSumCount.TryGetValue(prefixSumArray[i] + k, out int count) && count > 0)
            {
                subArrayCount += count;
            }
        }

        return subArrayCount;
    }
}