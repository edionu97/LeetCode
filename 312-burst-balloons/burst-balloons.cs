public class Solution 
{
    public int MaxCoins(int[] nums)
    {
        // Create a new array containign the padding of the intial array (meanining has 1 in both ends)
        int[] paddedNums = new int[nums.Length + 2];
        Array.Copy(nums, 0, paddedNums, 1, nums.Length);
        paddedNums[0] = paddedNums[^1] = 1;

        // Intialize the dynamic programing matrix
        // dp[i][j] represents the maximum coins that we can obtain by bursting the ballons between the (i, j) interval
        // solution will be id dp[0][nums.Length + 1]
        int[][] dp = new int[paddedNums.Length][];
        for (int i = 0; i < paddedNums.Length; ++i)
        {
            dp[i] = new int[paddedNums.Length];
        }

        // 1 3 1 5 8 1
        // 0 1 2 3 4 5
        for (int intervalSize = 2; intervalSize < paddedNums.Length; ++intervalSize)
        {
            // We generate intervals of size intervalSize for each idx (0, 2), (1,3), (2, 4) ...
            for (int start = 0; start < paddedNums.Length - intervalSize; ++start)
            {
                int end = start + intervalSize;

                // In that interval (start, end) we'll have multiple ballons
                // Choose the ballon that will be the last bursted
                for (int lastBurst = start + 1; lastBurst < end; ++lastBurst)
                {
                    dp[start][end] = Math.Max(
                        dp[start][end],
                        dp[start][lastBurst] + dp[lastBurst][end] + paddedNums[start] * paddedNums[lastBurst] * paddedNums[end]);
                }

            }
        }

        return dp[0][paddedNums.Length - 1];
    }
}