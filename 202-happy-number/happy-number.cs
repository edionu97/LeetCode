public class Solution 
{
    public bool IsHappy(int n) 
    {
        HashSet<int> s = new();
        
        // as long as n is not 1
        while(n != 1)
        {
            if(s.Contains(n))
            {
                return false;
            }
            
            s.Add(n);
            n = GetSquaredDigitSum(n);
        }
        
        return true;
    }
    
    private static int GetSquaredDigitSum(int n)
    {
        var s = 0;
        while(n != 0)
        {
            var c = (n % 10);
            
            s += c * c;
            n /= 10;
        }
        
        return s;
    }
}