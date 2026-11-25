public class Solution 
{
    public string MinWindow(string s, string t) 
    {
        // compute the occurence map
        Dictionary<string, int> occurences = new();
        foreach(var c in t)
        {
            var cString = c.ToString();
            if(!occurences.ContainsKey(cString))
            {
                occurences.Add(cString, 0);
            }
            ++occurences[cString];
        }

        // compute the min windows size
        Dictionary<string, int> wordOccurence = new();
        MinSecInfo secInfo = new (-1, int.MaxValue);
        int matches = 0, windowLeft = 0, head = 0;
        while(head <= s.Length - 1)
        {
            // get current string s[head...wordLength]
            var current = s.Substring(head, 1);

            // increment word occurences when a match is found
            if(!wordOccurence.ContainsKey(current))
            {
                wordOccurence.Add(current, 0);
            }

            // move window right if given condition is not satisified
            if(occurences.ContainsKey(current) && occurences[current] == ++wordOccurence[current])
            {
                ++matches;
            }

            // try to make the window shorter
            while(windowLeft <= head && matches == occurences.Count)
            {
                var secLength = head - windowLeft  + 1;

                // we have a new min value encountered
                if(secLength < secInfo.Length)
                {
                    secInfo = new MinSecInfo(windowLeft, secLength);
                }

                // remove word from window
                var subs = s.Substring(windowLeft++, 1);
                if(occurences.ContainsKey(subs) && --wordOccurence[subs] < occurences[subs])
                {
                    --matches;
                }
                wordOccurence[subs] = Math.Max(wordOccurence[subs], 0);
            }
            
            // we need to remove all choosen words to keep the number of occurences true
            matches = Math.Max(0, matches);
            windowLeft = Math.Min(windowLeft, head++);
        }

        var(start, l) = secInfo;
        if(start < 0)
        {
            return string.Empty;
        }

        return s.Substring(start, l);
    }

    private record MinSecInfo(int StartsFrom, int Length);
}