public class Solution {
    
    public bool WordPattern(string pattern, string s) 
    {

        // split pattern into words
        var wordPattern = pattern.Aggregate(string.Empty, (prev, c) => prev + " " + c).Trim();
    
        // compute the word distinct pattern
        var patternDistinctPattern = BuildDistinctPattern(wordPattern);
        
        // if two words have the same pattern => pattern matched
        return BuildDistinctPattern(s) == patternDistinctPattern; 
    }
    
    // Creates a new pattern for the s string based on distinct words
    private string BuildDistinctPattern(string input)
    {
        // get all the words in the input
        var words = input.Split(' ', StringSplitOptions.RemoveEmptyEntries);
        
        Dictionary<string, int> distinctWordMap = new();
        
        // compute the distinct pattern first distinct word will have 1, second distinct word will have 2, etc
        var distincts = 0;
        var distinctPattern = new StringBuilder();
        foreach(var w in words)
        {
            if(!distinctWordMap.ContainsKey(w))
            {
                distinctWordMap[w] = ++distincts;
            }
            distinctPattern.Append(distinctWordMap[w]);     
        }
        
        return distinctPattern.ToString();
    }
}