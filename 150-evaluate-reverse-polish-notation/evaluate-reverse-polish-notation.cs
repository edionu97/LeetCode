public class Solution 
{
    public int EvalRPN(string[] tokens)
    {
        // create the math operations
        Dictionary<string, Func<int, int, int>> op = new()
        {
            ["+"] = (a, b) => a + b,
            ["-"] = (a, b) => a - b,
            ["*"] = (a, b) => a * b,
            ["/"] = (a, b) => a / b
        };
        
        //evaluate expression
        Stack<int> expElements = new();
        foreach(var e in tokens)
        {
            // push operand in 
            if(!op.ContainsKey(e))
            {
                expElements.Push(int.Parse(e));
                continue;
            }
            
            // get the elements
            var b = expElements.Pop();
            var a = expElements.Pop();
            
            expElements.Push(op[e](a, b));
        }
        
        return expElements.Pop();
    }
}