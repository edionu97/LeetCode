public class Solution 
{
    public int[][] Insert(int[][] intervals, int[] newInterval)
    {
        // get insert pos
        var p = GetIntervalInsertPosition(intervals, newInterval);
        
        List<int[]> lst = new();
        for(int i = 0, j = 0; i <= intervals.Length; ++i)
        {
            if(i == p)
            {
                lst.Add(newInterval);
                continue;
            }
            
            lst.Add(intervals[j++]);
        }
        
        return MergeSortedIntervals(lst.ToArray());
    }
    
    private static int GetIntervalInsertPosition(int[][] intervals, int[] newI)
    {
        return FindPos(0, intervals.Length - 1);
        
        // binary search O(logn)
        int FindPos(int li, int ls)
        {
            // insert after last
            if(li > ls)
            {
                return li;
            }
            
            // compute middle
            var m = (li + ls) >> 1;
            
            var middleI = intervals[m];
            
            var leftI = m - 1 >= 0 ? intervals[m - 1] : new int[]{-1, -1};
             
            // we found m when lI < nI <= rI
            if(IsLess(leftI, newI) && IsGreaterE(middleI, newI))
            {
                return m;
            }
            
            // search left
            
            if(IsGreaterE(middleI, newI))
            {
                return FindPos(li, m - 1);
            }
            
            // search right
            return FindPos(m + 1, ls);
        }
        
        // Checks if a is greater or equal than b
        bool IsGreaterE(int[] a, int[] b)
        {
            // if both intervals start with same left value second must be larger 
            if(b[0] == a[0])
            {
                return b[1] < a[1];
            }
            
            return b[0] < a[0];
        }
                   
        // Checks if a is less than b
        bool IsLess(int[] a, int[] b)
        {
            return a[0] < b[0] && a[1] < b[1];
        }
    }
    
    private static int[][] MergeSortedIntervals(int[][] intervals) 
    {
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