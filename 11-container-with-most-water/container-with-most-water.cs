public class Solution 
{
    public int MaxArea(int[] height) 
    {
        int area = 0, i = 0, j = height.Length - 1;
        
        // find two numbers with smaller difference which are as farthest as possible to each other
        while(i <= j && i >= 0 && j < height.Length)
        {
        
            // compute the current arrea
            area = Math.Max((j - i) * Math.Min(height[i], height[j]), area);
            
            // when advancing keep largest two numbers
            
            if(height[i] < height[j])
            {
                ++i;
                continue;
            }
            
            --j;
        }
        
        return area;
    }
}