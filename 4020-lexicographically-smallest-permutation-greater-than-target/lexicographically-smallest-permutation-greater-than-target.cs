public class Solution 
{
    /// <summary>
    /// Returns the lexicographically smallest permutation of <paramref name="s"/> that is strictly greater than
    /// <paramref name="target"/>, or an empty string when no such permutation exists.
    /// </summary>
    /// <param name="s">
    /// The characters to permute. Assumed to contain only 'a' through 'z' and to be the same length as
    /// <paramref name="target"/>.
    /// </param>
    /// <param name="target">The exclusive lower bound that the produced permutation must exceed, compared ordinally.</param>
    /// <returns>The smallest qualifying permutation, or <see cref="string.Empty"/> when there is none.</returns>
    /// <remarks>
    /// <para>
    /// The input is reduced to a 26 slot frequency table, because only the multiset of available characters matters,
    /// not their original order. The answer is then built one position at a time with backtracking: the prefix is kept
    /// tied to <paramref name="target"/> for as long as possible, and the tie is broken at the latest position that
    /// still has a larger character available. Breaking as late as possible, with the smallest character that breaks
    /// it, is what makes the result minimal.
    /// </para>
    /// <para>
    /// At position <c>index</c> the candidates are scanned in ascending order starting at <c>target[index]</c>.
    /// Anything smaller would make the result permanently less than the target, so it is never considered.
    /// </para>
    /// <list type="bullet">
    ///   <item><description>
    ///   A candidate strictly greater than <c>target[index]</c> makes the prefix exceed the target regardless of what
    ///   follows, so all remaining characters are emitted in ascending order to form the smallest possible tail and the
    ///   search returns immediately.
    ///   </description></item>
    ///   <item><description>
    ///   A candidate equal to <c>target[index]</c> leaves the prefix tied, so the method recurses to the next position.
    ///   If that branch fails, the character is returned to the frequency table, the builder is truncated back to
    ///   <c>index</c>, and the scan continues with the next larger candidate.
    ///   </description></item>
    /// </list>
    /// <para>
    /// Exhausting every candidate at a position propagates failure upward; failure at position zero means no
    /// permutation of <paramref name="s"/> exceeds <paramref name="target"/>.
    /// </para>
    /// <para>
    /// Each position is expanded at most once for the tie and once for the first larger candidate, so the search visits
    /// O(n) nodes and scans at most 26 characters at each, instead of enumerating all n! permutations.
    /// </para>
    /// </remarks>
    public string LexGreaterPermutation(string s, string target)
    {
        // Count the frequency of each character in the string s
        int[] freq = new int['z' - 'a' + 1];
        foreach (char ch in s)
        {
            ++freq[ch - 'a'];
        }

        return LexGreaterPermutationInternal(0, new StringBuilder());

        /*
        * Helper function to recursively build the lexicographically smallest permutation greater than the target string.
        */
        string LexGreaterPermutationInternal(int index, StringBuilder sb)
        {
            // Base case: If we have constructed a string of the same length as the target check if it is lexicographically greater than the target
            if (sb.Length == target.Length)
            {
                string result = sb.ToString();

                return string.Compare(result, target, StringComparison.Ordinal) > 0 ? result : string.Empty;
            }

            char targetChar = target[index];

            // Iterate through all characters from the target character to 'z' to find the next greater character
            for (char letter = targetChar; letter <= 'z'; ++letter)
            {
                if (freq[letter - 'a'] <= 0)
                {
                    continue;
                }

                --freq[letter - 'a']; sb.Append(letter);

                // Handle the case where the current letter is equal to the target character
                if (letter > targetChar)
                {
                    for (char l = 'a'; l <= 'z'; ++l)
                    {
                        while (freq[l - 'a']-- > 0)
                        {
                            sb.Append(l);
                        }
                    }

                    return sb.ToString();
                }

                // Do recursive call to the next index, if the letter is equal to the targetChar 

                string result = LexGreaterPermutationInternal(index + 1, sb);
                if (!string.IsNullOrEmpty(result))
                {
                    return result;
                }

                // Backtrack current letter and try the next letter
                freq[letter - 'a']++; sb.Remove(index, 1);
            }

            return string.Empty;
        }
    }
}