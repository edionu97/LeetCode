public class Solution 
{
    public IList<IList<string>> GroupAnagrams(string[] strs)
    {
        // build a dictionary having as key the letter distribution in words, and as value all words having that distribution
        Dictionary<string, IList<string>> wordLetterDistribution = new();
        
        foreach(var w in strs)
        {
            var key = GetLetterDistribution(w);
            
            // that distribution is unknown for moment
            if(!wordLetterDistribution.ContainsKey(key))
            {
                wordLetterDistribution[key] = new List<string>();
            }
    
            // add the word in lettr distribution group
            wordLetterDistribution[key].Add(w);
        }
        
        // build response
        List<IList<string>> res = new();
        foreach(var lst in wordLetterDistribution.Values)
        {
            res.Add(lst);
        }
        
        return res;
    }
    
    private static string GetLetterDistribution(string s)
    {
        // compute the letter distribution map
        Dictionary<char, int> occurenceMap = new();
        foreach(var c in s)
        {
            var value = !occurenceMap.ContainsKey(c) ? 0 : occurenceMap[c];
            
            occurenceMap[c] = value + 1;
        }
        
        // compute the letter distribution key
        StringBuilder distribution = new();
        for(var i = 'a'; i <= 'z'; ++i)
        {
            // letter not in word
           if(!occurenceMap.ContainsKey(i))
           {
               continue;
           }
            
           distribution.Append(i).Append(occurenceMap[i]); 
        }
        
        return distribution.ToString();
    }
    
}