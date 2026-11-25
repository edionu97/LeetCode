public class Solution 
{
    public bool IsIsomorphic(string s, string t)
    {
       return IsIso(s, t) && IsIso(t, s); 
    }
    
    private static bool IsIso(string s, string t) 
    {
        
        Dictionary<char, char> replaceMap = new(); 
        
        for(var idx = 0; idx < s.Length; ++idx)
        { 
            var c = s[idx];
            
            // if first time a replacement is found, memorise it
            if(!replaceMap.ContainsKey(c))
            {
                replaceMap[c] = t[idx];
                continue;
            }
            
            if(t[idx] != replaceMap[c])
            {
                return false;
            }
        }
        
        return true;
    }
}