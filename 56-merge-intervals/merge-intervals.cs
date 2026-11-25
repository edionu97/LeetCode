public class Solution 
{
    public int[][] Merge(int[][] intervals) 
    {
        // create comparer and sort the intervals array
        var comparer = Comparer<int[]>.Create((a, b) => a[0] != b[0] ? a[0] - b[0] : a[1] - b[1]);
        
        intervals = intervals.OrderBy(x => x, comparer).ToArray();
        
        // merge intervals
        List<int[]> result = new();
        for(var i = 0; i < intervals.Length; ++i)
        {
            var curent = intervals[i];
            while(i + 1 < intervals.Length && curent[1] >= intervals[i + 1][0])
            {
                curent[1] = Math.Max(intervals[++i][1], curent[1]);
            }
            
            result.Add(curent);
        }
        
        return result.ToArray();  
    }
}