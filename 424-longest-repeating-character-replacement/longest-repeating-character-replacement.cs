public class Solution 
{
    public int CharacterReplacement(string s, int k)
    {
        int[] freq = new int['Z' - 'A' + 1];

        int maxSubstringSize = int.MinValue;

        // Use a sliding window approach to build the substring
        // We'll increase the window size as long as the number of replacements requied in order to have a same character substring is less or equal to k
        // Otherwise will move the window right, by removing the intial characters, untill we obtain a new valid window
        for (int i = 0, left = 0, right = 0; i < s.Length; ++i)
        {
            // Increase freq of current letter
            freq[s[i] - 'A']++;

            // Remove items from left window untill we respect the problem conditions
            while ((right - left + 1) - GetMaxFreq(freq) > k)
            {
                freq[s[left++] - 'A']--;
            }

            // Compute the max size of the substring
            maxSubstringSize = Math.Max(maxSubstringSize, right - left + 1);

            // Increase window
            ++right;
        }

        return maxSubstringSize;

        static int GetMaxFreq(int[] freq)
        {
            int max = int.MinValue;

            for (int i = 'A'; i <= 'Z'; ++i)
            {
                max = Math.Max(max, freq[i - 'A']);
            }

            return max;
        }
    }
}