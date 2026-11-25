public class Solution 
{
    public int FindMinArrowShots(int[][] points) 
    {
        // create comparer and sort the points array
        var comparer = Comparer<int[]>.Create((a, b) => a[0] != b[0] ? a[0].CompareTo(b[0]) : a[1].CompareTo(b[1]));

        var intervals = points.OrderBy(x => x, comparer).ToArray();
        
        // merge intervals
        var arrows = 0;
        for(var i = 0; i < intervals.Length; ++i)
        {
            var curent = intervals[i];

            while(i + 1 < intervals.Length && curent[1] >= intervals[i + 1][0])
            {
                curent[1] = Math.Min(intervals[++i][1], curent[1]);
            }
            
            ++arrows;
        }
    
        return arrows;  
    }
}