public class Solution 
{
    public int LongestConsecutive(int[] nums) 
    {
        // mark all numbers from the set
        HashSet<int> numbers = new();
        foreach(var i in nums)
        { 
            numbers.Add(i);
        }
        
        // iterate over numbers
        var result = int.MinValue;
        foreach(var e in nums)
        {
            if(!numbers.Contains(e))
            {
                continue;
            }
            
            result = Math.Max(result, GetLongestConsSeqContaining(e, numbers));
        }
        
        return Math.Max(result, 0);
    }
    
    private static int GetLongestConsSeqContaining(int element, ISet<int> set)
    {
        var length = 1;
        
        // count length of the longest seq containing i (greater than i)
        var greater = element;
        while(set.Contains(++greater))
        {
            set.Remove(greater);
            ++length;
        }
    
        // count the lenght of the longest seq containing i - 1 (lowe than i)
        var lower = element;
        while(set.Contains(--lower))
        {
            ++length;
            set.Remove(lower);
        }

        return length;
    }
}