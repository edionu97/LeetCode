public class Solution 
{
    public int Trap(int[] height)
    {
        var m = height.Length;

        // compute the right maximum value till a particular index
        var rMax = new int[m];
        for(var i = m - 2; i > 0; --i)
        {
            rMax[i] = Math.Max(height[i + 1], rMax[i + 1]);
        }

        // compute left maximum value till a particular index
        var lMax = new int[m];
        for(var i = 1; i < m; ++i)
        {
            lMax[i] = Math.Max(lMax[i - 1], height[i - 1]);
        }

        // compute trap value
        var totalWaterBlocks = 0;
        for(var i = 0; i < m; ++i)
        {
            // compute the number of total blocks as a difference between the smallest maximum value and 
            // current height value if there exist a height difference
            totalWaterBlocks += Math.Max(Math.Min(lMax[i], rMax[i]) - height[i], 0);
        }
        
        return totalWaterBlocks;
    }
}