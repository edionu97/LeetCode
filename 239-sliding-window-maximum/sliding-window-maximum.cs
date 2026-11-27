using System.Numerics;

public class Solution 
{
    public int[] MaxSlidingWindow(int[] nums, int k) 
    {
        if (k == 1)
        {
            return nums;
        }

        // Create a range maximum query (RMQ) structure for the input array
        RMQ rmq = new(nums, RMQ.QueryType.Max);

        int[] result = new int[nums.Length - k + 1];
        for (int i = 0; i <= nums.Length - k; i++)
        {
            // Query the RMQ structure for the maximum value in the current sliding window
            result[i] = rmq.Query(i, i + k - 1);

        }

        return result;
    }

    /// <summary>
    /// Provides efficient range minimum or maximum queries using a sparse table.
    /// </summary>
    /// <remarks>Supports inclusive range queries after construction and is configured for either minimum or
    /// maximum aggregation.</remarks>
    private sealed class RMQ
    {
        private readonly int[][] sparseTable;

        private readonly Func<int, int, int> valueCallback;

        public RMQ(int[] array, QueryType queryType = QueryType.Max)
        {
            if (array == null || array.Length == 0)
            {
                throw new ArgumentException("Array cannot be null or empty.", nameof(array));
            }

            // Set the value callback function based on the query type (minimum or maximum)
            this.valueCallback = (queryType == QueryType.Min) ? Math.Min : Math.Max;

            // Build the sparse table for efficient range queries
            this.sparseTable = BuildSparseTable(array, valueCallback);
        }

        /// <summary>
        /// Returns the minimum or maximum value for the specified inclusive range.
        /// </summary>
        /// <param name="left">The inclusive zero-based index of the range's starting element.</param>
        /// <param name="right">The inclusive zero-based index of the range's ending element.</param>
        /// <returns>The aggregate value computed for the specified range.</returns>
        /// <exception cref="ArgumentOutOfRangeException">Thrown when the range is invalid or either index is outside the table.</exception>
        public int Query(int left, int right)
        {
            if (left < 0 || right >= sparseTable.Length || left > right)
            {
                throw new ArgumentOutOfRangeException($"Invalid range for RMQ query, range: [{left}, {right}]");
            }

            // Compute the length of the range and the largest power of 2 that fits within that length
            int length = right - left + 1, log2 = BitOperations.Log2((uint)length);

            // Get the reminder of the length when divided by the largest power of 2
            int reminder = length - (1 << log2);

            // If the reminder is zero, it means the range length is exactly a power of 2, and we can directly return the value from the sparse table
            if (reminder == 0)
            {
                return sparseTable[left][log2];
            }

            // If the reminder is not zero, we need to combine the results from two overlapping ranges in the sparse table
            return valueCallback.Invoke(sparseTable[left][log2], sparseTable[right - (1 << log2) + 1][log2]);
        }

        private static int[][] BuildSparseTable(int[] array, Func<int, int, int> getValue)
        {
            // Calculate the maximum power of 2 needed for the sparse table
            int log2 = BitOperations.Log2((uint)array.Length);

            // Initialize the sparse table
            int[][] sparseTable = new int[array.Length][];
            for (int i = 0; i < array.Length; i++)
            {
                // Allocate space for each row in the sparse table
                sparseTable[i] = new int[log2 + 1];

                // Set the first column of the sparse table to the original array values
                // This is the base case for the sparse table, where each element is its own minimum or maximum
                sparseTable[i][0] = array[i];
            }

            // Build the sparse table using dynamic programming
            // Construct it in a bottom-up manner, per column , where each column represents a power of 2 range
            for (int j = 1; j <= log2; j++)
            {
                // We compute the minimum or maximum for ranges of size 2^j by combining results from two overlapping ranges of size 2^(j-1)
                for (int i = 0; i <= array.Length - (1 << j); i++)
                {
                    sparseTable[i][j] = getValue.Invoke(sparseTable[i][j - 1], sparseTable[i + (1 << (j - 1))][j - 1]);
                }
            }

            return sparseTable;
        }

        public enum QueryType : byte
        {
            /// <summary>
            /// Gets the minimum value.
            /// </summary>
            Min,

            /// <summary>
            /// Gets the maximum value.
            /// </summary>
            Max
        }
    }
}