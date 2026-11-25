public class Solution 
{
    public int Change(int amount, int[] coins) 
    {
        // dp[i] holds the number of ways in which we can use coins to express ammount i
        int[] dp = new int[amount + 1];

        // ammount 0 can be expresed in a single way, by choosing nothing
        dp[0] = 1;

        // Iterate the coins
        for(int i = 0; i < coins.Length; ++i)
        {
            // Use the coin[i] to update the partial ammounts starting with current coin
            for(int j = coins[i]; j <= amount; ++j)
            {
                dp[j] += dp[j - coins[i]];
            }
        }

        return dp[amount];
    }
}