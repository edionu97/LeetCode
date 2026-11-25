public class Solution 
{
    public bool IsValid(string s) 
    {
        // specify all parentheses grouos
        Dictionary<char, char>brackets = new()
        {
            ['('] = ')',
            ['['] = ']',
            ['{'] = '}'
        };
        
        Stack<char> stack = new();
        foreach(var c in s)
        {
            
            if(brackets.ContainsKey(c))
            {
                stack.Push(c);
                continue;
            }
            
            // no longer have any opened parenthesrs
            if(!stack.Any())
            {
                return false;
            }
            
            // parentheses missmatch
            if(brackets[stack.Pop()] != c)
            {
                return false;
            }
        }
    
        return !stack.Any();
    }
}