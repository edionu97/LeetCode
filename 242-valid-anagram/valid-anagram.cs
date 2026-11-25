public class Solution 
{
    public bool IsAnagram(string s, string t) 
    {
        // build the occurence map for s array
        Dictionary<char, int> occurenceMap = new();
        foreach(var c in s)
        {
            if(!occurenceMap.ContainsKey(c))
            {
                occurenceMap[c] = 0;
            }
            
            ++occurenceMap[c];
        }
        
        foreach(var c in t)
        {
            // t contain different letters than s
            if(!occurenceMap.ContainsKey(c))
            {
                return false;
            }
            
            // no more availabe letters in s
            if(occurenceMap[c] == 0)
            {
                return false;
            }
            
            --occurenceMap[c];
        }
        
        // anagrams only if we've used all leters fro. s
        return occurenceMap.Values.All(x => x == 0);
    }
}