public class Solution 
{
    public int LengthOfLastWord(string s) 
    {
        int  l = 0, i = s.Length - 1;

        // get first letter
        while(i >= 0 && s[i] == ' ')
        {
            --i;
        }

        // compute the word diff
        while(i >= 0 && s[i] != ' ' )
        {
            ++l;--i;
        }

        return l;
    }
}