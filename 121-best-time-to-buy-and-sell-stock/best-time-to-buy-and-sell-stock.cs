public class Solution 
{
    public int MaxProfit(int[] prices) 
    {
        var m = prices.Length;

        // initialize profit matrix
        var profit = new int[m]; 

        var maxProfit = 0;
        for(var i = m - 1; i >= 0; --i)
        {
            // selling and buying in last day makes 0 profit (impossible)
            if(i == m - 1)
            {
                profit[i] = 0;
                continue;
            }

            // either choose the next day or the day to sell or best from other days
            profit[i] = Math.Max(prices[i + 1] - prices[i], prices[i + 1] + profit[i + 1] - prices[i]);
            maxProfit = Math.Max(maxProfit, profit[i]);
        }

        return maxProfit;
    }
}