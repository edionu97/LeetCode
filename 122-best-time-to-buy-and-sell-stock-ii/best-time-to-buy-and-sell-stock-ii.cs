public class Solution 
{
    public int MaxProfit(int[] prices)     
    {
        int m = prices.Length, profit = 0;
        
        // compute the total profit
        for(var i = 0; i < m - 1; ++i)
        {
            // identify the end of the current ascending trend if exists

            while(i < m - 1 && prices[i] >= prices[i + 1]) 

            {
                ++i;
            }
            var bottom = i;
            
            // identify the highest value from the current ascending trend (if exists)
            while(i < m - 1 && prices[i + 1] >= prices[i]) 
            {
                ++i;
            }
            var top = i;
        
            profit += Math.Max(0, prices[top] - prices[bottom]);
       }
        
        return profit;
    }
        
}