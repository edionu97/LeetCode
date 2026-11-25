public class Solution 
{
    public bool CanConstruct(string ransomNote, string magazine) 
    {
        // construct the letter frequency map from the magazine string
        Dictionary<char, int> freq = new();
        foreach(var c in magazine)
        {
            if(!freq.ContainsKey(c))
            {
               freq[c] = 0; 
            }  
            ++freq[c];
        }
        
        foreach(var c in ransomNote)
        {
            if(!freq.ContainsKey(c))
            {
                return false;
            }
            
            if(freq[c]-- <= 0)
            {
                return false;
            }
        }
        
        return true;
    }
}