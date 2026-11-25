public class Solution 
{
    public string ReverseWords(string s) 
    {
        var stack = new Stack<string>();

        for(var i = 0; i < s.Length; ++i)
        {
            // eliminate spaces and find first letter from a word
            while(i < s.Length && s[i] == ' ')
            {
                ++i;
            }
            var start = i;

            // get all the other letters
            while(i < s.Length && s[i] != ' ')
            {
                ++i;
            }
            var end = i - 1; 

            // remove empty strings
            var word = s.Substring(start, end - start + 1);
            if(string.IsNullOrEmpty(word))
            {
                continue;
            }

            stack.Push(word);
        }

        return string.Join(' ', stack);
    }
}