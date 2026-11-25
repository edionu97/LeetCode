public class Solution 
{
    public string LongestPalindrome(string s)
    {
        if(string.IsNullOrEmpty(s))
        {
            return s;
        }

        return RunManManacherAlgToComputeTheLongestPalindromicStr(s);

        /**
        * <summary>
        * Returns the longest palindromic substring of <paramref name="s"/> in O(n) time using
        * Manacher's algorithm.
        * </summary>
        * <remarks>
        * Expanding around every center is O(n^2) because neighbouring centers keep re-comparing the
        * same characters. Manacher's insight is that a palindrome is symmetric, so once we know the
        * widest palindrome found so far and its boundaries, the radius at any position inside those
        * boundaries can be seeded from its mirror position instead of being measured from scratch.
        * Each character then takes part in at most one successful expansion, which is what makes the
        * whole scan linear.
        * <para>
        * Even-length palindromes have no single character at their center, so the input is first
        * interleaved with '#' separators. Every palindrome in the transformed string is odd-length
        * and centered on a real index, so one pass handles both parities.
        * </para>
        * </remarks>
        * <param name="s">The string to search. Must not be empty.</param>
        * <returns>The longest palindromic substring, or the last one found if several are tied.</returns>
        */
        static string RunManManacherAlgToComputeTheLongestPalindromicStr(string s)
        {
            // Transform the string to handle even-length palindromes
            string transformedS = ConvertToManacherString(s);

            int boundaryLeft = -1;
            int boundaryRight = 1;
            int[] palindromeLengths = new int[transformedS.Length];

            for (int current = 0; current < transformedS.Length; current++)
            {
                // Calculate the mirror position of the current character,  relative to the center  = 'current'
                int currentLeftMirror = boundaryRight + boundaryLeft - current;

                // If the current position is within the right boundary, we can use the previously calculated palindrome lengths
                if (current < boundaryRight && currentLeftMirror >= 0 && currentLeftMirror < transformedS.Length)
                {
                    // Value of the palindrome length at current character is the minimum of the palindrome length at the mirror position and the distance to the right boundary
                    // That's because the palindrome at the mirror position is guaranteed to be a palindrome, but it may extend beyond the right boundary
                    palindromeLengths[current] = Math.Min(boundaryRight - current, palindromeLengths[currentLeftMirror]);
                }

                // Expand the palindrome centered at the current character
                while (current + palindromeLengths[current] + 1 < transformedS.Length
                    && current - palindromeLengths[current] - 1 >= 0
                    && transformedS[current + palindromeLengths[current] + 1] == transformedS[current - palindromeLengths[current] - 1])
                {
                    palindromeLengths[current]++;
                }

                // No need to update, our palindrome has not expanded beyond the right boundary
                if (current + palindromeLengths[current] <= boundaryRight)
                {
                    continue;
                }

                // Update the boundaries to the new palindrome
                boundaryLeft = current - palindromeLengths[current];
                boundaryRight = current + palindromeLengths[current];
            }

            // Find the maximum length and its center index
            int maxLength = int.MinValue, center = -1;
            for (int index = 0; index < transformedS.Length; index++)
            {
                // compute the maximum length and center of the longest palindromic substring
                if (palindromeLengths[index] <= maxLength)
                {
                    continue;
                }

                maxLength = palindromeLengths[index] - 1;
                center = index;
            }

            ReadOnlySpan<char> longestPalindromicSubstring = transformedS.AsSpan(center - maxLength, 2 * maxLength + 1);
            return ConvertFromManacherString(longestPalindromicSubstring);

            /*
            * Helper function to transform the input string by inserting a special character (e.g., '#') between each character and at the beginning and end of the string.
            */
            static string ConvertToManacherString(string s)
            {
                StringBuilder sb = new("#");
                foreach (char ch in s)
                {
                    sb.Append($"{ch}#");
                }

                return sb.ToString();
            }

            /*
            * Helper function to convert the transformed string back to the original string by removing the special characters.
            */
            static string ConvertFromManacherString(ReadOnlySpan<char> s)
            {
                StringBuilder b = new();
                foreach (char c in s)
                {
                    if (c == '#')
                    {
                        continue;
                    }
                    b.Append(c);
                }

                return b.ToString();
            }
        }
    }
}