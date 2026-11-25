public class Solution 
{
    public bool IsSubsequence(string s, string t) 
    {
        Dictionary<char, IList<int>> indices = new();
        
        // compute indice map for each charater
        for(var i = 0; i < t.Length; ++i)
        {
            if(!indices.ContainsKey(t[i]))
            {
                indices.Add(t[i], new List<int>());
            }
               
            indices[t[i]].Add(i);
        }
        
        var current = -1;
        foreach(var c in s)
        {
            // if letted not found it cannot be a subsection
            if(!indices.ContainsKey(c))
            {
                return false;
            }
            
            // only for intialization, to not put it separatelly
            if(current < 0)
            {
                current = indices[c].First();
                continue;
            }
             
            current = FindFirstGreaterThan(indices[c], current);
        
            if(current < 0)
            {
                return false;
            }
        
        }
 
       return true;
    }
              
               
    private static int FindFirstGreaterThan(IList<int> elements, int e)
    {
        var m = elements.Count - 1;
        
        return FindFirstGreaterThan(elements, 0, m, e); 
    }

    private static int FindFirstGreaterThan(IList<int> elements, int start, int end, int element)
    {
        // not found scenario
        if(start > end)  
        {
            return -1;
        }
        
        // compute middle
        var m = (start + end) >> 1;
        
        var leftE = m - 1 >= 0 ? elements[m - 1] : -1;
        
        var rightE = m + 1 >= elements.Count ? -1 : elements[m + 1];
        
        // Console.WriteLine(left + " " + start + " " + end + " " + elements[m]);
        
        if(elements[m] == element)
        {
            return rightE;
        }
        
        if(elements[m] > element && element > leftE)
        {
            return elements[m];
        }
        
        // look for greater elements
        if(element > elements[m])
        {
            return FindFirstGreaterThan(elements, m + 1, end, element);
        }
        
        // look for smaller elements
        return FindFirstGreaterThan(elements, start, m - 1, element);
    }
}