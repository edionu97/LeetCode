public class Solution 
{
    public IList<int> FindSubstring(string s, string[] words)
    {
        // iterate words and compute the occurence map
        Dictionary<string, int> occurences = new();
        foreach(var word in words)
        {
            if(!occurences.ContainsKey(word))
            {
                occurences.Add(word, 0);
            }
            
            ++occurences[word];
        } 
        
        // compute all indices
        IList<int> l = new List<int>();
        HashSet<int> visited = new();
        for(var i = 0; i < s.Length; ++i)
        {
            if(visited.Contains(i))
            {
                continue;
            }
            
            l = l.Concat(GetAllConcatenatedStringsFrom(i, s, occurences, words.Length, visited)).ToList();
        }
        
        return l;
    }
    
    /// Checks if there is an concatenated string starting from current position in right direction
    private static IEnumerable<int> GetAllConcatenatedStringsFrom(int windowLeft, string s, IDictionary<string, int> occurences, int wordNo, ISet<int> visited)
    {
        // do not repeat already computed steps
        if(visited.Contains(windowLeft))
        {
            yield break;
        }
        
        // compute word length
        var wordLength = occurences.First().Key.Length;
        
        Dictionary<string, int> wordOccurence = new();
        
        // try find all the concatenated substrings starting from 'head' position
        int matches = 0, head = windowLeft, maxWindowLeft = s.Length - wordLength;
        
        while(head <= maxWindowLeft)
        {
            // mark head as visited to no longer use it in next function call
            visited.Add(head);
            
            // get current string s[head...wordLength]
            var current = s.Substring(head, wordLength);
            
            // move window right if no word match staring from head and reinitialize all values
            if(!occurences.ContainsKey(current))
            {
                // our most left point is advanced
                windowLeft = head = head + wordLength;
                
                // forget all the memorized data
                matches = 0;
                wordOccurence.Clear();
                continue;
            }
            
            // increment word occurences when a match is found
            if(!wordOccurence.ContainsKey(current))
            {
                wordOccurence.Add(current, 0);
            }
            ++wordOccurence[current];
            
    
            // move window right if given condition is not satisified
            while(windowLeft <= head && wordOccurence[current] - occurences[current] > 0)
            {
                var subs = s.Substring(windowLeft, wordLength);
                
                windowLeft += wordLength;
            
                // remove word from window
                --matches;
                --wordOccurence[subs];
            }
            
            // we need to remove all choosen words to keep the number of occurences true
            matches = Math.Max(0, matches);
            windowLeft = Math.Min(windowLeft, head);
            
            // we found a position and its the left side of the window
            if(++matches == wordNo)
            { 
                yield return windowLeft;
            }
            
            // advance to next word
            head += wordLength;
        }
    }
    
   
}
