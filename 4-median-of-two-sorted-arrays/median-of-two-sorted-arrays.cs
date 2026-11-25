public class Solution 
{
    /// <summary>
    /// Returns the median of two sorted arrays in O(log(min(m, n))) time, without merging them.
    /// </summary>
    /// <remarks>
    /// The median is whatever value splits the merged ordering into two halves of equal size, so
    /// rather than building that ordering we search for the split directly. Any candidate split is
    /// fully described by a single number: how many of the smallest elements are taken from the
    /// shorter array. The rest of the left half is then forced to come from the longer array, which
    /// makes the search one-dimensional and therefore binary-searchable.
    /// <para>
    /// A split is correct when every element on the left is less than or equal to every element on
    /// the right. Since both arrays are already sorted, that check collapses to comparing the two
    /// elements straddling each cut. If the shorter array pushed a value that is too large onto the
    /// left, it took too many elements and the upper bound drops; otherwise it took too few and the
    /// lower bound rises.
    /// </para>
    /// </remarks>
    /// <param name="nums1">A sorted array. May be empty.</param>
    /// <param name="nums2">A sorted array. May be empty, but not at the same time as <paramref name="nums1"/>.</param>
    /// <returns>The median of the combined elements of both arrays.</returns>
    public double FindMedianSortedArrays(int[] nums1, int[] nums2)
    {
        // Ensure nums1 is the smaller array
        if (nums1.Length > nums2.Length)
        {
            (nums1, nums2) = (nums2, nums1);
        }

        for (int minTake = 0, maxTake = nums1.Length, leftPartSize = (nums1.Length + nums2.Length + 1) / 2; minTake <= maxTake;)
        {
            // Compute how many elements to take from the shorter array and the longer array
            int takeFromShorter = (minTake + maxTake) / 2;
            int takeFromLonger = leftPartSize - takeFromShorter;

            // Get the boundary elements for the partitions for both arrays
            int maxLeftShorter = takeFromShorter == 0 ? int.MinValue : nums1[takeFromShorter - 1];
            int minRightShorter = takeFromShorter == nums1.Length ? int.MaxValue : nums1[takeFromShorter];

            int maxLeftLonger = takeFromLonger == 0 ? int.MinValue : nums2[takeFromLonger - 1];
            int minRightLonger = takeFromLonger == nums2.Length ? int.MaxValue : nums2[takeFromLonger];

            // Check if we have found the correct partition
            if (maxLeftShorter <= minRightLonger && maxLeftLonger <= minRightShorter)
            {
                // Handle the case where the total number of elements is even => median is the average of the two middle elements
                if ((nums1.Length + nums2.Length) % 2 == 0)
                {
                    return (Math.Max(maxLeftShorter, maxLeftLonger) + Math.Min(minRightShorter, minRightLonger)) / 2.0;
                }

                // Because we are taking the left part size as (nums1.Length + nums2.Length + 1) / 2, the median will be the max of the left parts
                return Math.Max(maxLeftShorter, maxLeftLonger);
            }

            // Handle the case when the partition is not correct, and we need to decrease the number of elements taken from the shorter array
            if (maxLeftShorter > minRightLonger)
            {
                maxTake = takeFromShorter - 1;
                continue;
            }

            // Handle the case when the partition is not correct, and we need to increase the number of elements taken from the shorter array
            minTake = takeFromShorter + 1;
        }

        return -1;
    }
}