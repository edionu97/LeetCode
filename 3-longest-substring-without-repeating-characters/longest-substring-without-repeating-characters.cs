public class Solution 
{
    public int LengthOfLongestSubstring(string s) 
    {
        int head = 0, maxWidth = 0;
        
        // initially assume no chars in list
        HashSet<char>headChars = new();
        for(var i = 0; i < s.Length; ++i)
        {
            
            // move window right untill given condition is satisfied
            while(headChars.Contains(s[i]) && head < s.Length)
            {
                headChars.Remove(s[head++]);
            }
            
            maxWidth = Math.Max(i - head + 1, maxWidth);
            
            headChars.Add(s[i]);
        }
        
        return maxWidth;
    }
}