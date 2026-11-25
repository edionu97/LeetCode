public class Solution 
{
    public int StrStr(string haystack, string needle)
    {
        // Check for null or empty strings
        if (string.IsNullOrEmpty(haystack) || string.IsNullOrEmpty(needle))
        {
            return -1;
        }

        // Build the prefix array for the needle string
        int[] prefixArray = BuildPrefixArray(needle);

        int i = 0, j = 0;
        while (i < haystack.Length)
        {
            // Advance both pointers while characters match
            while (i < haystack.Length && j < needle.Length && haystack[i] == needle[j])
            {
                i++;
                j++;
            }

            // If we have matched the entire needle, return the starting index
            if (j == needle.Length)
            {
                return i - j;
            }

            // If there's a mismatch, use the prefix array to skip characters in the needle
            if (j != 0)
            {
                j = prefixArray[j - 1];
                continue;
            }

            // If j is 0, we just move to the next character in the haystack
            i++;
        }

        return -1;

        /*
        * Helper function to build the prefix array for the needle string.
        * The prefix array is used to determine how many characters we can skip when a mismatch occurs during the search.
        */
        static int[] BuildPrefixArray(string str)
        {
            int[] prefixArray = new int[str.Length];

            int k = 0;

            // Uses the idea from the KMP algorithm to build the prefix array
            for (int i = 1; i < str.Length; i++)
            {
                // If the current character doesn't match, we fall back to the previous longest prefix
                while (k > 0 && str[k] != str[i])
                {
                    k = prefixArray[k - 1];
                }

                // If the current character matches, we can extend the current longest prefix
                if (str[k] == str[i])
                {
                    k++;
                }

                prefixArray[i] = k;
            }

            return prefixArray;
        }
    }
}