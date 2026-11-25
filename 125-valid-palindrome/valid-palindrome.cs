public class Solution 
{
    public bool IsPalindrome(string s)     
    {
        int left = 0, right = s.Length - 1;
        
        while(left <= right)
        {
            // ignore non alphanumeric from seft side
            while(left <= right && !IsAlphanumeric(s[left]))
            {
                ++left;
            }
             
            // ignore alphanumeric from left
            while(left <= right && !IsAlphanumeric(s[right]))
            {
                --right;
            }
        
            // stop condition
            if(left > right)
            {
                break;
            }
            
            if(!AreEqual(s[left], s[right]))
            {
                return false;
            }
    
            ++left; --right;
        }
        
        return true;
    }
                 
                  
    private static bool IsAlphanumeric(char s)
    {
        if(s >= '0' && s <= '9')
        {
            return true;
        }
        
        if(s >= 'a' && s <= 'z')
        {
           return true;
        }
        
        return s >= 'A' && s <= 'Z';
    }
    
    private static bool AreEqual(char c1, char c2)
    {
        if(c1 == c2)
        {
            return true;
        }
        
        // check are same
        var min = Math.Min((int) c1, (int) c2);   

        var max = Math.Max((int) c1, (int) c2);
        
        // if min charactef is upercase letter and not equal to max maybe the other is a lowercase letter
        if(min >= (int)'A' && min <= (int) 'Z')
        {
           return (char)(min + 32) == (char)max;
        }
        
        return false;
    }
    
}