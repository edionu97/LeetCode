using System.Collections.Immutable;
using System.Runtime.InteropServices;

public class Solution 
{
    /// <summary>
    /// Returns the lexicographically smallest palindrome that can be formed by permuting the characters of
    /// <paramref name="s"/> and that is strictly greater than <paramref name="target"/>, or an empty string
    /// when no such palindrome exists.
    /// </summary>
    /// <param name="s">The multiset of characters to permute. Assumed to be the same length as <paramref name="target"/>.</param>
    /// <param name="target">The exclusive lower bound that the produced palindrome must exceed, compared ordinally.</param>
    /// <returns>The smallest qualifying palindromic permutation, or <see cref="string.Empty"/> when there is none.</returns>
    /// <remarks>
    /// <para>
    /// <b>Feasibility and structure.</b> A string has a palindromic permutation only when at most one character
    /// occurs an odd number of times, so the method first builds a character frequency map and rejects the input
    /// outright when two or more characters have odd counts. The odd character, if there is one, is reserved as the
    /// palindrome's center and its count is reduced by one, which leaves every remaining count even. Those remaining
    /// characters are split evenly between the two mirrored halves, so the whole palindrome is determined by its
    /// first half. Finding the smallest valid first half therefore yields the smallest valid palindrome.
    /// </para>
    /// <para>
    /// <b>Building the first half.</b> The half is filled left to right with backtracking, using the classic
    /// "next greater arrangement of the same digits" strategy. The distinct characters are sorted once into
    /// <c>letters</c>; a character is a usable candidate at any point only if at least two of it remain, because
    /// anything placed in the first half is mirrored in the second. At position <c>index</c> the scan is restricted
    /// to candidates not smaller than <c>target[index]</c>, since a smaller character would make the result
    /// permanently less than the target.
    /// </para>
    /// <list type="bullet">
    ///   <item><description>
    ///   <b>Candidate greater than <c>target[index]</c>:</b> the prefix now exceeds the target regardless of what
    ///   follows, so the remaining slots are filled greedily with the smallest characters still available. That is
    ///   the minimal completion, and the search stops.
    ///   </description></item>
    ///   <item><description>
    ///   <b>Candidate equal to <c>target[index]</c>:</b> the result is still tied with the target, so the method
    ///   recurses to the next position. If that branch fails, the character's count and the builder are rolled back
    ///   and the scan continues with the next larger candidate.
    ///   </description></item>
    /// </list>
    /// <para>
    /// <b>Final check.</b> A branch that stays tied all the way to the end can finish equal to or below the target,
    /// for example when the target is itself a palindromic permutation of <paramref name="s"/>. The completed half
    /// is therefore expanded into the full palindrome and compared ordinally; anything not strictly greater fails
    /// the branch and forces backtracking.
    /// </para>
    /// <para>
    /// <b>Cost.</b> At most one candidate per position equals the target character, and the first larger candidate
    /// terminates the search, so each position is expanded at most twice. That gives roughly O(n * k) work for an
    /// alphabet of size k, instead of the exponential cost of enumerating permutations.
    /// </para>
    /// </remarks>
    public string LexPalindromicPermutation(string s, string target)
    {
        // Build the frequency dictionary for the characters in the input string and test if a palindromic permutation is possible
        IDictionary<char, int> freq = BuildFrequencyLetterMap(s);
        if (freq.Count(x => x.Value % 2 != 0) > 1)
        {
            return string.Empty;
        }

        if (target.Length == 1 && string.Compare(s, target, StringComparison.Ordinal) > 0)
        {
            return s;
        }

        // Try select the middle character for the palindrome if the length of the input string is odd
        char? middleChr = SelectMiddleCharacter();

        // Build the first half of the palindrome
        string firstHalf = BuildFirstPalindromicHalf();
        if (string.IsNullOrEmpty(firstHalf))
        {
            return string.Empty;
        }

        // Build the full palindrome from the first half and the middle character (if any)
        return BuildPalindromeFromFirstHalf(firstHalf, middleChr);

        /*
        * Helper function to get the frequency of characters in a string
        */
        static IDictionary<char, int> BuildFrequencyLetterMap(string str)
        {
            Dictionary<char, int> freq = new();

            // Count the frequency of each character in the string
            foreach (char c in str)
            {
                ++CollectionsMarshal.GetValueRefOrAddDefault(freq, c, out _);
            }

            return freq;
        }

        /*
        * Helper function to select the middle character of the palindrome if there is one (i.e., if there is a character with an odd frequency).
        */
        char? SelectMiddleCharacter()
        {
            // Check if there exists a character with an odd frequency, which will be the middle character of the palindrome
            if (freq.All(x => x.Value % 2 == 0))
            {
                return null;
            }


            // Select and decrease the frequency of the character with an odd frequency to be used as the middle character of the palindrome
            char mid = freq.SingleOrDefault(x => x.Value % 2 != 0).Key;
            --freq[mid];

            return mid;
        }

        /*
        * Helper function to build the first half of the palindrome that is lexicographically greater than the target string
        */
        string BuildFirstPalindromicHalf()
        {
            // Build the first half of the palindrome
            int firstHalfLength = freq.Sum(x => x.Value) >> 1;

            // Get the letters in lexicographical order
            ImmutableArray<char> letters = [.. freq.Keys.OrderBy(c => c)];

            // Initialize a StringBuilder to build the first half of the palindrome and start the recursive function to build the first half of the palindrome
            return BuildFirstPalindromicHalfInternal(0, new StringBuilder(firstHalfLength));

            /*
            * Internal recursive function to build the first half of the palindrome
            */
            string BuildFirstPalindromicHalfInternal(int index, StringBuilder sb)
            {
                // If we've filled the fist half check if the resulting palindrome is lexicographically greater than the target
                if (index == firstHalfLength)
                {
                    string result = BuildPalindromeFromFirstHalf(sb.ToString(), middleChr);

                    // To be sure we need to actually build the full palindrome and compare it with the target
                    return string.Compare(result, target, StringComparison.Ordinal) <= 0
                        ? string.Empty
                        : sb.ToString();
                }

                // Get the character at the current index of the target string
                char targetFirstChar = target[index];

                // Iterate through the letters in lexicographical order, starting from the target character at the current index
                foreach (char letter in letters.Where(l => l >= targetFirstChar))
                {
                    if (!IsFirstHalfPalindromicCandidate(letter))
                    {
                        continue;
                    }

                    // Use the letter for the current index and decrease its frequency by 2
                    freq[letter] -= 2; sb.Append(letter);

                    // If the letter is greater than the target character, we can fill the rest of the first half with the smallest available letters
                    if (letter > targetFirstChar)
                    {
                        foreach (var chr in letters.Where(IsFirstHalfPalindromicCandidate))
                        {
                            // There is no more room to add the letter to the first half of the palindrome
                            if (sb.Length == firstHalfLength)
                            {
                                break;
                            }

                            // Use the letter for the current index and decrease its frequency by 2
                            do
                            { 
                                freq[chr] -= 2; sb.Append(chr);
                            } 
                            while (sb.Length < firstHalfLength && IsFirstHalfPalindromicCandidate(chr));
                        }

                        // Stop if we have filled the first half of the palindrome
                        if (sb.Length == firstHalfLength)
                        {
                            return sb.ToString();
                        }

                        break;
                    }

                    // When letters are equal, we need to check the next index to ensure that the resulting palindrome is lexicographically greater than the target
                    string half = BuildFirstPalindromicHalfInternal(index + 1, sb);
                    if (!string.IsNullOrEmpty(half))
                    {
                        return half;
                    }

                    freq[letter] += 2; sb.Remove(index, 1);
                }

                // If we reach this point, it means we couldn't find a valid palindromic half that is lexicographically greater than the target
                return string.Empty;

                /*
                * Check if the letter can be used to build the first half of the palindrome
                * Important: The letter must have a frequency of at least 2 to be used in the first half of the palindrome because it will be mirrored in the second half of the palindrome.
                */
                bool IsFirstHalfPalindromicCandidate(char letter)
                {
                    if (!freq.TryGetValue(letter, out int value))
                    {
                        return false;
                    }

                    return value - 2 >= 0;
                }
            }
        }

        /*
        * Helper function to build the full palindrome from the first half and the middle character (if any)
        */
        static string BuildPalindromeFromFirstHalf(string firstHalf, char? middleChr)
        {
            return firstHalf + middleChr + new string(firstHalf.Reverse().ToArray());
        }
    }
}