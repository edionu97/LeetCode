public class Solution 
{
    public string SimplifyPath(string path) 
    {
        // get the elements from path
        var pathElements = path.Split(@"/", StringSplitOptions.RemoveEmptyEntries);
        
        //keep record of current folders
        Stack<string> canPathE = new();
        
        foreach(var element in pathElements)
        {
            // root is always in path
            if(!canPathE.Any())
            {
                canPathE.Push(string.Empty);
            }
            
            // move up
            if(element == "..")
            {
                canPathE.Pop();
                continue;
            }
            
            // stay
            if(element == ".")
            {
                continue;
            }
            
            // advance
            canPathE.Push(element); 
        }
        
        // build path
        var c = canPathE.ToArray();
        Array.Reverse(c);
        var cPath = string.Join('/', c);
        
        return string.IsNullOrEmpty(cPath) ? "/" : cPath;
    }
}